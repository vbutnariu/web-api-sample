using AutoMapper;
using Demo.Common.Model.Autorization;
using Demo.Core.DomainModel.App;
using Demo.Core.DomainModel.Authorization;
using Demo.Core.Mapper;
using Pm.Common.Model.Device;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pm.Common.Model.Mapper.Configuration
{
    public class DeviceModelConfiguration : IMapperConfiguration
    {
        public int Order => 3;

        public Action<IMapperConfigurationExpression> GetConfiguration()
        {
            Action<IMapperConfigurationExpression> action = cfg =>
            {
                cfg.CreateMap<DeviceModel, Devices>()
                .ForMember(s => s.DeviceTwinProperties, d => d.Ignore());


                cfg.CreateMap<Devices, DeviceModel>();


            };
            return action;
        }
    }
}
