using GamePlan.Domain.Dto.Role;
using GamePlan.Domain.Dto.UserRole;
using GamePlan.Domain.Result;

namespace GamePlan.Domain.Interfaces.Services
{
	/// <summary>
	/// Сервис, предназначенный для управления ролями пользователей
	/// </summary>
	public interface IRolesForUsersService
	{
		/// <summary>
		/// Создание роли
		/// </summary>
		/// <param name="dto"></param>
		/// <returns></returns>
		Task<BaseResult<RolesForUsersDto>> CreateRoleAsync(CreateRolesForUsersDto dto);

		/// <summary>
		/// Удаление роли
		/// </summary>
		/// <param name="dto"></param>
		/// <returns></returns>
		Task<BaseResult<RolesForUsersDto>> DeleteRoleAsync(Guid id);

		/// <summary>
		/// Обновление роли
		/// </summary>
		/// <param name="dto"></param>
		/// <returns></returns>
		Task<BaseResult<RolesForUsersDto>> UpdateRoleAsync(RolesForUsersDto dto);

		/// <summary>
		/// Добавление роли для пользователя
		/// </summary>
		/// <param name="dto"></param>
		/// <returns></returns>
		Task<BaseResult<UsersInRolesDto>> AddRoleForUserAsync(UsersInRolesDto dto);

		/// <summary>
		/// Удаление роли для пользователя
		/// </summary>
		/// <param name="dto"></param>
		/// <returns></returns>
		Task<BaseResult<UsersInRolesDto>> DeleteRoleForUserAsync(DeleteUserRoleDto dto);

		/// <summary>
		/// Обновление роли пользователя
		/// </summary>
		/// <param name="dto"></param>
		/// <returns></returns>
		Task<BaseResult<UsersInRolesDto>> UpdateRoleForUserAsync(UpdateUserRoleDto dto);
	}
}
