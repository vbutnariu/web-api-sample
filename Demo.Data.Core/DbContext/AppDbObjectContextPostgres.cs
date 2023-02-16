using Demo.Core.Data;
using Demo.Core.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Data;
using System.Data.Common;

using System.Linq;
using System.Reflection;
using Demo.Common.Enums;

namespace Demo.Core.Data.DbContext
{
    public abstract class AppDbObjectContextPostgres : DbObjectContextBase
    {
        #region Static Constructor
        static AppDbObjectContextPostgres()
        {
            /// Added temporary fix for treating local date times as UTC
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        } 
        #endregion
        #region Fields
        private readonly string connectionString;
        #endregion
        #region Properties
        public override string ConnectionString => connectionString;
        public override Common.Enums.DatabaseProviderEnum Provider => Common.Enums.DatabaseProviderEnum.Postgres; 
        #endregion
        #region Ctor

        public AppDbObjectContextPostgres(string connectionStringOrKey) : base()
        {
            this.connectionString = connectionStringOrKey;
           
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            LowerCaseMappingExtensions.UseLowercase = true;
            base.OnModelCreating(modelBuilder);
        }

        #endregion
        #region Methods

        public override void DropDatabase()
        {
            this.Database.EnsureDeleted();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(connectionString, b => b.MigrationsAssembly("NC.Data.Migrations"));
            
            base.OnConfiguring(optionsBuilder);
           
            
        } 
        #endregion
    }
}
