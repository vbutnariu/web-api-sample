using Demo.Common.Model;
using Demo.Core.Data;
using Demo.Core.DomainModel.App;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Protocols;
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
        private readonly IRepository<DeviceFirmwareDeviceModels> deviceFirmwares;

        public DeviceService(IRepository<Devices> deviceRepository,
             IRepository<DeviceModels> deviceceModelRepository,
            IRepository<DeviceFirmwareDeviceModels> deviceFirmwares)
        {
            this.deviceRepository = deviceRepository;
            this.deviceModelRepository = deviceceModelRepository;
            this.deviceFirmwares = deviceFirmwares;
        }

        public List<DeviceModel> GetAllDevices()
        {
            return deviceRepository.Table.Select(d => d.ToModel()).ToList();
        }


        public List<DeviceModelDto> GetAllDeviceModels()
        {
            return deviceModelRepository.Table.Select(d => d.ToModel()).ToList();
        }

        public List<DeviceFirmwareDeviceModels> DeviceFirmwareDeviceModels1(Guid deviceModelId)
        {
            var model = deviceModelRepository.Table.Include(d => d.DeviceFirmwareDeviceModels).Where(d => d.Id == deviceModelId).FirstOrDefault();

            return model.DeviceFirmwareDeviceModels.ToList(); 
        }

        public List<DeviceFirmwareDeviceModels> DeviceFirmwareDeviceModels2(Guid deviceModelId)
        {
           return  deviceFirmwares.Table.Where(f=>f.DeviceModelId == deviceModelId).ToList();  
            
        }

        public List<DeviceFirmwareDeviceModels> DeviceFirmwareDeviceModels3(Guid deviceModelId)
        {
            var query = from d in deviceModelRepository.Table
                        join
                        df in deviceFirmwares.Table on d.Id equals df.DeviceModelId
                        where d.Id == deviceModelId
                        select df;

            return query.ToList();

        }

    }
}
