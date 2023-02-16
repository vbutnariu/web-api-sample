using Demo.Common.Exceptions.WEBApi;
using Demo.Core.Data;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using Npgsql.Bulk;
using System.Linq;
using EFCore.BulkExtensions;
using System.Threading.Tasks;
using System.Linq.Expressions;
using Npgsql;
using System.Data;
using System.Threading;
using System.Reflection;
using Microsoft.EntityFrameworkCore.Metadata;
using Demo.Common.Attributes;

namespace Demo.Data.Core
{
	/// <summary>
	/// Entity Framework repository
	/// </summary>
	public partial class EfRepository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
	{
		#region Fields



		private DbSet<TEntity> entities;

		private static string uniqueTablePrefix = Guid.NewGuid().ToString().Replace("-", "_");
		private static int tablesCounter = 0;

		private static MethodInfo CompleteMethodInfo = typeof(NpgsqlBinaryImporter).GetMethod("Complete");
		private readonly IDbContextFactory dbContextFactory;
		private IDbContext currentContext = null;
		#endregion

		#region Ctor

		public EfRepository(Demo.Core.Data.IDbContextFactory factory)
		{
			this.dbContextFactory = factory;
		}
		#endregion

		#region Utilities

		/// <summary>
		/// Rollback of entity changes and return full error message
		/// </summary>
		/// <param name="exception">Exception</param>
		/// <returns>Error message</returns>
		protected string GetFullErrorText(DbUpdateException exception)
		{

			//Commented out all entity changes
			////rollback entity changes

			return exception.ToString();
		}

		private void EnsureCurrentContext()
		{
			if (currentContext is null)
			{
				var catalogAttribute = typeof(TEntity).GetCustomAttribute<CatalogAttribute>();
				if (catalogAttribute != null)
				{
					currentContext = dbContextFactory.GetDbContext(catalogAttribute.Catalog);
				}
				else
				{
					currentContext = dbContextFactory.GetDefaultDbContext();
				}
			}
		}

		#endregion

		#region Methods

