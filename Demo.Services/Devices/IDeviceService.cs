using Demo.Core.DomainModel.App;
using Pm.Common.Model.Device;
using Pm.Common.Model.DeviceModel;
using System.Collections.Generic;

namespace Demo.Services.Core.Device
{
    public interface IDeviceService
    {
        List<DeviceModel> GetAllDevices();
        List<DeviceModelDto> GetAllDeviceModels();
    }
}   