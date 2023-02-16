using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Demo.Core.Data
{
	public partial interface IRepository<T> where T : BaseEntity
	{

		IDbContext Context { get; }

		/// <summary>
		/// Get entity by identifier
		/// </summary>
		/// <param name="id">Identifier</param>
		/// <returns>Entity</returns>
		T GetById(object id);
		/// <summary>
		/// Get entity by identifier for keys with multiple columns
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		T GetById(params object[] keyValues);

		/// <summary>
		/// Insert entity
		/// </summary>
		/// <param name="entity">Entity</param>
		void Insert(T entity);

		/// <summary>
		/// Insert entities
		/// </summary>
		/// <param name="entities">Entities</param>
		void Insert(IEnumerable<T> entities);

		/// <summary>
		/// bulk insert entities
		/// </summary>
		/// <param name="entities"></param>
		void BulkInsert(IList<T> entities);

		/// <summary>
		/// 
		/// </summary>
		/// <param name="entity"></param>
		void Upsert(T entity, object key);

		/// <summary>
		/// 
		/// </summary>
		/// <param name="entities"></param>
		void BulkUpsert(IList<T> entities);

		/// <summary>
		/// Update entity
		/// </summary>
		/// <param name="entity">Entity</param>
		void Update(T entity, object key);

		/// <summary>
		/// Update entity with composite primary keys
		/// </summary>
		/// <param name="entity">Entity</param>
		void Update(T entity, params object[] compositePrimaryKeys);

		

		/// <summary>
		/// 
		/// </summary>
		/// <param name="entities"></param>
		void BulkUpdate(IList<T> entities);

		/// <summary>
		/// Delete entity
		/// </summary>
		/// <param name="entity">Entity</param>
		void Delete(T entity);

		/// <summary>
		/// Delete entity
		/// </summary>
		/// <param name="entity">Entity Id</param>
		void Delete(Guid entityId);

		/// <summary>
		/// Delete entities
		/// </summary>
		/// <param name="entities">Entities</param>
		void Delete(IEnumerable<T> entities);


		/// <summary>
		/// 
		/// </summary>
		/// <param name="entities"></param>
		void BulkDelete(IList<T> entities);



		/// <summary>
		/// Get all entity entries
		/// </summary>
		/// <param name="func">Function to select entries</param>
		/// <param name="getCacheKey">Function to get a cache key; pass null to don't cache; return null from this function to use the default key</param>
		/// <param name="includeDeleted">Whether to include deleted items (applies only to <see cref="Nop.Core.Domain.Common.ISoftDeletedEntity"/> entities)</param>
		/// <returns>
		/// A task that represents the asynchronous operation
		/// The task result contains the entity entries
		/// </returns>
		Task<IList<T>> GetAllAsync(Func<IQueryable<T>, IQueryable<T>> func = null);

		/// <summary>
		/// 
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public Task<T> GetByIdAsync(object id);

		/// <summary>
		/// 
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public Task<T> GetByIdAsync(params object[] keyValues);

		/// <summary>
		/// Insert the entity entry
		/// </summary>
		/// <param name="entity">Entity entry</param>
		/// <param name="publishEvent">Whether to publish event notification</param>
		/// <returns>A task that represents the asynchronous operation</returns>
		Task InsertAsync(T entity);

		/// <summary>
		/// Insert entity entries
		/// </summary>
		/// <param name="entities">Entity entries</param>
		/// <returns>A task that represents the asynchronous operation</returns>
		Task InsertAsync(IList<T> entities);

		/// <summary>
		/// Update the entity entry
		/// </summary>
		/// <param name="entity">Entity entry</param>
		/// <returns>A task that represents the asynchronous operation</returns>
		Task UpdateAsync(T entity, object key);


		/// <summary>
		/// Delete the entity entry
		/// </summary>
		/// <param name="entity">Entity entry</param>
		/// <returns>A task that represents the asynchronous operation</returns>
		Task DeleteAsync(T entity);

		/// <summary>
		/// Delete entity entries
		/// </summary>
		/// <param name="entities">Entity entries</param>
		/// <returns>A task that represents the asynchronous operation</returns>
		Task DeleteAsync(IList<T> entities);

		/// <summary>
		/// Delete entity entries by the passed predicate
		/// </summary>
		/// <param name="predicate">A function to test each element for a condition</param>
		/// <returns>
		/// A task that represents the asynchronous operation
		/// The task result contains the number of deleted records
		/// </returns>
		Task<int> DeleteAsync(Expression<Func<T, bool>> predicate);

		/// <summary>
		/// Loads the original copy of the entity entry
		/// </summary>
		/// <param name="entity">Entity entry</param>
		/// <returns>
		/// A task that represents the asynchronous operation
		/// The task result contains the copy of the passed entity entry
		/// </returns>
		Task<T> LoadOriginalCopyAsync(T entity);



		/// <summary>
		/// Gets a table
		/// </summary>
		IQueryable<T> Table { get; }

		/// <summary>
		/// Gets a table with "no tracking" enabled (EF feature) Use it only when you load record(s) only for read-only operations
		/// </summary>
		IQueryable<T> TableNoTracking { get; }

		/// <summary>
		/// DB set
		/// </summary>
		/// <param name="throwExceptionOnRollbackForInnerTrasactions"></param>
		/// <returns></returns>
		DbSet<T> Set { get; }
		IDbTransaction CreateTransaction(bool throwExceptionOnRollbackForInnerTrasactions);

		IDbTransaction CreateTransaction();

		void LoadRelatedEntitiesFiltered<TRelatedEntity>(T entity, Expression<Func<T, ICollection<TRelatedEntity>>> navigationProperty, Expression<Func<TRelatedEntity, bool>> predicate) where TRelatedEntity : class;
		void LoadRelatedEntity<TRelatedEntity>(T entity, Expression<Func<T, TRelatedEntity>> navigationProperty) where TRelatedEntity : class;
	}

}
