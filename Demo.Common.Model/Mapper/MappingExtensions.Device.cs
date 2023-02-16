using Demo.Core.DomainModel.Authorization;
using Demo.Common.Model.Autorization;
using Pm.Common.Model.Device;
using Demo.Core.DomainModel.App;
using Pm.Common.Model.DeviceModel;

namespace Demo.Common.Model
{
    public static partial class MappingExtensions
    {
        #region Device model
        public static DeviceModel ToModel(this Devices entity)
        {
            return entity.MapTo<Devices, DeviceModel>();
        }

        public static Devices ToEntity(this DeviceModel model)
        {
            return model.MapTo<DeviceModel, Devices>();
        }


        #endregion
        #region DeviceModels model
        public static DeviceModelDto ToModel(this DeviceModels entity)
        {
            return entity.MapTo<DeviceModels, DeviceModelDto>();
        }

        public static DeviceModels ToEntity(this DeviceModelDto model)
        {
            return model.MapTo<DeviceModelDto, DeviceModels>();
        }


        #endregion

    }
}
