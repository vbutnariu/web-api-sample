using Demo.Services.Core.Device;
using Demo.WebApi.Ergodat.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pm.Common.Model.Device;
using Pm.Common.Model.DeviceModel;
using System.Collections.Generic;

namespace Demo.WebApi.Server.Controllers
{

    [Authorize]
    public class DevicesController : AppBaseController
    {
        private readonly IDeviceService deviceService;

        public DevicesController(IDeviceService deviceService)
        {
            this.deviceService = deviceService;
        }


        [HttpGet]
       
        [Route("Device")]
        public List<DeviceModel> Devices()
        {
            return TryExecute(() => deviceService.GetAllDevices());
        }


        [HttpGet]
        
        [Route("DeviceModels")]
        public List<DeviceModelDto> DeviceModels()
        {
            return TryExecute(() => deviceService.GetAllDeviceModels());
        }


    }
}
