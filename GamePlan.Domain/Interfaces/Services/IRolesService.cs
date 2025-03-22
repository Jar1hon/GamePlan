using GamePlan.Domain.Dto.Role;
using GamePlan.Domain.Dto.RolesForUsers;
using GamePlan.Domain.Dto.User;
using GamePlan.Domain.Dto.UserRole;
using GamePlan.Domain.Result;

namespace GamePlan.Domain.Interfaces.Services
{
	/// <summary>
	/// Сервис, предназначенный для управления ролями пользователей
	/// </summary>
	public interface IRolesService
	{
		/// <summary>
		/// Создание роли
		/// </summary>
		/// <param name="dto"></param>
		/// <returns></returns>
		Task<BaseResult<RolesDto>> CreateRoleAsync(CreateRolesDto dto);

		/// <summary>
		/// Удаление роли
		/// </summary>
		/// <param name="dto"></param>
		/// <returns></returns>
		Task<BaseResult<RolesDto>> DeleteRoleAsync(Guid id);

		/// <summary>
		/// Обновление роли
		/// </summary>
		/// <param name="dto"></param>
		/// <returns></returns>
		Task<BaseResult<RolesDto>> UpdateRoleAsync(RolesDto dto);

		/// <summary>
		/// Добавление роли для пользователя
		/// </summary>
		/// <param name="dto"></param>
		/// <returns></returns>
		Task<BaseResult<UsersInRolesDto>> AddRoleToUserAsync(UsersInRolesDto dto);

		/// <summary>
		/// Удаление роли для пользователя
		/// </summary>
		/// <param name="dto"></param>
		/// <returns></returns>
		Task<BaseResult<UsersInRolesDto>> DeleteRoleFromUserAsync(DeleteUserRoleDto dto);

		/// <summary>
		/// Обновление роли пользователя
		/// </summary>
		/// <param name="dto"></param>
		/// <returns></returns>
		Task<BaseResult<UsersInRolesDto>> UpdateRoleToUserAsync(UpdateUserRoleDto dto);

		/// <summary>
		/// Получение списка ролей
		/// </summary>
		/// <returns></returns>
		Task<BaseResult<List<RolesDto>>> GetAllRolesAsync();

		/// <summary>
		/// Получение роли по ID
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		Task<BaseResult<RolesDto>> GetRoleByRoleIdAsync(Guid roleId);

		/// <summary>
		/// Получение ролей пользователя по его ID
		/// </summary>
		/// <param name="userId"></param>
		/// <returns></returns>
		Task<BaseResult<List<UserWithRolesDto>>> GetRolesByUserIdAsync(Guid userId);

		/// <summary>
		/// Получение пользователей с определённой ролью
		/// </summary>
		/// <param name="roleId"></param>
		/// <returns></returns>
		Task<BaseResult<List<UserDto>>> GetUsersByRoleIdAsync(Guid roleId);

		/// <summary>
		/// Проверка наличия роли у пользователя
		/// </summary>
		/// <param name="userId"></param>
		/// <param name="roleId"></param>
		/// <returns></returns>
		Task<BaseResult<bool>> IsUserInRoleAsync(Guid userId, Guid roleId);

		/// <summary>
		/// Получение всех пользователей с их ролями
		/// </summary>
		/// <returns></returns>
		Task<BaseResult<List<UserWithRolesDto>>> GetAllUsersWithRolesAsync();

		/// <summary>
		/// Получение роли по названию
		/// </summary>
		/// <param name="roleName"></param>
		/// <returns></returns>
		Task<BaseResult<RolesDto>> GetRoleIdByNameAsync(string roleName);
	}
}
