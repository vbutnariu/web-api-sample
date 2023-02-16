using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Demo.Common.Enums;

namespace Demo.Core.Data
{
	/// <summary>
	/// Represents base query type mapping configuration
	/// </summary>
	/// <typeparam name="TQuery">Query type type</typeparam>

	public partial class QueryTypeConfiguration<TQuery> : IMappingConfiguration, IEntityTypeConfiguration<TQuery> where TQuery : class
	{
		public DatabaseProviderEnum Provider { get; set; }
		public System.Type EntityType { get => typeof(TQuery); }
		#region Utilities

		/// <summary>
		/// Developers can override this method in custom partial classes in order to add some custom configuration code
		/// </summary>
		/// <param name="builder">The builder to be used to configure the query</param>
		protected virtual void PostConfigure(EntityTypeBuilder<TQuery> builder, DatabaseProviderEnum provider)
		{
		}

		#endregion

		#region Methods

		/// <summary>
		/// Configures the query type
		/// </summary>
		/// <param name="builder">The builder to be used to configure the query type</param>
		public virtual void Configure(EntityTypeBuilder<TQuery> builder)
		{
			builder.HasNoKey();
			var type = builder.GetType().GetGenericArguments()[0];
			builder.ToTable(type.Name.ToLower(), t => t.ExcludeFromMigrations());
			//add custom configuration
			this.PostConfigure(builder, this.Provider);
		}

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