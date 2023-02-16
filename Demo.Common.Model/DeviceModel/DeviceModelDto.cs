using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pm.Common.Model.DeviceModel
{
    public class DeviceModelDto
    {
        public Guid Id { get; set; }
        public string Vendor { get; set; }
        public string Identifier { get; set; }
        public string Version { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }
        public string Akz { get; set; }
    }
}
