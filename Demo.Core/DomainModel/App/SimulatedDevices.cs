// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using Demo.Core.Data;
using System;
using System.Collections.Generic;

namespace Demo.Core.DomainModel.App
{
    public partial class SimulatedDevices : BaseEntity
    {
        public string DeviceId { get; set; }
        public string AzureIotHubConnectionString { get; set; }
        public DateTime? LastActiveAt { get; set; }
        public string ControllerId { get; set; }
    }
}