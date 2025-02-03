using GamePlan.Domain.Dto.Role;

namespace GamePlan.Domain.Dto.RolesForUsers
{
	public record UserWithRolesDto(Guid UserId, string UserName, string Email, DateTime CreatedAt, 
		DateTime UpdatedAt, List<RolesForUsersDto> Roles);
}
