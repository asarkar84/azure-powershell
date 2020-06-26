// ----------------------------------------------------------------------------------
//
// Copyright Microsoft Corporation
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// http://www.apache.org/licenses/LICENSE-2.0
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// ----------------------------------------------------------------------------------

using System;
using Microsoft.Azure.Commands.Billing.Common;
using Microsoft.Azure.Commands.Billing.Models;
using Microsoft.Azure.Management.Billing;
using Microsoft.Azure.Management.Billing.Models;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using Microsoft.Rest.Azure;
using Microsoft.WindowsAzure.Commands.Common.CustomAttributes;

namespace Microsoft.Azure.Commands.Billing.Cmdlets.Invoices
{

    [Cmdlet("Get", ResourceManager.Common.AzureRMConstants.AzureRMPrefix + "BillingInvoice", DefaultParameterSetName = Constants.ParameterSetNames.ListParameterSet), OutputType(typeof(PSInvoice))]
    public class GetAzureRmBillingInvoice : AzureBillingCmdletBase
    {
        [Parameter(Mandatory = true, HelpMessage = "Get the latest invoice.", ParameterSetName = Constants.ParameterSetNames.LatestItemParameterSet)]
        public SwitchParameter Latest { get; set; }

        [Parameter(Mandatory = true, HelpMessage = "Name of a specific invoice to get.", ParameterSetName = Constants.ParameterSetNames.SingleItemParameterSet)]
        [ValidateNotNullOrEmpty]
        public List<string> Name { get; set; }

        [Parameter(Mandatory = false, HelpMessage = "Determine the maximum number of records to return.", ParameterSetName = Constants.ParameterSetNames.ListParameterSet)]
        [ValidateNotNull]
        [ValidateRange(1, 100)]
        public int? MaxCount { get; set; }

        [Parameter(Mandatory = false, HelpMessage = "Generate the download url of the invoices.")]
        public SwitchParameter GenerateDownloadUrl { get; set; }

        // 2020 GA Parameters
        [Parameter(Mandatory = false, Position = 0, HelpMessage = "Name of the billing account to get invoices.")]
        public string BillingAccountName { get; set; }

        [Parameter(Mandatory = false, Position = 0, HelpMessage = "Name of the billing profile to get invoices.")]
        public string BillingProfileName { get; set; }

        [Parameter(Mandatory = false, Position = 0, HelpMessage = "The start date to fetch the invoices. The date should be specified in MM-DD-YYYY format.")]
        public DateTime? PeriodStartDate { get; set; }

        [Parameter(Mandatory = false, Position = 0, HelpMessage = "The end date to fetch the invoices. The date should be specified in MM-DD-YYYY format.")]
        public DateTime? PeriodEndDate { get; set; }

