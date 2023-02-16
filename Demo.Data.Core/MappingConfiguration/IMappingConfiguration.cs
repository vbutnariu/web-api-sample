using Microsoft.EntityFrameworkCore;
using Demo.Common.Enums;

namespace Demo.Data.Core
{
    /// <summary>
    /// Represents database context model mapping configuration
    /// </summary>
    public partial interface IMappingConfiguration
    {
        DatabaseProviderEnum Provider { get; set; }

		System.Type EntityType { get; }
		/// <summary>
		/// Apply this mapping configuration
		/// </summary>
		/// <param name="modelBuilder">The builder being used to construct the model for the database context</param>
		void ApplyConfiguration(ModelBuilder modelBuilder);
    }
}