using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Demo.Common.Attributes;
using Demo.Common.Enums;
using Demo.Core.Data;
using Demo.Core.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Core.Data.DbContext
{
	public abstract class DbObjectContextBase : Microsoft.EntityFrameworkCore.DbContext, IDbContext
	{
		#region Properties
		public abstract DatabaseProviderEnum Provider { get; }

		public int TranCount { get; set; }

		public abstract string ConnectionString { get; }

		public abstract Catalog Catalog { get; }


		#endregion
		#region Methods
		/// <summary>
		/// Begin transaction method
		/// </summary>
		/// <param name="throwExceptionOnRollbackForInnerTrasactions"></param>
		/// <returns></returns>
		public Demo.Core.Data.IDbTransaction BeginTransaction(bool throwExceptionOnRollbackForInnerTrasactions = true)
		{
			if (this.Database.CurrentTransaction == null)
			{
				this.Database.BeginTransaction();
			}
			this.TranCount++;
			return new ApplicationDbTransaction(this, throwExceptionOnRollbackForInnerTrasactions);
		}


		/// <summary>
		/// Detach an entity from the context
		/// </summary>
		/// <typeparam name="TEntity">Entity type</typeparam>
		/// <param name="entity">Entity</param>
		public virtual void DetachEntity<TEntity>(TEntity entity) where TEntity : BaseEntity
		{
			if (entity == null)
				throw new ArgumentNullException(nameof(entity));

			var entityEntry = this.Entry(entity);
			if (entityEntry == null)
				return;

			//set the entity is not being tracked by the context
			entityEntry.State = EntityState.Detached;
		}


		/// <summary>
		/// Creates a LINQ query for the entity based on a raw SQL query
		/// </summary>
		/// <typeparam name="TEntity">Entity type</typeparam>
		/// <param name="sql">The raw SQL query</param>
		/// <param name="parameters">The values to be assigned to parameters</param>
		/// <returns>An IQueryable representing the raw SQL query</returns>
		public virtual IQueryable<TEntity> EntityFromSql<TEntity>(string sql, params object[] parameters) where TEntity : BaseEntity
		{
			return this.Set<TEntity>().FromSqlRaw(CreateSqlWithParameters(sql, parameters), parameters);
		}

		/// <summary>
		/// Execute Rwa SQL command
		/// </summary>
		/// <param name="sql"></param>
		/// <param name="doNotEnsureTransaction"></param>
		/// <param name="timeout"></param>
		/// <param name="parameters"></param>
		/// <returns></returns>
		public int ExecuteSqlCommand(string sql, bool doNotEnsureTransaction = false, int? timeout = null, params object[] parameters)
		{
			//set specific command timeout
			var previousTimeout = this.Database.GetCommandTimeout();
			this.Database.SetCommandTimeout(timeout);

			var result = 0;
			if (!doNotEnsureTransaction)
			{
				//use with transaction
				using (var transaction = this.Database.BeginTransaction())
				{
					result = this.Database.ExecuteSqlRaw(sql, parameters);
					transaction.Commit();
				}
			}
			else
				result = this.Database.ExecuteSqlRaw(sql, parameters);

			//return previous timeout back
			this.Database.SetCommandTimeout(previousTimeout);

			return result;
		}

		/// <summary>
		/// Generate a script to create all tables for the current model
		/// </summary>
		/// <returns>A SQL script</returns>
		public virtual string GenerateCreateScript()
		{
			return this.Database.GenerateCreateScript();
		}

		/// <summary>
		/// Run migration
		/// </summary>
		public void MigrateDatabase()
		{
			this.Database.Migrate();
		}

		/// <summary>
		/// Creates a LINQ query for the query type based on a raw SQL query
		/// </summary>
		/// <typeparam name="TQuery">Query type</typeparam>
		/// <param name="sql">The raw SQL query</param>
		/// <returns>An IQueryable representing the raw SQL query</returns>
		public virtual IQueryable<TQuery> QueryFromSql<TQuery>(string sql) where TQuery : class
		{
			return base.Set<TQuery>().FromSqlRaw(sql);
		}

		/// <summary>
		/// Creates a LINQ query for the query type based on a raw SQL query
		/// </summary>
		/// <typeparam name="TQuery"></typeparam>
		/// <param name="sql"></param>
		/// <param name="parameters"></param>
		/// <returns></returns>

		public IQueryable<TQuery> QueryFromSql<TQuery>(string sql, params object[] parameters) where TQuery : class
		{
			return base.Set<TQuery>().FromSqlRaw(sql, parameters);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <typeparam name="TEntity"></typeparam>
		/// <param name="entity"></param>
		public void UpdateEntity<TEntity>(TEntity entity) where TEntity : BaseEntity
		{
			var entityEntry = this.Entry(entity);
			if (entityEntry == null)
				throw new ArgumentNullException(nameof(entity));
			this.Entry(entity).State = EntityState.Modified;

		}



		public override int SaveChanges()
		{

			return base.SaveChanges();
		}

		#endregion
		#region Properties
		/// <summary>
		/// Creates a DbSet that can be used to query and save instances of entity
		/// </summary>
		/// <typeparam name="TEntity">Entity type</typeparam>
		/// <returns>A set for the given entity type</returns>
		public virtual new DbSet<TEntity> Set<TEntity>() where TEntity : BaseEntity
		{
			return base.Set<TEntity>();
		}
		#endregion
		#region Utilities



		/// <summary>
		/// Further configuration the model
		/// </summary>
		/// <param name="modelBuilder">The builder being used to construct the model for this context</param>
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			var assemblyList = RetrieveEntityConfigurationAssemblies();
			//dynamically load all entity and query type configurations
			var typeConfigurations = assemblyList.SelectMany(x => x.GetTypes()).Where(type =>
				   (type.BaseType?.IsGenericType ?? false)
					   && (type.BaseType.GetGenericTypeDefinition() == typeof(EntityTypeConfiguration<>)
						   || type.BaseType.GetGenericTypeDefinition() == typeof(QueryTypeConfiguration<>)));

			foreach (var typeConfiguration in typeConfigurations)
			{
				var configuration = (IMappingConfiguration)Activator.CreateInstance(typeConfiguration);
				configuration.Provider = this.Provider;
				configuration.ApplyConfiguration(modelBuilder);
				var entityType = configuration.EntityType;
				
			}

			base.OnModelCreating(modelBuilder);
		}


		private Assembly[] RetrieveEntityConfigurationAssemblies()
		{
			var assemblies = new List<Assembly>();
			ResolveEntityConfigurationAssemblies(assemblies);
			return assemblies.ToArray();
		}

		protected virtual void ResolveEntityConfigurationAssemblies(List<Assembly> assemblies)
		{
			assemblies.Add(Assembly.GetExecutingAssembly());
		}

		/// <summary>
		/// Drop database if exists
		/// </summary>
		public virtual void DropDatabase()
		{
			throw new NoNullAllowedException("Database cannot be deleted!");
		}

		/// <summary>
		/// Modify the input SQL query by adding passed parameters
		/// </summary>
		/// <param name="sql">The raw SQL query</param>
		/// <param name="parameters">The values to be assigned to parameters</param>
		/// <returns>Modified raw SQL query</returns>
		protected virtual string CreateSqlWithParameters(string sql, params object[] parameters)
		{

			//add parameters to sql
			if (parameters != null)
			{
				for (var i = 0; i <= (parameters?.Length ?? 0) - 1; i++)
				{
					if (!(parameters[i] is DbParameter parameter))
						continue;

					sql = $"{sql}{(i > 0 ? "," : string.Empty)} @{parameter.ParameterName}";

					//whether parameter is output
					if (parameter.Direction == ParameterDirection.InputOutput || parameter.Direction == ParameterDirection.Output)
						sql = $"{sql} output";
				}
			}

			return sql;
		}

		/// <summary>
		/// convers SQL value to mapped entity type
		/// </summary>
		/// <param name="schemaName"></param>
		/// <param name="tableName"></param>
		/// <param name="columnName"></param>
		/// <param name="value"></param>
		/// <returns></returns>
		public object ConvertValue(string schemaName, string tableName, string columnName, string value)
		{
			var column = FindColumn(schemaName, tableName, columnName);
			var mapping = column.PropertyMappings.First().TypeMapping;
			var converter = mapping.Converter;
			var clrType = converter?.ProviderClrType ?? mapping.ClrType;

			try
			{
				if (value == null || value.ToLower().Equals("null"))
				{
					return column.DefaultValue;
				}
				else
				{
					if (clrType == typeof(Guid))
					{
						return Guid.Parse(value);
					}
					if (clrType == typeof(string))
					{
						return value;
					}
					if (clrType.IsEnum)
					{
						return Convert.ToInt32(value);
					}
					if (clrType == typeof(Boolean))
					{
						return Convert.ToBoolean(Convert.ToInt16(value));
					}
					if (clrType == typeof(byte[]))
					{
						return new byte[] { };
						//TODO add conversion metod to byte[]
					}
					else
					{
						return Convert.ChangeType(value, clrType);
					}
				}
			}
			catch
			{
				return column.DefaultValue;
			}
		}


		public string GetMappedTableName(string schemaName, string tableName)
		{
			return FindTable(schemaName, tableName).Name;
		}

		public string GetMappedSchemaName(string schemaName, string tableName)
		{
			return FindTable(schemaName, tableName).Schema;
		}

		public string GetMappedColumnName(string schemaName, string tableName, string columnName)
		{
			return FindColumn(schemaName, tableName, columnName).Name;
		}


		private IColumn FindColumn(string schemaName, string tableName, string columnName)
		{
			var table = FindTable(schemaName, tableName);
			var column = table?.FindColumn(columnName);
			if (column == null)
			{
				column = table?.Columns.First(x => x.Name.ToLower() == columnName.ToLower());
			}
			return column;
		}

		private ITable FindTable(string schemaName, string tableName)
		{
			var model = this.Model.GetRelationalModel();
			return model.Tables.Where(x => x.Name.ToLower() == tableName && x.Schema?.ToLower() == schemaName).FirstOrDefault();

		}
		#endregion
	}
}
