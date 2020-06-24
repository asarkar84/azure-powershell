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
        const string DownloadUrlExpand = "downloadUrl";

        [Parameter(Mandatory = true, HelpMessage = "Get the latest invoice.", ParameterSetName = Constants.ParameterSetNames.LatestItemParameterSet)]
        public SwitchParameter Latest { get; set; }

        [Parameter(Mandatory = true, HelpMessage = "Name of a specific invoice to get.", ParameterSetName = Constants.ParameterSetNames.SingleItemParameterSet)]
        [ValidateNotNullOrEmpty]
        public List<string> Name { get; set; }

        [Parameter(Mandatory = false, HelpMessage = "Determine the maximum number of records to return.", ParameterSetName = Constants.ParameterSetNames.ListParameterSet)]
        [ValidateNotNull]
        [ValidateRange(1, 100)]
        public int? MaxCount { get; set; }

        [Parameter(Mandatory = false, HelpMessage = "Generate the download url of the invoices.", ParameterSetName = Constants.ParameterSetNames.ListParameterSet)]
        public SwitchParameter GenerateDownloadUrl { get; set; }

        public override void ExecuteCmdlet()
        {
            try
            {
                if (ParameterSetName.Equals(Constants.ParameterSetNames.ListParameterSet))
                {
                    //string expand = this.GenerateDownloadUrl.IsPresent ? DownloadUrlExpand : null;

                    //WriteObject(BillingManagementClient.Invoices.List(expand, null, null, MaxCount).Select(x => new PSInvoice(x)), true);
                    
                    // call by subid 
                    // filter by top Max Count here in c#
                    var now = DateTime.Now;
                    var endDate = DateTime.Now;
                    var startDate = now.Subtract(new TimeSpan(365,0,0,0));

                    var invoicesList = BillingManagementClient.Invoices.ListByBillingSubscription(
                        periodStartDate: startDate.ToString(),
                        periodEndDate: endDate.ToString());

                    var invoices = (from invoice in invoicesList
                        orderby invoice.InvoiceDate
                        select invoice).Take(MaxCount.Value);

                    foreach (var invoice in invoices)
                    {
                        WriteObject(new PSInvoice(invoice));
                    }
                    return;
                }

                if (ParameterSetName.Equals(Constants.ParameterSetNames.LatestItemParameterSet))
                {
                    // return top1 
                    var now = DateTime.Now;
                    var endDate = DateTime.Now;
                    var startDate = now.Subtract(new TimeSpan(365, 0, 0, 0));

                    var invoicesList = BillingManagementClient.Invoices.ListByBillingSubscription(
                        periodStartDate: startDate.ToString(),
                        periodEndDate: endDate.ToString());

                    var invoices = (from invoice in invoicesList
                        orderby invoice.InvoiceDate
                        select invoice).Take(1);

                    WriteObject(new PSInvoice(invoices.First()));
                }
                else if (ParameterSetName.Equals(Constants.ParameterSetNames.SingleItemParameterSet))
                {
                    foreach (var invoiceName in Name)
                    {
                        try
                        {
                            // call GetInvoiceBySubscriptionAndInvoiceNumberTest
                            var invoice = new PSInvoice(BillingManagementClient.Invoices.GetBySubscriptionAndInvoiceId(invoiceName));
                            WriteObject(invoice);
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
    }

    ////[CmdletDeprecation("2.0.0")]
    //[Cmdlet("Get", ResourceManager.Common.AzureRMConstants.AzureRMPrefix + "BillingInvoice", DefaultParameterSetName = Constants.ParameterSetNames.ListParameterSet), OutputType(typeof(PSInvoice))]
    //public class GetAzureRmBillingInvoice : AzureBillingCmdletBase
    //{
    //    //const string DownloadUrlExpand = "downloadUrl";

    //    //[Parameter(Mandatory = true, HelpMessage = "Get the latest invoice.", ParameterSetName = Constants.ParameterSetNames.LatestItemParameterSet)]
    //    //public SwitchParameter Latest { get; set; }

    //    //[Parameter(Mandatory = true, HelpMessage = "Name of a specific invoice to get.", ParameterSetName = Constants.ParameterSetNames.SingleItemParameterSet)]
    //    //[ValidateNotNullOrEmpty]
    //    //public List<string> Name { get; set; }

    //    //[Parameter(Mandatory = false, HelpMessage = "Determine the maximum number of records to return.", ParameterSetName = Constants.ParameterSetNames.ListParameterSet)]
    //    //[ValidateNotNull]
    //    //[ValidateRange(1, 100)]
    //    //public int? MaxCount { get; set; }

    //    //[Parameter(Mandatory = false, HelpMessage = "Name of a specific billing account to get invoices.", ParameterSetName = Constants.ParameterSetNames.BillingAccountNameParameterSet)]
    //    //public string BillingAccountName { get; set; }

    //    //[Parameter(Mandatory = false, HelpMessage = "Name of a specific billing profile to get invoices.", ParameterSetName = Constants.ParameterSetNames.BillingProfileNameParameterSet)]
    //    //public string BillingProfileName { get; set; }

    //    //[Parameter(Mandatory = false, HelpMessage = "Generate the download url of the invoices.", ParameterSetName = Constants.ParameterSetNames.ListParameterSet)]
    //    //public SwitchParameter GenerateDownloadUrl { get; set; }

    //    [Parameter(Mandatory = false, Position = 0, HelpMessage = "Name of the billing account to get invoices.")]
    //    public string BillingAccountName { get; set; }

    //    [Parameter(Mandatory = false, Position = 0, HelpMessage = "Name of the billing profile to get invoices.")]
    //    public string BillingProfileName { get; set; }

    //    [Parameter(Mandatory = false, Position = 0, HelpMessage = "Name of the subscription to get invoices.")]
    //    public string SubscriptionName { get; set; }

    //    [Parameter(Mandatory = false, Position = 0, HelpMessage = "Name of a specific invoice to get.", ParameterSetName = Constants.ParameterSetNames.SingleItemParameterSet)]
    //    public string Name { get; set; }

    //    [Parameter(Mandatory = true, Position = 0, HelpMessage = "The start date to fetch the invoices. The date should be specified in MM-DD-YYYY format.", ParameterSetName = Constants.ParameterSetNames.ListParameterSet)]
    //    public DateTime PeriodStartDate { get; set; }

    //    [Parameter(Mandatory = true, Position = 0, HelpMessage = "The end date to fetch the invoices. The date should be specified in MM-DD-YYYY format.", ParameterSetName = Constants.ParameterSetNames.ListParameterSet)]
    //    public DateTime PeriodEndDate { get; set; }

    //    public override void ExecuteCmdlet()
    //    {
    //        try
    //        {
    //            if (ParameterSetName.Equals(Constants.ParameterSetNames.ListParameterSet))
    //            {
    //                if (!string.IsNullOrWhiteSpace(BillingAccountName))
    //                {
    //                    if (!string.IsNullOrWhiteSpace(BillingProfileName))
    //                    {
    //                        //fetch by /{ba}/{bp}
    //                        var invoices =
    //                            BillingManagementClient.Invoices.ListByBillingProfile(
    //                                billingAccountName: BillingAccountName,
    //                                billingProfileName: BillingProfileName,
    //                                periodStartDate: PeriodStartDate.ToString(),
    //                                periodEndDate: PeriodEndDate.ToString());
    //                        foreach (var invoice in invoices)
    //                        {
    //                            var currentInvoice = new PSInvoice(invoice);
    //                            WriteObject(currentInvoice);
    //                        }
    //                    }
    //                    else
    //                    {
    //                        //fetch by /{ba}
    //                        var invoices = BillingManagementClient.Invoices.ListByBillingAccount(
    //                            billingAccountName: BillingAccountName,
    //                            periodStartDate: PeriodStartDate.ToString(),
    //                            periodEndDate: PeriodEndDate.ToString());
    //                        foreach (var invoice in invoices)
    //                        {
    //                            var currentInvoice = new PSInvoice(invoice);
    //                            WriteObject(currentInvoice);
    //                        }
    //                    }
    //                }
    //                else
    //                {
    //                    // fetch by subid
    //                    var invoices = BillingManagementClient.Invoices.ListByBillingSubscription(
    //                        periodStartDate: PeriodStartDate.ToString(),
    //                        periodEndDate: PeriodEndDate.ToString());
    //                    foreach (var invoice in invoices)
    //                    {
    //                        var currentInvoice = new PSInvoice(invoice);
    //                        WriteObject(currentInvoice);
    //                    }
    //                }
    //                //string expand = this.GenerateDownloadUrl.IsPresent ? DownloadUrlExpand : null;
    //            }
    //            else if (ParameterSetName.Equals(Constants.ParameterSetNames.SingleItemParameterSet))
    //            {
    //                if (!string.IsNullOrWhiteSpace(BillingAccountName))
    //                {
    //                    //fetch specific invoice by /{ba}/invoices/invoiceName
    //                    WriteObject(BillingManagementClient.Invoices.Get(
    //                        billingAccountName: BillingAccountName,
    //                        invoiceName: Name));
    //                }
    //                else if (!string.IsNullOrWhiteSpace(SubscriptionName))
    //                {
    //                    //fetch by ba/billingsubscription/{subId}/invoiceName
    //                }
    //                else
    //                {
    //                    // fetch ba/default/invoiceName
    //                }
    //            }

    //            //if (ParameterSetName.Equals(Constants.ParameterSetNames.LatestItemParameterSet))
    //            //{
    //            //    WriteObject(new PSInvoice());
    //            //}
    //            //else if (ParameterSetName.Equals(Constants.ParameterSetNames.SingleItemParameterSet))
    //            //{
    //            //    foreach (var invoiceName in Name)
    //            //    {
    //            //        try
    //            //        {
    //            //            var invoice = new PSInvoice();
    //            //            WriteObject(invoice);
    //            //        }
    //            //        catch (ErrorResponseException error)
    //            //        {
    //            //            WriteWarning(invoiceName + ": " + error.Body.Error.Message);
    //            //            // continue with the next
    //            //        }
    //            //    }
    //            //}
    //        }
    //        catch (ErrorResponseException e)
    //        {
    //            WriteWarning(e.Body.Error.Message);
    //        }
    //    }
    //}
}
