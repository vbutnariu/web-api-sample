using Demo.Services.Core.Device;
using Demo.WebApi.Ergodat.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pm.Common.Model.Device;
using System.Collections.Generic;

namespace Demo.WebApi.Server.Controllers
{
    
    public class DeviceController : AppBaseController
    {
        private readonly IDeviceService deviceService;

        public DeviceController(IDeviceService deviceService)
        {
            this.deviceService = deviceService;
        }


        [HttpGet]
        [AllowAnonymous]
        //[Route("/api/[controller]/version")]
        public List<DeviceModel> Devices()
        {
            return TryExecute(() => deviceService.GetAllDevices());
        }


    }
}
