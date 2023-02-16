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
    public partial class DeviceModelsConfiguration : IEntityTypeConfiguration<DeviceModels>
    {
        public void Configure(EntityTypeBuilder<DeviceModels> entity)
        {
            entity.ToTable("device_models", "shared");

            entity.HasIndex(e => new { e.Vendor, e.Identifier, e.Version }, "device_models_vendor_identifier_version_idx")
                .IsUnique();

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");

            entity.Property(e => e.Akz)
                .IsRequired()
                .HasColumnType("character varying")
                .HasColumnName("akz");

            entity.Property(e => e.Identifier)
                .IsRequired()
                .HasColumnType("character varying")
                .HasColumnName("identifier");

            entity.Property(e => e.Name)
                .IsRequired()
                .HasColumnType("character varying")
                .HasColumnName("name");

            entity.Property(e => e.Type)
                .IsRequired()
                .HasColumnType("character varying")
                .HasColumnName("type");

            entity.Property(e => e.Vendor)
                .IsRequired()
                .HasColumnType("character varying")
                .HasColumnName("vendor");

            entity.Property(e => e.Version)
                .HasColumnType("character varying")
                .HasColumnName("version");

            OnConfigurePartial(entity);
        }

        partial void OnConfigurePartial(EntityTypeBuilder<DeviceModels> entity);
    }
}
