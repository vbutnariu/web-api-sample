using AutoMapper;
using Demo.Common.Model.Autorization;
using Demo.Core.DomainModel.Authorization;
using Demo.Core.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Common.Model.Mapper.Configuration
{
	public class AuthorizationModelConfiguration : IMapperConfiguration
	{
		public int Order => 2;

		public Action<IMapperConfigurationExpression> GetConfiguration()
		{
			Action<IMapperConfigurationExpression> action = cfg =>
			{
				cfg.CreateMap<RestClientModel, RestClient>()
					  .ForMember(s => s.RefreshTokens, t => t.Ignore());

				cfg.CreateMap<RestClient, RestClientModel>();

				cfg.CreateMap<RefreshTokenModel, RefreshToken>()
					.ForMember(s => s.RestClient, t => t.Ignore());

				cfg.CreateMap<RefreshToken, RefreshTokenModel>();
			};
			return action;
		}
	}
}
