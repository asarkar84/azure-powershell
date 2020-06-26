# ----------------------------------------------------------------------------------
#
# Copyright Microsoft Corporation
# Licensed under the Apache License, Version 2.0 (the "License");
# you may not use this file except in compliance with the License.
# You may obtain a copy of the License at
# http://www.apache.org/licenses/LICENSE-2.0
# Unless required by applicable law or agreed to in writing, software
# distributed under the License is distributed on an "AS IS" BASIS,
# WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
# See the License for the specific language governing permissions and
# limitations under the License.
# ----------------------------------------------------------------------------------

<#
.SYNOPSIS
List invoices
#>
function Test-ListInvoices
{
    $billingInvoices = Get-AzBillingInvoice

    Assert-NotNull $billingInvoices
    Assert-NotNull $billingInvoices.Name
    Assert-NotNull $billingInvoices.Status
    Assert-NotNull $billingInvoices.BillingProfileDisplayName
    Assert-NotNull $billingInvoices.InvoiceDate
    Assert-Null $billingInvoices.BillingProfileId
}

<#
.SYNOPSIS
List invoices with DownloadUrl
#>
function Test-ListInvoicesWithDownloadUrl
{
    $billingInvoices = Get-AzBillingInvoice -GenerateDownloadUrl

    Assert-NotNull $billingInvoices
    Assert-NotNull $billingInvoices.DownloadUrl
    Assert-NotNull $billingInvoices.Name
    Assert-NotNull $billingInvoices.Status
    Assert-NotNull $billingInvoices.BillingProfileDisplayName
    Assert-NotNull $billingInvoices.InvoiceDate
}

<#
.SYNOPSIS
List invoices with MaxCount
#>
function Test-ListInvoicesWithMaxCount
{
    $billingInvoices = Get-AzBillingInvoice -GenerateDownloadUrl -MaxCount 1

    Assert-NotNull $billingInvoices
    Assert-True {$billingInvoices.Count -eq 1}
    Assert-NotNull $billingInvoices.DownloadUrl
    Assert-NotNull $billingInvoices.Name
    Assert-NotNull $billingInvoices.Status
    Assert-NotNull $billingInvoices.BillingProfileDisplayName
    Assert-NotNull $billingInvoices.InvoiceDate
}

<#
.SYNOPSIS
Get latest invoice
#>
function Test-GetLatestInvoice
{
    $billingInvoice = Get-AzBillingInvoice -Latest

	Assert-NotNull $billingInvoice	
    Assert-NotNull $billingInvoice.Name
    Assert-NotNull $billingInvoice.Status
    Assert-NotNull $billingInvoice.BillingProfileDisplayName
    Assert-NotNull $billingInvoice.InvoiceDate
}

<#
.SYNOPSIS
Get invoice with specified name
#>
function Test-GetInvoiceWithName
{
	$billingInvoices = Get-AzBillingInvoice -Name T000512627

	Assert-NotNull $billingInvoices
	Assert-NotNull $billingInvoices.Name
    Assert-AreEqual $billingInvoices.Name T000512627
}

<#
.SYNOPSIS
List invoices
#>
function Test-GetInvoicesWithBillingAccountName
{
    $billingInvoices = Get-AzBillingInvoice -BillingAccountName db038d21-b0d2-463c-942f-b09127c6f4e4:7c9c4a38-593e-479e-8958-9a338a0d8d02_2019-05-31 -GenerateDownloadUrl

    Assert-NotNull $billingInvoices
    Assert-NotNull $billingInvoices.Name
    Assert-NotNull $billingInvoices.Status
    Assert-NotNull $billingInvoices.BillingProfileDisplayName
    Assert-NotNull $billingInvoices.InvoiceDate
    Assert-NotNull $billingInvoices.DownloadUrl
}

<#
.SYNOPSIS
List invoices
#>
function Test-GetInvoicesWithBillingProfileName
{
    $billingInvoices = Get-AzBillingInvoice -BillingAccountName db038d21-b0d2-463c-942f-b09127c6f4e4:7c9c4a38-593e-479e-8958-9a338a0d8d02_2019-05-31 -BillingProfileName RI2B-S4N4-BG7-TGB -GenerateDownloadUrl

    Assert-NotNull $billingInvoices
    Assert-NotNull $billingInvoices.Name
    Assert-NotNull $billingInvoices.Status
    Assert-NotNull $billingInvoices.BillingProfileDisplayName
    Assert-NotNull $billingInvoices.InvoiceDate
    Assert-NotNull $billingInvoices.DownloadUrl
}

