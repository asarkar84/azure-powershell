﻿// <auto-generated>
// Copyright (c) Microsoft and contributors.  All rights reserved.
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//   http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// 
// See the License for the specific language governing permissions and
// limitations under the License.
// 
// 
// Warning: This code was generated by a tool.
// 
// Changes to this file may cause incorrect behavior and will be lost if the
// code is regenerated.
// 
// For documentation on code generator please visit
//   https://aka.ms/nrp-code-generation
// Please contact wanrpdev@microsoft.com if you need to make changes to this file.
// </auto-generated>

using Microsoft.Azure.Management.Network.Models;
using Microsoft.WindowsAzure.Commands.Common.Attributes;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Microsoft.Azure.Commands.Network.Models
{
    public partial class PSPrivateEndpoint : PSTopLevelResource
    {
        [Ps1Xml(Target = ViewControl.Table)]
        public string ProvisioningState { get; set; }
        [Ps1Xml(Label = "Subnet", Target = ViewControl.Table, ScriptBlock = "$_.Subnet.Name")]
        public PSSubnet Subnet { get; set; }
        public List<PSNetworkInterface> NetworkInterfaces { get; set; }
        public List<PSPrivateLinkServiceConnection> PrivateLinkServiceConnections { get; set; }
        public List<PSPrivateLinkServiceConnection> ManualPrivateLinkServiceConnections { get; set; }

        [JsonIgnore]
        public string SubnetText
        {
            get { return JsonConvert.SerializeObject(Subnet, Formatting.Indented, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore }); }
        }

        [JsonIgnore]
        public string NetworkInterfacesText
        {
            get { return JsonConvert.SerializeObject(NetworkInterfaces, Formatting.Indented, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore }); }
        }

        [JsonIgnore]
        public string PrivateLinkServiceConnectionsText
        {
            get { return JsonConvert.SerializeObject(PrivateLinkServiceConnections, Formatting.Indented, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore }); }
        }

        [JsonIgnore]
        public string ManualPrivateLinkServiceConnectionsText
        {
            get { return JsonConvert.SerializeObject(ManualPrivateLinkServiceConnections, Formatting.Indented, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore }); }
        }

        public bool ShouldSerializeNetworkInterfaces()
        {
            return !string.IsNullOrEmpty(this.Name);
        }

        public bool ShouldSerializePrivateLinkServiceConnections()
        {
            return !string.IsNullOrEmpty(this.Name);
        }

        public bool ShouldSerializeManualPrivateLinkServiceConnections()
        {
            return !string.IsNullOrEmpty(this.Name);
        }

    }
}
