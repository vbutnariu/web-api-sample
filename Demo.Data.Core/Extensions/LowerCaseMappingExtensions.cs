using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Demo.Core.Infrastructure;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.EntityFrameworkCore
{
    public static class LowerCaseMappingExtensions
    {
       
        public static bool UseLowercase { get; set; }

        static LowerCaseMappingExtensions()
        {
          
        }
        public static PropertyBuilder<TProperty> HasColumnNameCs<TProperty>(this PropertyBuilder<TProperty> propertyBuilder, string name)
        {
            return propertyBuilder.HasColumnName(UseLowercase ? name.ToLower() : name);
        }

        public static EntityTypeBuilder<TEntity> ToTableCs<TEntity>(this EntityTypeBuilder<TEntity> entityTypeBuilder, string name, string schema) where TEntity : class
        {
            return entityTypeBuilder.ToTable(UseLowercase ? name.ToLower() : name, UseLowercase ? schema.ToLower() : schema);
        }

        public static EntityTypeBuilder<TEntity> ToTableCs<TEntity>(this EntityTypeBuilder<TEntity> entityTypeBuilder, string name) where TEntity : class
        {
            return entityTypeBuilder.ToTable(UseLowercase ? name.ToLower() : name);
        }
    }

}
