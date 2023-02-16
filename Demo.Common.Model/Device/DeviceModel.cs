using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pm.Common.Model.Device
{
    public class DeviceModel
    {
        public long Id { get; set; }
        public string DeviceId { get; set; }
        public string VirtualLaboratoryId { get; set; }
        public string Vendor { get; set; }
        public string SerialNumber { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string ModelType { get; set; }
        public string ModelIdentificationId { get; set; }
        public string ModelIdentificationVersion { get; set; }
        public string BaseModelIdentificationId { get; set; }
        public string BaseModelIdentificationVersion { get; set; }
    }
}