		/// <summary>
		/// Get entity by identifier
		/// </summary>
		/// <param name="id">Identifier</param>
		/// <returns>Entity</returns>
		public virtual TEntity GetById(object id)
		{
			return Set.Find(id);

		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="keyValues"></param>
		/// <returns></returns>
		public TEntity GetById(params object[] keyValues)
		{
			return Set.Find(keyValues);
		}

		/// <summary>
		/// Insert entity
		/// </summary>
		/// <param name="entity">Entity</param>
		public virtual void Insert(TEntity entity)
		{
			if (entity == null)
				throw new ArgumentNullException(nameof(entity));

			try
			{
				Set.Add(entity);
				Context.SaveChanges();
			}
			catch (DbUpdateException exception)
			{
				//ensure that the detailed error text is saved in the Log
				throw new DbUpdateException(GetFullErrorText(exception), exception);
			}
		}

		/// <summary>
		/// Insert entities
		/// </summary>
		/// <param name="entities">Entities</param>
		public virtual void Insert(IEnumerable<TEntity> entities)
		{
			if (entities == null)
				throw new ArgumentNullException(nameof(entities));

			try
			{
				Set.AddRange(entities);
				Context.SaveChanges();
			}
			catch (DbUpdateException exception)
			{
				//ensure that the detailed error text is saved in the Log
				throw new DbUpdateException(GetFullErrorText(exception), exception);
			}
		}


		/// <summary>
		/// Update entity
		/// </summary>
		/// <param name="entity">Entity</param>
		public virtual void Update(TEntity entity, object key)
		{
			object[] myObjArray = { key };
			Update(entity, myObjArray);
		}



		/// <summary>
		/// Update entity
		/// </summary>
		/// <param name="entity">Entity</param>
		public virtual void Update(TEntity entity, params object[] compositePrimaryKeys)
		{
			try
			{
				if (entity == null)
					throw new ArgumentNullException("entity");

				var existingEntity = this.Set.Find(compositePrimaryKeys);

				if (existingEntity == null)
					throw new ArgumentException("Entity does not exist in the DB");

				(this.Context as Microsoft.EntityFrameworkCore.DbContext).Entry(existingEntity).CurrentValues.SetValues(entity);

				this.Context.SaveChanges();

				if (entity is ITrackableEntity trackableEntity)
				{
					trackableEntity.ModifiedBy = (existingEntity as ITrackableEntity).ModifiedBy;
					trackableEntity.ModifiedOn = (existingEntity as ITrackableEntity).ModifiedOn;
				}
			}
			catch (DbUpdateException exception)
			{
				//ensure that the detailed error text is saved in the Log
				throw new DbUpdateException(GetFullErrorText(exception), exception);
			}
		}

		/// <summary>
		/// Delete entity
		/// </summary>
		/// <param name="entity">Entity</param>
		public virtual void Delete(TEntity entity)
		{
			if (entity == null)
				throw new ArgumentNullException(nameof(entity));

			try
			{
				Set.Remove(entity);
				Context.SaveChanges();
			}
			catch (Exception ex)
			{
				throw new ItemCannotBeDeletedException("Item cannot be deleted", ex);
			}
		}

		/// <summary>
		/// Delete entities
		/// </summary>
		/// <param name="entities">Entities</param>
		public virtual void Delete(IEnumerable<TEntity> entities)
		{
			if (entities == null)
				throw new ArgumentNullException(nameof(entities));

			try
			{
				Set.RemoveRange(entities);
				Context.SaveChanges();
			}
			catch (Exception ex)
			{
				throw new ItemCannotBeDeletedException("Item cannot be deleted", ex);
			}
		}

		public void Delete(Guid entityId)
		{
			var entity = this.GetById(entityId);

			if (entity == null)
				throw new ArgumentNullException(nameof(entityId));

			try
			{
				Set.Remove(entity);
				Context.SaveChanges();
			}
			catch (Exception ex)
			{
				throw new ItemCannotBeDeletedException("Item cannot be deleted", ex);
			}
		}



		public void Upsert(TEntity entity, object key)
		{
			try
			{
				if (entity == null)
					throw new ArgumentNullException("entity");

				var existingEntity = this.Set.Find(key);

				if (existingEntity == null)
				{
					this.Set.Add(entity);
				}
				else
				{
					Set.Update(entity);
				}

				this.Context.SaveChanges();
			}
			catch (DbUpdateException exception)
			{
				//ensure that the detailed error text is saved in the Log
				throw new DbUpdateException(GetFullErrorText(exception), exception);
			}
		}


		public Demo.Core.Data.IDbTransaction CreateTransaction(bool throwExceptionOnRollbackForInnerTrasactions)
		{
			return Context.BeginTransaction(throwExceptionOnRollbackForInnerTrasactions);
		}

		public Demo.Core.Data.IDbTransaction CreateTransaction()
		{
			return Context.BeginTransaction(true);
		}

		public void LoadRelatedEntitiesFiltered<TRelatedEntity>(TEntity entity, System.Linq.Expressions.Expression<Func<TEntity, ICollection<TRelatedEntity>>> navigationProperty, System.Linq.Expressions.Expression<Func<TRelatedEntity, bool>> predicate) where TRelatedEntity : class
		{
			throw new NotImplementedException();
		}

		public void LoadRelatedEntity<TRelatedEntity>(TEntity entity, System.Linq.Expressions.Expression<Func<TEntity, TRelatedEntity>> navigationProperty) where TRelatedEntity : class
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// Insert entities
		/// </summary>
		/// <param name="entities">Entities</param>
		public virtual void BulkInsert(IList<TEntity> entities)
		{
			if (entities == null)
				throw new ArgumentNullException(nameof(entities));

			try
			{
				if (Context.Provider == Common.Enums.DatabaseProviderEnum.SqlServer)
				{
					(Context as Microsoft.EntityFrameworkCore.DbContext).AddRange(entities);
					Context.SaveChanges();
				}
				else if (Context.Provider == Common.Enums.DatabaseProviderEnum.Postgres)
				{
					(Context as Microsoft.EntityFrameworkCore.DbContext).AddRange(entities);
					Context.SaveChanges();
				}
			}
			catch (DbUpdateException exception)
			{
				//ensure that the detailed error text is saved in the Log
				throw new DbUpdateException(GetFullErrorText(exception), exception);
			}
		}

		public virtual void BulkUpdate(IList<TEntity> entities)
		{

			if (entities == null)
				throw new ArgumentNullException(nameof(entities));

			try
			{
				if (Context.Provider == Common.Enums.DatabaseProviderEnum.SqlServer)
				{
					(Context as Microsoft.EntityFrameworkCore.DbContext).BulkUpdate(entities);
				}
				else if (Context.Provider == Common.Enums.DatabaseProviderEnum.Postgres)
				{
					var uploader = new NpgsqlBulkUploader(Context as Microsoft.EntityFrameworkCore.DbContext);

					uploader.Update(entities);
				}
			}
			catch (DbUpdateException exception)
			{
				//ensure that the detailed error text is saved in the Log
				throw new DbUpdateException(GetFullErrorText(exception), exception);
			}

		}

		public virtual void BulkUpsert(IList<TEntity> entities)
		{
			if (entities == null)
				throw new ArgumentNullException(nameof(entities));

			try
			{
				(Context as Microsoft.EntityFrameworkCore.DbContext).BulkInsertOrUpdate(entities);
			}
			catch (DbUpdateException exception)
			{
				//ensure that the detailed error text is saved in the Log
				throw new DbUpdateException(GetFullErrorText(exception), exception);
			}

		}

		public virtual void BulkDelete(IList<TEntity> entities)
		{
			if (entities == null)
				throw new ArgumentNullException(nameof(entities));

			try
			{
				if (Context.Provider == Common.Enums.DatabaseProviderEnum.SqlServer)
				{
					(Context as Microsoft.EntityFrameworkCore.DbContext).BulkDelete(entities, options => options.BatchSize = 100);
				}
				else if (Context.Provider == Common.Enums.DatabaseProviderEnum.Postgres)
				{
					Delete(entities);

				}
			}
			catch (DbUpdateException exception)
			{
				//ensure that the detailed error text is saved in the Log
				throw new DbUpdateException(GetFullErrorText(exception), exception);
			}
		}

		#region PostgreSql Bulk Delete

		public IDbContextTransaction EnsureOrStartTransaction(
		   Microsoft.EntityFrameworkCore.DbContext Context, IsolationLevel isolation)
		{
			if (Context.Database.CurrentTransaction == null)
			{
				if (System.Transactions.Transaction.Current != null)
				{
					return null;
				}

				return Context.Database.BeginTransaction(isolation);
			}

			return null;
		}

		private bool EnsureConnected(NpgsqlConnection conn)
		{
			if (conn.State != System.Data.ConnectionState.Open)
			{
				conn.Open();
				return true;
			}
			return false;
		}


		/// <summary>
		/// Get unique object name using user-defined prefix.
		/// </summary>
		/// <param name="prefix">Prefix.</param>
		/// <returns>Unique name.</returns>
		internal static string GetUniqueName(string prefix)
		{
			var counter = Interlocked.Increment(ref tablesCounter);
			return $"{prefix}_{uniqueTablePrefix}_{counter}";
		}

		#endregion

		public virtual void BulkSync(IList<TEntity> entities)
		{
			if (entities == null)
				throw new ArgumentNullException(nameof(entities));

			try
			{
				(Context as Microsoft.EntityFrameworkCore.DbContext).BulkInsertOrUpdateOrDelete(entities);
			}
			catch (DbUpdateException exception)
			{
				//ensure that the detailed error text is saved in the Log
				throw new DbUpdateException(GetFullErrorText(exception), exception);
			}
		}

		public async Task<IList<TEntity>> GetAllAsync(Func<IQueryable<TEntity>, IQueryable<TEntity>> func = null)
		{
			var query = Table;
			query = func != null ? func(query) : query;
			return await query.ToListAsync();
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public async Task<TEntity> GetByIdAsync(object id)
		{
			return await Set.FindAsync(id);
		}

		/// <summary>
		/// Get by id async
		/// 
		/// </summary>
		/// <param name="keyValues"></param>
		/// <returns></returns>
		public async Task<TEntity> GetByIdAsync(params object[] keyValues)
		{
			return await Set.FindAsync(keyValues);
		}

		public async Task InsertAsync(TEntity entity)
		{
			if (entity == null)
				throw new ArgumentNullException(nameof(entity));

			try
			{
				await Set.AddAsync(entity);
				await Context.SaveChangesAsync();
			}
			catch (DbUpdateException exception)
			{
				//ensure that the detailed error text is saved in the Log
				throw new DbUpdateException(GetFullErrorText(exception), exception);
			}
		}

		public async Task InsertAsync(IList<TEntity> entities)
		{
			if (entities == null)
				throw new ArgumentNullException(nameof(entities));

			try
			{
				Set.AddRange(entities);
				await Context.SaveChangesAsync();
			}
			catch (DbUpdateException exception)
			{
				//ensure that the detailed error text is saved in the Log
				throw new DbUpdateException(GetFullErrorText(exception), exception);
			}
		}

		public async Task UpdateAsync(TEntity entity, object key)
		{
			await UpdateAsync(entity, key);
		}

		/// <summary>
		/// Update entity
		/// </summary>
		/// <param name="entity">Entity</param>
		public virtual async Task UpdateAsync(TEntity entity, params object[] compositePrimaryKeys)
		{
			try
			{
				if (entity == null)
					throw new ArgumentNullException("entity");

				var existingEntity = await this.Set.FindAsync(compositePrimaryKeys);

				if (existingEntity == null)
					throw new ArgumentException("Entity does not exist in the DB");

				(this.Context as Microsoft.EntityFrameworkCore.DbContext).Entry(existingEntity).CurrentValues.SetValues(entity);

				await this.Context.SaveChangesAsync();

				if (entity is ITrackableEntity trackableEntity)
				{
					trackableEntity.ModifiedBy = (existingEntity as ITrackableEntity).ModifiedBy;
					trackableEntity.ModifiedOn = (existingEntity as ITrackableEntity).ModifiedOn;
				}
			}
			catch (DbUpdateException exception)
			{
				//ensure that the detailed error text is saved in the Log
				throw new DbUpdateException(GetFullErrorText(exception), exception);
			}
		}

		public Task DeleteAsync(TEntity entity)
		{
			throw new NotImplementedException();
		}

		public Task DeleteAsync(IList<TEntity> entities)
		{
			throw new NotImplementedException();
		}

		public Task<int> DeleteAsync(Expression<Func<TEntity, bool>> predicate)
		{
			throw new NotImplementedException();
		}

		public Task<TEntity> LoadOriginalCopyAsync(TEntity entity)
		{
			throw new NotImplementedException();
		}




		#endregion

		#region Properties

		/// <summary>
		/// Gets a table
		/// </summary>
		public virtual IQueryable<TEntity> Table => Set;

		/// <summary>
		/// Gets a table with "no tracking" enabled (EF feature) Use it only when you load record(s) only for read-only operations
		/// </summary>
		public virtual IQueryable<TEntity> TableNoTracking => Set.AsNoTracking();

		/// <summary>
		/// Gets an entity set
		/// </summary>
		public virtual DbSet<TEntity> Set
		{
			get
			{
				if (entities == null)
					entities = Context.Set<TEntity>();

				return entities;
			}
		}



		public IDbContext Context { get { EnsureCurrentContext(); return currentContext; } }






		#endregion
	}
}
