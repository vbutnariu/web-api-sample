using AutoMapper;
using Demo.Core.DomainModel.App;
using Demo.Core.Mapper;
using Pm.Common.Model.DeviceModel;
using System;

namespace Pm.Common.Model.Mapper.Configuration
{
    public class DeviceModelConfiguration : IMapperConfiguration
    {
        public int Order => 3;

        public Action<IMapperConfigurationExpression> GetConfiguration()
        {
            Action<IMapperConfigurationExpression> action = cfg =>
            {
                cfg.CreateMap<Device.DeviceModel, Devices>()
                .ForMember(s => s.DeviceTwinProperties, d => d.Ignore());


                cfg.CreateMap<Devices, Device.DeviceModel>();


                cfg.CreateMap<DeviceModelDto, DeviceModels>()
               .ForMember(s => s.DeviceFirmwareDeviceModels, d => d.Ignore());


                cfg.CreateMap<DeviceModels, DeviceModelDto>();



            };
            return action;
        }
    }
}
