using Demo.Common.Model;
using Demo.Core.Data;
using Demo.Core.DomainModel.App;
using Pm.Common.Model.Device;
using Pm.Common.Model.DeviceModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Services.Core.Device
{
    public class DeviceService : IDeviceService
    {
        private readonly IRepository<Devices> deviceRepository;
        private readonly IRepository<DeviceModels> deviceModelRepository;

        public DeviceService(IRepository<Devices> deviceRepository, IRepository<DeviceModels> deviceceModelRepository)
        {
            this.deviceRepository = deviceRepository;
            this.deviceModelRepository = deviceceModelRepository;
        }

        public List<DeviceModel> GetAllDevices()
        {
           return deviceRepository.Table.Select(d => d.ToModel()).ToList();
        }


        public List<DeviceModelDto> GetAllDeviceModels()
        {
            return deviceModelRepository.Table.Select(d => d.ToModel()).ToList();
        }

    }
}
