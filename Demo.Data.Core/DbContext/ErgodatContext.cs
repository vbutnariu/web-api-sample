using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Demo.Common.Enums;
using Demo.Core.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Data.Core.DbContext
{
	public class ErgodatContext : DbObjectContextBase
	{

		#region Fields
		private readonly string connectionString;
		private readonly DatabaseProviderEnum provider = DatabaseProviderEnum.SqlServer;

		
		#endregion
		#region Properties
		public override string ConnectionString => connectionString;

		public override DatabaseProviderEnum Provider => provider;

		public override Catalog Catalog => Catalog.Ergonomed;



		#endregion
		#region Ctor

		public ErgodatContext(string connectionString) : base()
		{
			if (string.IsNullOrEmpty(connectionString))
			{
				throw new ArgumentException($"'{nameof(connectionString)}' cannot be null or empty.", nameof(connectionString));
			}

			this.connectionString = connectionString;
		}

		protected override void ResolveEntityConfigurationAssemblies(List<Assembly> assemblies)
		{
			assemblies.Add(Assembly.GetExecutingAssembly());
		}
		#endregion
		#region Methods
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
		}

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{

			SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(connectionString);
			// add always trust to server certificate
			builder.TrustServerCertificate = true;

			// if initial catalog is set in the connection string then we do not oerride with the specified catalog 
			if (string.IsNullOrEmpty(builder.InitialCatalog))
			{
				builder.InitialCatalog = this.Catalog.GetCatalogName();
			}

			optionsBuilder.UseSqlServer(builder.ToString(),
				b =>
				{
					//b.MigrationsAssembly("Pm.Data.Migrations");
					//b.MigrationsHistoryTable("MigrationHistory", "main");
				});

			optionsBuilder.EnableSensitiveDataLogging();

			base.OnConfiguring(optionsBuilder);

		}
		
		#endregion
	}
}
