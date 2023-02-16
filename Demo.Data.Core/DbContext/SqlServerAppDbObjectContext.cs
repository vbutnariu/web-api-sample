using Demo.Core.Data;
using Demo.Core.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Data;
using System.Data.Common;

using System.Linq;
using System.Reflection;
using Demo.Common.Enums;
using System.Threading.Tasks;
using System.Threading;
using System.Text;
using Microsoft.Data.SqlClient;

namespace Demo.Data.Core.DbContext
{
	public abstract class SqlServerAppDbObjectContext : DbObjectContextBase
	{
		#region Fields
		private readonly string connectionString;
		#endregion
		#region Properties
		public override string ConnectionString => connectionString;
		public override Common.Enums.DatabaseProviderEnum Provider => Common.Enums.DatabaseProviderEnum.SqlServer;
		#endregion
		#region Ctor

		public SqlServerAppDbObjectContext(string connectionString) : base()
		{
			if (string.IsNullOrEmpty(connectionString))
			{
				throw new ArgumentException($"'{nameof(connectionString)}' cannot be null or empty.", nameof(connectionString));
			}

			this.connectionString = connectionString;
		}
		#endregion
		#region Methods
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			LowerCaseMappingExtensions.UseLowercase = false;
			base.OnModelCreating(modelBuilder);
		}

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{

			SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(connectionString);
			// add always trust to server certificate
			builder.TrustServerCertificate = true;

			optionsBuilder.UseSqlServer(builder.ToString(),
				b =>
				{
					b.MigrationsAssembly("NC.Data.Migrations.Sql");
				});

			optionsBuilder.EnableSensitiveDataLogging();

			base.OnConfiguring(optionsBuilder);

		}


		#endregion
	}
}
