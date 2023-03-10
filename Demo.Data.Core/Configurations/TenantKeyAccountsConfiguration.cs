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
    public partial class TenantKeyAccountsConfiguration : IEntityTypeConfiguration<TenantKeyAccounts>
    {
        public void Configure(EntityTypeBuilder<TenantKeyAccounts> entity)
        {
            entity.ToTable("tenant_key_accounts", "shared");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");

            entity.Property(e => e.EmailAddress)
                .IsRequired()
                .HasColumnType("character varying")
                .HasColumnName("email_address");

            entity.Property(e => e.FirstName)
                .IsRequired()
                .HasColumnType("character varying")
                .HasColumnName("first_name");

            entity.Property(e => e.LastName)
                .IsRequired()
                .HasColumnType("character varying")
                .HasColumnName("last_name");

            entity.Property(e => e.PhoneNumber)
                .HasColumnType("character varying")
                .HasColumnName("phone_number");

            entity.Property(e => e.Region)
                .IsRequired()
                .HasColumnType("character varying")
                .HasColumnName("region");

            entity.Property(e => e.TimeZone)
                .IsRequired()
                .HasColumnType("character varying")
                .HasColumnName("time_zone");

            OnConfigurePartial(entity);
        }

        partial void OnConfigurePartial(EntityTypeBuilder<TenantKeyAccounts> entity);
    }
}
