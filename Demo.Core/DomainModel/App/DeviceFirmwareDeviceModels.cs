﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using Demo.Core.Data;
using System;
using System.Collections.Generic;

namespace Demo.Core.DomainModel.App
{
    public partial class DeviceFirmwareDeviceModels : BaseEntity
    {
        public Guid Id { get; set; }
        public Guid FirmwareId { get; set; }
        public Guid DeviceModelId { get; set; }
        public Guid? MinSuppVersionId { get; set; }
        public Guid? MaxSuppVersionId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? ModifiedAt { get; set; }

        public virtual DeviceModels DeviceModel { get; set; }
        public virtual DeviceFirmwares Firmware { get; set; }
        public virtual DeviceFirmwareVersions MaxSuppVersion { get; set; }
        public virtual DeviceFirmwareVersions MinSuppVersion { get; set; }
    }
}