﻿#  
# Module manifest for module 'AzureRM.Installer'  
#  
# Generated by: Microsoft Corporation  
#  
# Generated on: 9/18/2015  
#  
  
@{  
  
#Base module
RootModule = '.\AzureRM.psm1'

# Version number of this module.  
ModuleVersion = '1.4.0'  
  
# ID used to uniquely identify this module  
GUID = 'B433E830-B479-4F7F-9C80-9CC6C28E1B51'  
  
# Author of this module  
Author = 'Microsoft Corporation'  
  
# Company or vendor of this module  
CompanyName = 'Microsoft Corporation'  
  
# Copyright statement for this module  
Copyright = 'Microsoft Corporation. All rights reserved.'    
  
# Description of the functionality provided by this module  
Description = 'Azure Resource Manager Module'  
  
# Minimum version of the Windows PowerShell engine required by this module  
PowerShellVersion = '3.0'  
  
# Name of the Windows PowerShell host required by this module  
PowerShellHostName = ''  
  
# Minimum version of the Windows PowerShell host required by this module  
PowerShellHostVersion = ''  
  
# Minimum version of the .NET Framework required by this module  
DotNetFrameworkVersion = '4.0'  
  
# Minimum version of the common language runtime (CLR) required by this module  
CLRVersion='4.0'  
  
# Processor architecture (None, X86, Amd64, IA64) required by this module  
ProcessorArchitecture = 'None'  
  
# Modules that must be imported into the global environment prior to importing this module  
RequiredModules = @(
    @{ ModuleName = 'AzureRM.Profile'; RequiredVersion = '1.0.8'},
    @{ ModuleName = 'Azure.Storage'; RequiredVersion = '1.1.2'},
    @{ ModuleName = 'AzureRM.ApiManagement'; RequiredVersion = '1.1.0'},
    @{ ModuleName = 'AzureRM.Automation'; RequiredVersion = '1.0.8'},
    @{ ModuleName = 'AzureRM.Backup'; RequiredVersion = '1.0.8'},
    @{ ModuleName = 'AzureRM.Batch'; RequiredVersion = '1.1.0'},
    @{ ModuleName = 'AzureRM.Cdn'; RequiredVersion = '1.0.2'},
    @{ ModuleName = 'AzureRM.Compute'; RequiredVersion = '1.3.0'},
    @{ ModuleName = 'AzureRM.DataFactories'; RequiredVersion = '1.0.8'},
    @{ ModuleName = 'AzureRM.DataLakeAnalytics'; RequiredVersion = '1.1.0'},
    @{ ModuleName = 'AzureRM.DataLakeStore'; RequiredVersion = '1.0.8'},
    @{ ModuleName = 'AzureRM.DevTestLabs'; RequiredVersion = '1.0.0'},
    @{ ModuleName = 'AzureRM.Dns'; RequiredVersion = '1.0.8'},
    @{ ModuleName = 'AzureRM.HDInsight'; RequiredVersion = '1.1.0'},
    @{ ModuleName = 'AzureRM.Insights'; RequiredVersion = '1.0.8'},
    @{ ModuleName = 'AzureRM.KeyVault'; RequiredVersion = '1.1.7'},
    @{ ModuleName = 'AzureRM.LogicApp'; RequiredVersion = '1.0.4'},
    @{ ModuleName = 'AzureRM.Network'; RequiredVersion = '1.0.9'},
    @{ ModuleName = 'AzureRM.NotificationHubs'; RequiredVersion = '1.0.8'},
    @{ ModuleName = 'AzureRM.OperationalInsights'; RequiredVersion = '1.0.8'},
    @{ ModuleName = 'AzureRM.RecoveryServices'; RequiredVersion = '1.1.0'},
    @{ ModuleName = 'AzureRM.RecoveryServices.Backup'; RequiredVersion = '1.0.0'},
    @{ ModuleName = 'AzureRM.RedisCache'; RequiredVersion = '1.1.6'},
    @{ ModuleName = 'AzureRM.Resources'; RequiredVersion = '1.1.0'},
    @{ ModuleName = 'AzureRM.ServerManagement'; RequiredVersion = '1.0.0'},
    @{ ModuleName = 'AzureRM.SiteRecovery'; RequiredVersion = '1.1.7'},
    @{ ModuleName = 'AzureRM.Sql'; RequiredVersion = '1.0.8'},
    @{ ModuleName = 'AzureRM.Storage'; RequiredVersion = '1.1.0'},
    @{ ModuleName = 'AzureRM.StreamAnalytics'; RequiredVersion = '1.0.8'},
    @{ ModuleName = 'AzureRM.Tags'; RequiredVersion = '1.0.8'},
    @{ ModuleName = 'AzureRM.TrafficManager'; RequiredVersion = '1.0.8'},
    @{ ModuleName = 'AzureRM.UsageAggregates'; RequiredVersion = '1.0.8'},
    @{ ModuleName = 'AzureRM.Websites'; RequiredVersion = '1.1.0'}
) 
  
# Assemblies that must be loaded prior to importing this module  
RequiredAssemblies = @()  
  
# Script files (.ps1) that are run in the caller's environment prior to importing this module  
ScriptsToProcess = @()  
  
# Type files (.ps1xml) to be loaded when importing this module  
TypesToProcess = @(
)  
  
# Format files (.ps1xml) to be loaded when importing this module  
FormatsToProcess = @( 
)  
  
# Modules to import as nested modules of the module specified in ModuleToProcess  
NestedModules = @()  
  
# Functions to export from this module  
FunctionsToExport = '*'  
  
# Cmdlets to export from this module  
CmdletsToExport = '*'  
  
# Variables to export from this module  
VariablesToExport = '*'  
  
# Aliases to export from this module  
AliasesToExport = '*'  
  
# List of all modules packaged with this module  
ModuleList = @()  
  
# List of all files packaged with this module  
FileList =  @()  
  
# Private data to pass to the module specified in ModuleToProcess  
PrivateData = @{

    PSData = @{

        # Tags applied to this module. These help with module discovery in online galleries.
        # Tags = @()

        # A URL to the license for this module.
        LicenseUri = 'https://raw.githubusercontent.com/Azure/azure-powershell/dev/LICENSE.txt'

        # A URL to the main website for this project.
        ProjectUri = 'https://github.com/Azure/azure-powershell'

        # A URL to an icon representing this module.
        # IconUri = ''

        # ReleaseNotes of this module
        ReleaseNotes = 'https://github.com/Azure/azure-powershell/blob/dev/ChangeLog.md'

    } # End of PSData hashtable

} # End of PrivateData hashtable  

} 
