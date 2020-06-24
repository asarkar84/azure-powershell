﻿// ----------------------------------------------------------------------------------
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

using System.Collections.Generic;
using System.Linq;
using ApiBillingProfile = Microsoft.Azure.Management.Billing.Models.BillingProfile;

namespace Microsoft.Azure.Commands.Billing.Models
{
    public class PSBillingProfile
    {
        public string Id { get; private set; }

        public string Name { get; private set; }

        public string Type { get; private set; }

        public PSAddressDetails Address { get; set; }

        public string DisplayName { get; private set; }
        
        public string PurchaseOrderNumber { get; private set; }

        public bool? InvoiceEmailOptIn { get; private set; }
        
        public int? InvoiceDay { get; private set; }

        public string Currency { get; private set; }

        public IEnumerable<PSAzurePlan> EnabledAzurePlans { get; private set; }

        public IEnumerable<PSInvoiceSection> InvoiceSections { get; private set; }
        
        public PSBillingProfile()
        {
        }

        public PSBillingProfile(ApiBillingProfile billingProfile)
        {
            if (billingProfile != null)
            {
                this.Id = billingProfile.Id;
                this.Type = billingProfile.Type;
                this.Name = billingProfile.Name;
                this.Address = new PSAddressDetails(billingProfile.BillTo);
                this.DisplayName = billingProfile.DisplayName;
                this.PurchaseOrderNumber = billingProfile.PoNumber;
                this.InvoiceEmailOptIn = billingProfile.InvoiceEmailOptIn;
                this.InvoiceDay = billingProfile.InvoiceDay;
                this.Currency = billingProfile.Currency;
                if (billingProfile.EnabledAzurePlans != null)
                {
                    this.EnabledAzurePlans =
                        billingProfile.EnabledAzurePlans.Select(azurePlan => new PSAzurePlan(azurePlan));
                }

                if (billingProfile.InvoiceSections != null)
                {
                    this.InvoiceSections =
                        billingProfile.InvoiceSections.Value.Select(invoiceSection => new PSInvoiceSection(invoiceSection));
                }
            }
        }
    }
}
