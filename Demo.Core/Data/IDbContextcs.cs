using Demo.Core.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System;

using System.Threading.Tasks;
using System.Threading;
using Demo.Common.Enums;

namespace Demo.Core.Data
{

	public interface IDbContext : IDisposable
	{
		Common.Enums.DatabaseProviderEnum Provider { get; }
		int TranCount { get; set; }
		public string ConnectionString { get; }
		public Catalog Catalog { get; }

		#region Methods

		/// <summary>
		/// Creates a DbSet that can be used to query and save instances of entity
		/// </summary>
		/// <typeparam name="TEntity">Entity type</typeparam>
		/// <returns>A set for the given entity type</returns>
		DbSet<TEntity> Set<TEntity>() where TEntity : BaseEntity;

		/// <summary>
		/// Saves all changes made in this context to the database
		/// </summary>
		/// <returns>The number of state entries written to the database</returns>
		int SaveChanges();

		/// <summary>
		/// Generate a script to create all tables for the current model
		/// </summary>
		/// <returns>A SQL script</returns>
		string GenerateCreateScript();

		/// <summary>
		/// Get value for specific column table and schema
		/// </summary>
		/// <param name="schemaName"></param>
		/// <param name="tableName"></param>
		/// <param name="columnName"></param>
		/// <param name="value"></param>
		/// <returns></returns>
		object ConvertValue(string schemaName, string tableName, string columnName, string value);


		/// <summary>
		/// get the mapped table name with the same casing as defined in the map class
		/// </summary>
		/// <param name="schemaName"></param>
		/// <param name="tableName"></param>
		/// <returns></returns>
		string GetMappedTableName(string schemaName, string tableName);

		/// <summary>
		/// get the mapped schema name with the same casing as defined in the map class
		/// </summary>
		/// <param name="schemaName"></param>
		/// <param name="tableName"></param>
		/// <returns></returns>
		string GetMappedSchemaName(string schemaName, string tableName);

		/// <summary>
		/// get the mapped column name with the same casing as defined in the map class
		/// </summary>
		/// <param name="schemaName"></param>
		/// <param name="tableName"></param>
		/// <returns></returns>
		string GetMappedColumnName(string schemaName, string tableName, string columnName);
		/// <summary>
		/// Creates a LINQ query for the query type based on a raw SQL query
		/// </summary>
		/// <typeparam name="TQuery">Query type</typeparam>
		/// <param name="sql">The raw SQL query</param>
		/// <returns>An IQueryable representing the raw SQL query</returns>
		IQueryable<TQuery> QueryFromSql<TQuery>(string sql) where TQuery : class;


		/// <summary>
		/// Creates a LINQ query for the query type based on a raw SQL query
		/// </summary>
		/// <typeparam name="TQuery">Query type</typeparam>
		/// <param name="sql">The raw SQL query</param>
		/// <param name="parameters">The values to be assigned to parameters</param>
		/// <returns>An IQueryable representing the raw SQL query</returns>
		IQueryable<TQuery> QueryFromSql<TQuery>(string sql, params object[] parameters) where TQuery : class;
		void DropDatabase();

		/// <summary>
		/// Creates a LINQ query for the entity based on a raw SQL query
		/// </summary>
		/// <typeparam name="TEntity">Entity type</typeparam>
		/// <param name="sql">The raw SQL query</param>
		/// <param name="parameters">The values to be assigned to parameters</param>
		/// <returns>An IQueryable representing the raw SQL query</returns>
		IQueryable<TEntity> EntityFromSql<TEntity>(string sql, params object[] parameters) where TEntity : BaseEntity;

		/// <summary>
		/// Executes the given SQL against the database
		/// </summary>
		/// <param name="sql">The SQL to execute</param>
		/// <param name="doNotEnsureTransaction">true - the transaction creation is not ensured; false - the transaction creation is ensured.</param>
		/// <param name="timeout">The timeout to use for command. Note that the command timeout is distinct from the connection timeout, which is commonly set on the database connection string</param>
		/// <param name="parameters">Parameters to use with the SQL</param>
		/// <returns>The number of rows affected</returns>
		int ExecuteSqlCommand(string sql, bool doNotEnsureTransaction = false, int? timeout = null, params object[] parameters);

		/// <summary>
		/// Detach an entity from the context
		/// </summary>
		/// <typeparam name="TEntity">Entity type</typeparam>
		/// <param name="entity">Entity</param>
		void DetachEntity<TEntity>(TEntity entity) where TEntity : BaseEntity;
		void UpdateEntity<TEntity>(TEntity entity) where TEntity : BaseEntity;
		void MigrateDatabase();
		IDbTransaction BeginTransaction(bool throwExceptionOnRollbackForInnerTrasactions);
		Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default);
		Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
		#endregion
	}

}
