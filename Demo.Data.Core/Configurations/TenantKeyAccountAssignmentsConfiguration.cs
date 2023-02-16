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
    public partial class TenantKeyAccountAssignmentsConfiguration : IEntityTypeConfiguration<TenantKeyAccountAssignments>
    {
        public void Configure(EntityTypeBuilder<TenantKeyAccountAssignments> entity)
        {
            entity.ToTable("tenant_key_account_assignments", "shared");

            entity.HasIndex(e => e.KeyAccountId, "tenant_key_account_assignments_key_account_id_idx")
                .IsUnique();

            entity.HasIndex(e => new { e.TenantId, e.KeyAccountId }, "tenant_key_account_assignments_tenant_id_key_account_id_idx")
                .IsUnique();

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");

            entity.Property(e => e.KeyAccountId).HasColumnName("key_account_id");

            entity.Property(e => e.TenantId).HasColumnName("tenant_id");

            entity.HasOne(d => d.KeyAccount)
                .WithOne(p => p.TenantKeyAccountAssignments)
                .HasForeignKey<TenantKeyAccountAssignments>(d => d.KeyAccountId)
                .HasConstraintName("tenant_key_account_assignments_key_account_id_fkey");

            entity.HasOne(d => d.Tenant)
                .WithMany(p => p.TenantKeyAccountAssignments)
                .HasForeignKey(d => d.TenantId)
                .HasConstraintName("tenant_key_account_assignments_tenant_id_fkey");

            OnConfigurePartial(entity);
        }

        partial void OnConfigurePartial(EntityTypeBuilder<TenantKeyAccountAssignments> entity);
    }
}