        public override void ExecuteCmdlet()
        {
            try
            {
                var endDate = DateTime.Now;
                var oneYearSpan = endDate.Subtract(new TimeSpan(365, 0, 0, 0));
                var startDate = new DateTime(oneYearSpan.Year, oneYearSpan.Month,1);

                if (PeriodStartDate != null && PeriodEndDate != null)
                {
                    startDate = PeriodStartDate.Value;
                    endDate = PeriodEndDate.Value;
                }

                var periodStartDate = startDate.ToString("O");
                var periodEndDate = endDate.ToString("O");

                dynamic invoices = null;
                
                if (ParameterSetName.Equals(Constants.ParameterSetNames.ListParameterSet))
                {
                    // modern flow
                    if (!string.IsNullOrWhiteSpace(BillingAccountName))
                    {
                        if (!string.IsNullOrWhiteSpace(BillingProfileName))
                        {
                            //fetch by /{ba}/{bp}
                            invoices =
                                BillingManagementClient.Invoices.ListByBillingProfile(
                                    billingAccountName: BillingAccountName,
                                    billingProfileName: BillingProfileName,
                                    periodStartDate: periodStartDate,
                                    periodEndDate: periodEndDate);
                        }
                        else // fetch by /{ba}
                        {
                            invoices = BillingManagementClient.Invoices.ListByBillingAccount(
                                billingAccountName: BillingAccountName,
                                periodStartDate: periodStartDate,
                                periodEndDate: periodEndDate);
                        }
                    }
                    else // ba/billingSub/{subId}/invoices
                    {
                        var invoicesList = BillingManagementClient.Invoices.ListByBillingSubscription(
                            periodStartDate: periodStartDate,
                            periodEndDate: periodEndDate);

                        invoices = (from invoice in invoicesList
                            where invoice.InvoiceDate.HasValue
                            orderby invoice.InvoiceDate.Value descending
                            select invoice).Take(MaxCount ?? 100);
                    }

                    if (invoices != null)
                    {
                        foreach (var invoice in invoices)
                        { 
                            var psInvoice = new PSInvoice(invoice);
                            if (GenerateDownloadUrl)
                            {
                                this.GetDownloadUrl(psInvoice.Name, invoice, psInvoice);
                            }
                           
                            WriteObject(psInvoice);
                        }
                    }

                    return;
                }

                if (ParameterSetName.Equals(Constants.ParameterSetNames.LatestItemParameterSet))
                {
                    var invoicesList = BillingManagementClient.Invoices.ListByBillingSubscription(
                        periodStartDate: periodStartDate,
                        periodEndDate: periodEndDate);

                    if (invoicesList != null && invoicesList.Any())
                    {
                        var recentInvoice = (from invoice in invoicesList
                            where invoice.InvoiceDate.HasValue
                            orderby invoice.InvoiceDate descending
                            select invoice).Take(1);

                        WriteObject(new PSInvoice(recentInvoice.FirstOrDefault()));
                    }
                }
                else if (ParameterSetName.Equals(Constants.ParameterSetNames.SingleItemParameterSet))
                {
                    foreach (var invoiceName in Name)
                    {
                        try
                        {
                            Invoice invoice = null;
                            if (!string.IsNullOrWhiteSpace(BillingAccountName)) // modern 
                            {
                                invoice = BillingManagementClient.Invoices.Get(
                                    billingAccountName: BillingAccountName,
                                    invoiceName: invoiceName);
                            }
                            else // legacy
                            {
                                invoice =
                                    BillingManagementClient.Invoices.GetBySubscriptionAndInvoiceId(invoiceName);
                            }

                            var psInvoice = new PSInvoice(invoice);
                            if (GenerateDownloadUrl)
                            {
                                this.GetDownloadUrl(invoiceName, invoice, psInvoice);
                            }

                            WriteObject(psInvoice);
                        }
                        catch (ErrorResponseException error)
                        {
                            WriteWarning(invoiceName + ": " + error.Body.Error.Message);
                            // continue with the next
                        }
                    }
                }
            }
            catch (ErrorResponseException e)
            {
                WriteWarning(e.Body.Error.Message);
            }
        }

        private static string GenerateDownloadToken(string url, string type)
        {
            if (url == null)
            {
                return string.Empty;
            }

            switch (type.ToUpperInvariant())
            {
                case "DRS":
                    var token = url.Split('=')?[1].Split('&')?[0];
                    return token??string.Empty;
                default:
                    return string.Empty;

            }
        }

        private void GetDownloadUrl(string invoiceName, Invoice invoice, PSInvoice psInvoice)
        {
            var invoiceDocument = invoice.Documents.FirstOrDefault(p =>
                p.Kind.ToUpperInvariant() == "INVOICE" && p.Url.Contains(invoice.Name));

            if (invoiceDocument != null)
            {
                var downloadUrl = BillingManagementClient.Invoices
                    .DownloadBillingSubscriptionInvoice(
                        invoiceName: invoiceName,
                        downloadToken: GenerateDownloadToken(invoiceDocument.Url,
                            invoiceDocument.Source));
                psInvoice.DownloadUrl = downloadUrl.Url;
                psInvoice.DownloadUrlExpiry = downloadUrl.ExpiryTime;
            }

        }
    }
}