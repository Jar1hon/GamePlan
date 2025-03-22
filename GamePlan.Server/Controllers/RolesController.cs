using Asp.Versioning;
using GamePlan.Domain.Dto.Role;
using GamePlan.Domain.Dto.RolesForUsers;
using GamePlan.Domain.Dto.User;
using GamePlan.Domain.Dto.UserRole;
using GamePlan.Domain.Entity;
using GamePlan.Domain.Interfaces.Services;
using GamePlan.Domain.Result;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace GamePlan.Api.Controllers
{
	[Consumes(MediaTypeNames.Application.Json)]
	[ApiController]
	[ApiVersion("1.0")]
	[Route("api/v{version:apiVersion}/[controller]")]
	public class RolesController : ControllerBase
	{
		private readonly IRolesService _roleService;

		public RolesController(IRolesService roleService)
		{
			_roleService = roleService;
		}

		/// <summary>
		/// Создает новую роль.
		/// </summary>
		/// <param name="dto">DTO (Data Transfer Object) с данными для создания роли.</param>
		/// <remarks>
		/// Пример запроса:
		///
		///     POST /create-role
		///     {
		///         "name": "Admin",
		///         "description": "Администратор системы"
		///     }
		///
		/// В случае успешного создания роли возвращается объект с данными созданной роли.
		/// </remarks>
		/// <response code="200">Роль успешно создана. Возвращает данные созданной роли.</response>
		/// <response code="400">Ошибка при создании роли. Возвращает сообщение об ошибке.</response>
		[HttpPost("create-role")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public async Task<ActionResult<BaseResult<Roles>>> CreateRole([FromBody] CreateRolesDto dto)
		{
			var response = await _roleService.CreateRoleAsync(dto);

			if (response.isSuccess)
			{
				return Ok(response);
			}

			return BadRequest(response);
		}

		/// <summary>
		/// Удаляет роль по её уникальному идентификатору.
		/// </summary>
		/// <param name="id">Уникальный идентификатор роли.</param>
		/// <remarks>
		/// Пример запроса:
		///
		///     DELETE /delete-role/{id}
		///
		/// В случае успешного удаления роли возвращается статус 200.
		/// </remarks>
		/// <response code="200">Роль успешно удалена.</response>
		/// <response code="400">При удалении роли произошла ошибка. Возвращает сообщение об ошибке.</response>
		[HttpDelete("delete-role/{id}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public async Task<ActionResult<BaseResult<Roles>>> DeleteRole(Guid id)
		{
			var response = await _roleService.DeleteRoleAsync(id);

			if (response.isSuccess)
			{
				return Ok(response);
			}

			return BadRequest(response);
		}

		/// <summary>
		/// Обновляет данные роли по её уникальному идентификатору.
		/// </summary>
		/// <param name="dto">DTO (Data Transfer Object) с данными для обновления роли.</param>
		/// <remarks>
		/// Пример запроса:
		///
		///     PUT /update-role
		///     {
		///         "id": "0194c64b-d66d-7953-b72c-0b2837e0ec21",
		///         "name": "Admin",
		///         "description": "Администратор системы"
		///     }
		///
		/// В случае успешного обновления роли возвращается объект с обновлёнными данными.
		/// </remarks>
		/// <response code="200">Роль успешно обновлена. Возвращает данные обновлённой роли.</response>
		/// <response code="400">При обновлении роли произошла ошибка. Возвращает сообщение об ошибке.</response>
		[HttpPut("update-role")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public async Task<ActionResult<BaseResult<Roles>>> UpdateRole([FromBody] RolesDto dto)
		{
			var response = await _roleService.UpdateRoleAsync(dto);

			if (response.isSuccess)
			{
				return Ok(response);
			}

			return BadRequest(response);
		}

		/// <summary>
		/// Добавляет роль пользователю.
		/// </summary>
		/// <param name="dto">DTO (Data Transfer Object) с данными для добавления роли пользователю.</param>
		/// <remarks>
		/// Пример запроса:
		///	
		///		POST /add-user-role
		///		{
		///			"login": "firstUser",
		///			"role": "Admin"
		///		}
		///		
		/// В случае успешного добавления роли пользователю возвращается статус 200.
		/// </remarks>
		/// <response code="200">Роль успешно добавлена пользователю.</response>
		/// <response code="400">При добавлении роли пользователю произошла ошибка. Возвращает сообщение об ошибке.</response>
		[HttpPost("add-user-role")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public async Task<ActionResult<BaseResult<Roles>>> AddRoleToUser([FromBody] UsersInRolesDto dto)
		{
			var response = await _roleService.AddRoleToUserAsync(dto);

			if (response.isSuccess)
			{
				return Ok(response);
			}

			return BadRequest(response);
		}

		/// <summary>
		/// Удаляет роль у пользователя.
		/// </summary>
		/// <param name="dto">DTO (Data Transfer Object) с данными для удаления роли у пользователя.</param>
		/// <remarks>
		/// Пример запроса:
		///
		///     DELETE /delete-user-role
		///     {
		///         "login": "firstUser",
		///         "role": "Admin"
		///     }
		///
		/// В случае успешного удаления роли у пользователя возвращается статус 200.
		/// </remarks>
		/// <response code="200">Роль успешно удалена у пользователя.</response>
		/// <response code="400">При удалении роли у пользователя произошла ошибка. Возвращает сообщение об ошибке.</response>
		[HttpDelete("delete-user-role")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public async Task<ActionResult<BaseResult<Roles>>> DeleteRoleFromUser([FromBody] DeleteUserRoleDto dto)
		{
			var response = await _roleService.DeleteRoleFromUserAsync(dto);

			if (response.isSuccess)
			{
				return Ok(response);
			}

			return BadRequest(response);
		}

		/// <summary>
		/// Обновляет роль у пользователя.
		/// </summary>
		/// <param name="dto">DTO (Data Transfer Object) с данными для обновления роли у пользователя.</param>
		/// <remarks>
		/// Пример запроса:
		///
		///     PUT /update-user-role
		///     {
		///         "login": "firstUser",
		///         "oldRoleId": "Admin",
		///         "newRoleId": "Moderator"
		///     }
		///
		/// В случае успешного обновления роли у пользователя возвращается статус 200.
		/// </remarks>
		/// <response code="200">Роль успешно обновлена у пользователя.</response>
		/// <response code="400">При обновлении роли у пользователя произошла ошибка. Возвращает сообщение об ошибке.</response>
		[HttpPut("update-user-role")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public async Task<ActionResult<BaseResult<Roles>>> UpdateRoleToUser([FromBody] UpdateUserRoleDto dto)
		{
			var response = await _roleService.UpdateRoleToUserAsync(dto);

			if (response.isSuccess)
			{
				return Ok(response);
			}

			return BadRequest(response);
		}

		/// <summary>
		/// Получает список всех ролей.
		/// </summary>
		/// <remarks>
		/// Пример запроса:
		///
		///     GET /get-roles
		///
		/// В случае успешного выполнения возвращается список всех ролей.
		/// </remarks>
		/// <response code="200">Список ролей успешно получен.</response>
		/// <response code="400">При получении списка ролей произошла ошибка. Возвращает сообщение об ошибке.</response>
		[HttpGet("get-roles")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public async Task<ActionResult<BaseResult<List<RolesDto>>>> GetAllRoles()
		{
			var response = await _roleService.GetAllRolesAsync();

			if (response.isSuccess)
			{
				return Ok(response);
			}

			return BadRequest(response);
		}

		/// <summary>
		/// Получает роль по её уникальному идентификатору.
		/// </summary>
		/// <param name="roleId">Уникальный идентификатор роли.</param>
		/// <remarks>
		/// Пример запроса:
		///
		///     GET /get-role/{roleId}
		///
		/// В случае успешного выполнения возвращается данные роли.
		/// </remarks>
		/// <response code="200">Роль успешно получена.</response>
		/// <response code="400">При получении роли произошла ошибка. Возвращает сообщение об ошибке.</response>
		[HttpGet("get-role/{roleId}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public async Task<ActionResult<BaseResult<List<RolesDto>>>> GetRoleByRoleId(Guid roleId)
		{
			var response = await _roleService.GetRoleByRoleIdAsync(roleId);

			if (response.isSuccess)
			{
				return Ok(response);
			}

			return BadRequest(response);
		}

		/// <summary>
		/// Получает список ролей пользователя по его уникальному идентификатору.
		/// </summary>
		/// <param name="userId">Уникальный идентификатор пользователя.</param>
		/// <remarks>
		/// Пример запроса:
		///
		///     GET /get-user-roles/{userId}
		///
		/// В случае успешного выполнения возвращается список ролей пользователя.
		/// </remarks>
		/// <response code="200">Список ролей пользователя успешно получен.</response>
		/// <response code="400">При получении списка ролей произошла ошибка. Возвращает сообщение об ошибке.</response>
		[HttpGet("get-user-roles/{userId}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public async Task<ActionResult<BaseResult<List<UserWithRolesDto>>>> GetRolesByUserId(Guid userId)
		{
			var response = await _roleService.GetRolesByUserIdAsync(userId);

			if (response.isSuccess)
			{
				return Ok(response);
			}

			return BadRequest(response);
		}


		/// <summary>
		/// Получает список пользователей, которые имеют определённую роль.
		/// </summary>
		/// <param name="roleId">Уникальный идентификатор роли.</param>
		/// <remarks>
		/// Пример запроса:
		///
		///     GET /get-users-in-role/{roleId}
		///
		/// В случае успешного выполнения возвращается список пользователей, которые имеют указанную роль.
		/// </remarks>
		/// <response code="200">Список пользователей успешно получен.</response>
		/// <response code="400">При получении списка пользователей произошла ошибка. Возвращает сообщение об ошибке.</response>
		[HttpGet("get-users-in-role/{roleId}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public async Task<ActionResult<BaseResult<List<UserDto>>>> GetUsersByRoleId(Guid roleId)
		{
			var response = await _roleService.GetUsersByRoleIdAsync(roleId);

			if (response.isSuccess)
			{
				return Ok(response);
			}

			return BadRequest(response);
		}

		/// <summary>
		/// Проверяет, есть ли у пользователя определённая роль.
		/// </summary>
		/// <param name="userId">Уникальный идентификатор пользователя.</param>
		/// <param name="roleId">Уникальный идентификатор роли.</param>
		/// <remarks>
		/// Пример запроса:
		///
		///     GET /is-user-in-role/{userId}/{roleId}
		///
		/// В случае успешного выполнения возвращается true, если роль есть у пользователя, иначе false.
		/// </remarks>
		/// <response code="200">Проверка выполнена успешно.</response>
		/// <response code="400">При проверке произошла ошибка. Возвращает сообщение об ошибке.</response>
		[HttpGet("is-user-in-role/{userId}/{roleId}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public async Task<ActionResult<BaseResult<bool>>> IsUserInRole(Guid userId, Guid roleId)
		{
			var response = await _roleService.IsUserInRoleAsync(userId, roleId);

			if (response.isSuccess)
			{
				return Ok(response);
			}

			return BadRequest(response);
		}

		/// <summary>
		/// Получает список всех пользователей с их ролями.
		/// </summary>
		/// <remarks>
		/// Пример запроса:
		///
		///     GET /get-users-with-roles
		///
		/// В случае успешного выполнения возвращается список пользователей с их ролями.
		/// </remarks>
		/// <response code="200">Список пользователей с ролями успешно получен.</response>
		/// <response code="400">При получении списка произошла ошибка. Возвращает сообщение об ошибке.</response>
		[HttpGet("get-users-with-roles")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public async Task<ActionResult<BaseResult<List<UserWithRolesDto>>>> GetAllUsersWithRoles()
		{
			var response = await _roleService.GetAllUsersWithRolesAsync();

			if (response.isSuccess)
			{
				return Ok(response);
			}

			return BadRequest(response);
		}

		/// <summary>
		/// Получает роль по её названию.
		/// </summary>
		/// <param name="roleName">Название роли.</param>
		/// <remarks>
		/// Пример запроса:
		///
		///     GET /get-role-by-name/{roleName}
		///
		/// В случае успешного выполнения возвращается данные роли.
		/// </remarks>
		/// <response code="200">Роль успешно получена.</response>
		/// <response code="400">При получении роли произошла ошибка. Возвращает сообщение об ошибке.</response>
		[HttpGet("get-role-by-name/{roleName}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public async Task<ActionResult<BaseResult<List<UserWithRolesDto>>>> GetRoleIdByName(string roleName)
		{
			var response = await _roleService.GetRoleIdByNameAsync(roleName);

			if (response.isSuccess)
			{
				return Ok(response);
			}

			return BadRequest(response);
		}
	}
}
