using Demo.Core.DomainModel.Authorization;
using Demo.Common.Model.Autorization;

namespace Demo.Common.Model
{
	public static partial class MappingExtensions
	{
		#region Security and OAUTH mapping 
		public static RestClientModel ToModel(this RestClient entity)
		{
			return entity.MapTo<RestClient, RestClientModel>();
		}

		public static RestClient ToEntity(this RestClientModel model)
		{
			return model.MapTo<RestClientModel, RestClient>();
		}


		public static RefreshTokenModel ToModel(this RefreshToken entity)
		{
			return entity.MapTo<RefreshToken, RefreshTokenModel>();
		}

		public static RefreshToken ToEntity(this RefreshTokenModel model)
		{
			return model.MapTo<RefreshTokenModel, RefreshToken>();
		}
		#endregion

	}
}
