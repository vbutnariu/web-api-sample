using AutoMapper;
using Demo.Core.Infrastructure;
using Demo.Core.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Demo.Common.Model.Mapper
{
    public class ModelMapperConfiguration : IModelMapperConfiguration
    {

        /// <summary>
        /// Get configuration
        /// </summary>
        /// <returns>Mapper configuration action</returns>
        public List<Action<IMapperConfigurationExpression>> GetConfiguration()
        {
            //dependencies
            var typeFinder = new ApplicationTypeFinder();

            //register mapper configurations provided by other assemblies
            var types = typeFinder.FindClassesOfType<IMapperConfiguration>();
            var instances = new List<IMapperConfiguration>();

            foreach (var type in types)
                instances.Add((IMapperConfiguration)Activator.CreateInstance(type));
            
            //sort
            instances = instances.AsQueryable().OrderBy(t => t.Order).ToList();
            
            //get configurations
            var configurationActions = new List<Action<IMapperConfigurationExpression>>();
            foreach (var mc in instances)
                configurationActions.Add(mc.GetConfiguration());

            return configurationActions;
        }



        /// <summary>
        /// Order of this mapper implementation
        /// </summary>
        public int Order
        {
            get { return 0; }
        }
    }
}




