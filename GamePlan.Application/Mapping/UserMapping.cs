using AutoMapper;
using GamePlan.Domain.Dto.User;
using GamePlan.Domain.Entity;

namespace GamePlan.Application.Mapping
{
	public class UserMapping : Profile
	{
		public UserMapping()
		{
			CreateMap<Users, UserDto>().ReverseMap();
		}
	}
}
