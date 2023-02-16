using Demo.Core.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Demo.Common.Enums;
using System;

namespace Demo.Data.Core
{
	/// <summary>
	/// Represents base entity mapping configuration
	/// </summary>
	/// <typeparam name="TEntity">Entity type</typeparam>
	public abstract class EntityTypeConfiguration<TEntity> : IMappingConfiguration, IEntityTypeConfiguration<TEntity> where TEntity : BaseEntity
	{


		#region Utilities

		/// <summary>
		/// Developers can override this method in custom partial classes in order to add some custom configuration code
		/// </summary>
		/// <param name="builder">The builder to be used to configure the entity</param>
		protected virtual void PostConfigure(EntityTypeBuilder<TEntity> builder, DatabaseProviderEnum provider)
		{
			switch (provider)
			{
				case DatabaseProviderEnum.SqlServer:
					PostConfigureSqlServer(builder);
					break;
				case DatabaseProviderEnum.Postgres:
					PostConfigurePostgres(builder);
					break;
				default:
					throw new NotImplementedException();

			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="builder"></param>
		protected virtual void PostConfigurePostgres(EntityTypeBuilder<TEntity> builder)
		{
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="builder"></param>
		protected virtual void PostConfigureSqlServer(EntityTypeBuilder<TEntity> builder)
		{
		}


		#endregion

		#region Methods

		/// <summary>
		/// Configures the entity
		/// </summary>
		/// <param name="entity">The builder to be used to configure the entity</param>
		public virtual void Configure(EntityTypeBuilder<TEntity> entity)
		{
			//add custom configuration
			this.PostConfigure(entity, this.Provider);
		}

		public DatabaseProviderEnum Provider { get; set; }

		public Type EntityType => typeof(TEntity);

		/// <summary>
		/// Apply this mapping configuration
		/// </summary>
		/// <param name="modelBuilder">The builder being used to construct the model for the database context</param>
		public virtual void ApplyConfiguration(ModelBuilder modelBuilder)
		{
			modelBuilder.ApplyConfiguration(this);
		}

		#endregion
	}
}