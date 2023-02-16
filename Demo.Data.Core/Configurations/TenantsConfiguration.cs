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
    public partial class TenantsConfiguration : IEntityTypeConfiguration<Tenants>
    {
        public void Configure(EntityTypeBuilder<Tenants> entity)
        {
            entity.ToTable("tenants", "shared");

            entity.HasIndex(e => e.Name, "tenants_name_idx")
                .IsUnique();

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");

            entity.Property(e => e.CreatedAt).HasColumnName("created_at");

            entity.Property(e => e.Description)
                .HasColumnType("character varying")
                .HasColumnName("description");

            entity.Property(e => e.ModifiedAt).HasColumnName("modified_at");

            entity.Property(e => e.Name)
                .IsRequired()
                .HasColumnType("character varying")
                .HasColumnName("name");

            entity.Property(e => e.ProfileImageId)
                .HasColumnType("character varying")
                .HasColumnName("profile_image_id");

            entity.Property(e => e.ProfileImageUri)
                .HasColumnType("character varying")
                .HasColumnName("profile_image_uri");

            OnConfigurePartial(entity);
        }

        partial void OnConfigurePartial(EntityTypeBuilder<Tenants> entity);
    }
}
