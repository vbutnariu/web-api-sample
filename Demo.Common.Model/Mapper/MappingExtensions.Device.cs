using Demo.Core.DomainModel.Authorization;
using Demo.Common.Model.Autorization;
using Pm.Common.Model.Device;
using Demo.Core.DomainModel.App;

namespace Demo.Common.Model
{
    public static partial class MappingExtensions
    {
        #region Security and OAUTH mapping 
        public static DeviceModel ToModel(this Devices entity)
        {
            return entity.MapTo<Devices, DeviceModel>();
        }

        public static Devices ToEntity(this DeviceModel model)
        {
            return model.MapTo<DeviceModel, Devices>();
        }


        #endregion

    }
}
