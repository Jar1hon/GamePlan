using AutoMapper;
using GamePlan.Domain.Dto.Role;
using GamePlan.Domain.Entity;

namespace GamePlan.Application.Mapping
{
	public class RolesForUsersMapping : Profile
	{
		public RolesForUsersMapping()
		{
			CreateMap<RolesForUsers, RolesForUsersDto>()
				.ForCtorParam(ctorParamName: "Id", m => m.MapFrom(s => s.Id))
				.ForCtorParam(ctorParamName: "Name", m => m.MapFrom(s => s.Name))
				.ForCtorParam(ctorParamName: "Description", m => m.MapFrom(s => s.Description))
				.ReverseMap();
		}
	}
}
