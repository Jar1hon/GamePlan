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

		/// <summary>
		/// Получение списка ролей
		/// </summary>
		/// <returns></returns>
		Task<BaseResult<List<RolesForUsersDto>>> GetAllRolesAsync();

		/// <summary>
		/// Получение роли по ID
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		Task<BaseResult<RolesForUsersDto>> GetRoleByIdAsync(Guid id);

		/// <summary>
		/// Получение ролей пользователя
		/// </summary>
		/// <param name="userId"></param>
		/// <returns></returns>
		Task<BaseResult<List<RolesForUsersDto>>> GetUserRolesAsync(Guid userId);

		/// <summary>
		/// Получение пользователей с определённой ролью
		/// </summary>
		/// <param name="roleId"></param>
		/// <returns></returns>
		Task<BaseResult<List<UserDto>>> GetUsersInRoleAsync(Guid roleId);

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
		Task<BaseResult<RolesForUsersDto>> GetRoleByNameAsync(string roleName);

		/// <summary>
		/// Удаление всех ролей пользователя
		/// </summary>
		/// <param name="userId"></param>
		/// <returns></returns>
		Task<BaseResult<bool>> DeleteAllRolesForUserAsync(Guid userId);

		/// <summary>
		/// Получение ролей с пагинацией
		/// </summary>
		/// <param name="page"></param>
		/// <param name="pageSize"></param>
		/// <returns></returns>
		Task<BaseResult<List<RolesForUsersDto>>> GetRolesWithPaginationAsync(int page, int pageSize);

		/// <summary>
		/// Получение пользователей с ролями с пагинацией
		/// </summary>
		/// <param name="page"></param>
		/// <param name="pageSize"></param>
		/// <returns></returns>
		Task<BaseResult<List<UserWithRolesDto>>> GetUsersWithRolesPaginationAsync(int page, int pageSize);

		/// <summary>
		/// Получение количества пользователей с определённой ролью
		/// </summary>
		/// <param name="roleId"></param>
		/// <returns></returns>
		Task<BaseResult<int>> GetUserCountInRoleAsync(Guid roleId);

		/// <summary>
		/// Получение всех ролей с количеством пользователей
		/// </summary>
		/// <returns></returns>
		Task<BaseResult<List<RoleWithUserCountDto>>> GetAllRolesWithUserCountAsync();

		/// <summary>
		/// Получение ролей по фильтру
		/// </summary>
		/// <param name="filter"></param>
		/// <returns></returns>
		Task<BaseResult<List<RolesForUsersDto>>> GetRolesByFilterAsync(RoleFilterDto filter);

		/// <summary>
		/// Получение пользователей с ролями по фильтру
		/// </summary>
		/// <param name="filter"></param>
		/// <returns></returns>
		Task<BaseResult<List<UserWithRolesDto>>> GetUsersWithRolesByFilterAsync(UserRoleFilterDto filter);
	}
}
