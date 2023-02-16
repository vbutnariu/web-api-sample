using Demo.Common.Model;
using Demo.Core.Data;
using Demo.Core.DomainModel.App;
using Pm.Common.Model.Device;
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

        public DeviceService(IRepository<Devices> deviceRepository)
        {
            this.deviceRepository = deviceRepository;
        }

        public List<DeviceModel> GetAllDevices()
        {
           return deviceRepository.Table.Select(d => d.ToModel()).ToList();
        }

    }
}
