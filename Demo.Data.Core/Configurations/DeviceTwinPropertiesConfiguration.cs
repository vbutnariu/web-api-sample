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
    public partial class DeviceTwinPropertiesConfiguration : IEntityTypeConfiguration<DeviceTwinProperties>
    {
        public void Configure(EntityTypeBuilder<DeviceTwinProperties> entity)
        {
            entity.ToTable("device_twin_properties", "shared");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");

            entity.Property(e => e.ConnectionStatus)
                .HasColumnType("character varying")
                .HasColumnName("connection_status");

            entity.Property(e => e.ConnectionStatusUpdateSeqNo)
                .HasColumnType("character varying")
                .HasColumnName("connection_status_update_seq_no");

            entity.Property(e => e.ConnectionStatusUpdatedAt).HasColumnName("connection_status_updated_at");

            entity.Property(e => e.ReportedProperties)
                .HasColumnType("character varying")
                .HasColumnName("reported_properties");

            entity.Property(e => e.ReportedPropertiesCompressionType)
                .HasColumnType("character varying")
                .HasColumnName("reported_properties_compression_type");

            entity.Property(e => e.ReportedPropertiesSeqNo)
                .HasColumnType("character varying")
                .HasColumnName("reported_properties_seq_no");

            entity.Property(e => e.ReportedPropertiesUpdatedAt).HasColumnName("reported_properties_updated_at");

            entity.HasOne(d => d.IdNavigation)
                .WithOne(p => p.DeviceTwinProperties)
                .HasForeignKey<DeviceTwinProperties>(d => d.Id)
                .HasConstraintName("device_twin_properties_id_fkey");

            OnConfigurePartial(entity);
        }

        partial void OnConfigurePartial(EntityTypeBuilder<DeviceTwinProperties> entity);
    }
}
