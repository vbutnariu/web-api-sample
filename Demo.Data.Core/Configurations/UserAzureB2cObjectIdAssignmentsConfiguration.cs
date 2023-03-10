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
    public partial class UserAzureB2cObjectIdAssignmentsConfiguration : IEntityTypeConfiguration<UserAzureB2cObjectIdAssignments>
    {
        public void Configure(EntityTypeBuilder<UserAzureB2cObjectIdAssignments> entity)
        {
            entity.HasKey(e => e.UserId)
                .HasName("user_azure_b2c_object_id_assignments_pkey");

            entity.ToTable("user_azure_b2c_object_id_assignments", "shared");

            entity.HasIndex(e => e.ObjectId, "user_azure_b2c_object_id_assignments_object_id_idx")
                .IsUnique();

            entity.Property(e => e.UserId)
                .HasColumnType("character varying")
                .HasColumnName("user_id");

            entity.Property(e => e.ObjectId)
                .IsRequired()
                .HasColumnType("character varying")
                .HasColumnName("object_id");

            entity.HasOne(d => d.User)
                .WithOne(p => p.UserAzureB2cObjectIdAssignments)
                .HasForeignKey<UserAzureB2cObjectIdAssignments>(d => d.UserId)
                .HasConstraintName("user_azure_b2c_object_id_assignments_user_id_fkey");

            OnConfigurePartial(entity);
        }

        partial void OnConfigurePartial(EntityTypeBuilder<UserAzureB2cObjectIdAssignments> entity);
    }
}
