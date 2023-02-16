using AutoMapper;
using System;
using System.Collections.Generic;

namespace Demo.Core.Infrastructure
{
    public interface IModelMapperConfiguration
    {
        List<Action<IMapperConfigurationExpression>> GetConfiguration();
    }
}
