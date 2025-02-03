using AutoMapper;
using GamePlan.Domain.Dto.RolesForUsers;
using GamePlan.Domain.Entity;

namespace GamePlan.Application.Mapping
{
	public class UserWithRolesMapping : Profile
	{
		public UserWithRolesMapping()
		{
			CreateMap<Users, UserWithRolesDto>()
				.ForCtorParam(ctorParamName: "UserId", m => m.MapFrom(s => s.Id))
				.ForCtorParam(ctorParamName: "UserName", m => m.MapFrom(s => s.UserName))
				.ForCtorParam(ctorParamName: "Email", m => m.MapFrom(s => s.Email))
				.ForCtorParam(ctorParamName: "CreatedAt", m => m.MapFrom(s => s.CreatedAt))
				.ForCtorParam(ctorParamName: "UpdatedAt", m => m.MapFrom(s => s.UpdatedAt))
				.ForCtorParam(ctorParamName: "Roles", m => m.MapFrom(s => s.Roles))
				.ReverseMap();
		}
	}
}
