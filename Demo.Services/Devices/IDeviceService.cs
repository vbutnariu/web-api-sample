using Demo.Core.DomainModel.App;
using Pm.Common.Model.Device;
using System.Collections.Generic;

namespace Demo.Services.Core.Device
{
    public interface IDeviceService
    {
        List<DeviceModel> GetAllDevices();
    }
}   