// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using Demo.Core.Data;
using System;
using System.Collections.Generic;

namespace Demo.Core.DomainModel.App
{
    public partial class Devices : BaseEntity
    {
        public long Id { get; set; }
        public string DeviceId { get; set; }
        public string VirtualLaboratoryId { get; set; }
        public string Vendor { get; set; }
        public string SerialNumber { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string ModelType { get; set; }
        public string ModelIdentificationId { get; set; }
        public string ModelIdentificationVersion { get; set; }
        public string BaseModelIdentificationId { get; set; }
        public string BaseModelIdentificationVersion { get; set; }

        public virtual DeviceTwinProperties DeviceTwinProperties { get; set; }
    }
}