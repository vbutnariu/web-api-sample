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
    public partial class VsfilesObjectTypeIdMappingEntriesConfiguration : IEntityTypeConfiguration<VsfilesObjectTypeIdMappingEntries>
    {
        public void Configure(EntityTypeBuilder<VsfilesObjectTypeIdMappingEntries> entity)
        {
            entity.HasKey(e => new { e.ProductInformationFileType, e.VsfilesObjectTypeId })
                .HasName("vsfiles_object_type_id_mapping_entries_pkey");

            entity.ToTable("vsfiles_object_type_id_mapping_entries", "shared");

            entity.Property(e => e.ProductInformationFileType)
                .HasColumnType("character varying")
                .HasColumnName("product_information_file_type");

            entity.Property(e => e.VsfilesObjectTypeId).HasColumnName("vsfiles_object_type_id");

            OnConfigurePartial(entity);
        }

        partial void OnConfigurePartial(EntityTypeBuilder<VsfilesObjectTypeIdMappingEntries> entity);
    }
}
