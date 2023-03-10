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
    public partial class DeviceFirmwareVersionsConfiguration : IEntityTypeConfiguration<DeviceFirmwareVersions>
    {
        public void Configure(EntityTypeBuilder<DeviceFirmwareVersions> entity)
        {
            entity.ToTable("device_firmware_versions", "shared");

            entity.HasIndex(e => new { e.FirmwareId, e.Version }, "device_firmware_versions_firmware_id_idx")
                .IsUnique();

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");

            entity.Property(e => e.CreatedAt)
                .HasColumnName("created_at")
                .HasDefaultValueSql("'-infinity'::timestamp with time zone");

            entity.Property(e => e.Description)
                .HasColumnType("character varying")
                .HasColumnName("description");

            entity.Property(e => e.FirmwareId).HasColumnName("firmware_id");

            entity.Property(e => e.ModifiedAt).HasColumnName("modified_at");

            entity.Property(e => e.PublishedAt).HasColumnName("published_at");

            entity.Property(e => e.ReleasedAt).HasColumnName("released_at");

            entity.Property(e => e.Version)
                .IsRequired()
                .HasColumnType("character varying")
                .HasColumnName("version");

            entity.HasOne(d => d.Firmware)
                .WithMany(p => p.DeviceFirmwareVersions)
                .HasForeignKey(d => d.FirmwareId)
                .HasConstraintName("device_firmware_versions_firmware_id_fkey");

            OnConfigurePartial(entity);
        }

        partial void OnConfigurePartial(EntityTypeBuilder<DeviceFirmwareVersions> entity);
    }
}
