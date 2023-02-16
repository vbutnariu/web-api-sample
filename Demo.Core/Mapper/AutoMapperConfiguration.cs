using AutoMapper;
using System;
using System.Collections.Generic;

namespace Demo.Core.Mapper
{
    public static class AutoMapperConfiguration
    {
        private static MapperConfiguration mapperConfiguration;
        private static IMapper mapper;

        /// <summary>
        /// Initialize mapper
        /// </summary>
        /// <param name="configurationActions">Configuration actions</param>
        public static void Init(List<Action<IMapperConfigurationExpression>> configurationActions)
        {
            if (configurationActions == null)
                throw new ArgumentNullException("configurationActions");

            mapperConfiguration = new MapperConfiguration(cfg =>
            {
                foreach (var ca in configurationActions)
                    ca(cfg);
            });

            mapper = mapperConfiguration.CreateMapper();
        }

        /// <summary>
        /// Mapper
        /// </summary>
        public static IMapper Mapper
        {
            get
            {
                return mapper;
            }
        }
        /// <summary>
        /// Mapper configuration
        /// </summary>
        public static MapperConfiguration MapperConfiguration
        {
            get
            {
                return mapperConfiguration;
            }
        }
    }
}
