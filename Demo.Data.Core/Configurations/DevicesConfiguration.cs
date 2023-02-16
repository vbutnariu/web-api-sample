﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using Demo.Core.Data;
using Demo.Core.DomainModel.App;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;

namespace Demo.Core.Data.Configurations
{
    public partial class DevicesConfiguration : EntityTypeConfiguration<Devices>
    {
        public void Configure(EntityTypeBuilder<Devices> entity)
        {
            entity.ToTable("devices", "shared");

            entity.HasIndex(e => e.DeviceId, "devices_device_id_idx")
                .IsUnique();

            entity.HasIndex(e => e.VirtualLaboratoryId, "devices_virtual_laboratory_id_idx");

            entity.Property(e => e.Id).HasColumnName("id");

            entity.Property(e => e.BaseModelIdentificationId)
                .HasColumnType("character varying")
                .HasColumnName("base_model_identification_id");

            entity.Property(e => e.BaseModelIdentificationVersion)
                .HasColumnType("character varying")
                .HasColumnName("base_model_identification_version");

            entity.Property(e => e.DeviceId)
                .IsRequired()
                .HasColumnType("character varying")
                .HasColumnName("device_id");

            entity.Property(e => e.ModelIdentificationId)
                .HasColumnType("character varying")
                .HasColumnName("model_identification_id");

            entity.Property(e => e.ModelIdentificationVersion)
                .HasColumnType("character varying")
                .HasColumnName("model_identification_version");

            entity.Property(e => e.ModelType)
                .HasColumnType("character varying")
                .HasColumnName("model_type");

            entity.Property(e => e.Name)
                .HasColumnType("character varying")
                .HasColumnName("name");

            entity.Property(e => e.SerialNumber)
                .HasColumnType("character varying")
                .HasColumnName("serial_number");

            entity.Property(e => e.Type)
                .HasColumnType("character varying")
                .HasColumnName("type");

            entity.Property(e => e.Vendor)
                .HasColumnType("character varying")
                .HasColumnName("vendor");

            entity.Property(e => e.VirtualLaboratoryId)
                .IsRequired()
                .HasColumnType("character varying")
                .HasColumnName("virtual_laboratory_id");

            OnConfigurePartial(entity);
        }

        partial void OnConfigurePartial(EntityTypeBuilder<Devices> entity);
    }
}
