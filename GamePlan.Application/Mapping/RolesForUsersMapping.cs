using AutoMapper;
using GamePlan.Domain.Dto.Role;
using GamePlan.Domain.Entity;

namespace GamePlan.Application.Mapping
{
	public class RolesForUsersMapping : Profile
	{
		public RolesForUsersMapping()
		{
			CreateMap<RolesForUsers, RolesForUsersDto>().ReverseMap();
		}
	}
}
