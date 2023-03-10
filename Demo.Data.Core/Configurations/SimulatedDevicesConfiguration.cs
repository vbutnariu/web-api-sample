// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using Demo.Core.Data;
using Demo.Core.DomainModel.App;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;

namespace Demo.Core.Data.Configurations
{
    public partial class SimulatedDevicesConfiguration : IEntityTypeConfiguration<SimulatedDevices>
    {
        public void Configure(EntityTypeBuilder<SimulatedDevices> entity)
        {
            entity.HasKey(e => e.DeviceId)
                .HasName("simulated_devices_pkey");

            entity.ToTable("simulated_devices", "shared");

            entity.Property(e => e.DeviceId)
                .HasColumnType("character varying")
                .HasColumnName("device_id");

            entity.Property(e => e.AzureIotHubConnectionString)
                .IsRequired()
                .HasColumnType("character varying")
                .HasColumnName("azure_iot_hub_connection_string");

            entity.Property(e => e.ControllerId)
                .HasColumnType("character varying")
                .HasColumnName("controller_id");

            entity.Property(e => e.LastActiveAt).HasColumnName("last_active_at");

            OnConfigurePartial(entity);
        }

        partial void OnConfigurePartial(EntityTypeBuilder<SimulatedDevices> entity);
    }
}
