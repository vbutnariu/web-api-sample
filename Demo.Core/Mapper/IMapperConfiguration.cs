using AutoMapper;
using System;

namespace Demo.Core.Mapper
{
    public interface IMapperConfiguration
    {
        /// <summary>
        /// Get configuration
        /// </summary>
        /// <returns>Mapper configuration action</returns>
        Action<IMapperConfigurationExpression> GetConfiguration();

        /// <summary>
        /// Order of this mapper implementation
        /// </summary>
        int Order { get; }
    }
}
