using Asp.Versioning;
using GamePlan.Domain.Dto.Role;
using GamePlan.Domain.Dto.RolesForUsers;
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
	public class RolesForUsersController : ControllerBase
	{
		private readonly IRolesForUsersService _roleService;

		public RolesForUsersController(IRolesForUsersService roleService)
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
		///         "name": "admin"
		///     }
		///
		/// В случае успешного создания роли возвращается объект с данными созданной роли.
		/// </remarks>
		/// <response code="200">Роль успешно создана. Возвращает данные созданной роли.</response>
		/// <response code="400">Ошибка при создании роли. Возвращает сообщение об ошибке.</response>
		[HttpPost("create-role")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public async Task<ActionResult<BaseResult<RolesForUsers>>> CreateRole([FromBody] CreateRolesForUsersDto dto)
		{
			var response = await _roleService.CreateRoleAsync(dto);

			if (response.isSuccess)
			{
				return Ok(response);
			}

			return BadRequest(response);
		}

		/// <summary>
		/// Удаление роли по Id
		/// </summary>
		/// <param name="id"></param>
		///<remarks>
		///	Метод для удаления роли по его Id:
		///	
		///		DELETE
		///		{
		///			"id": "0194c64b-d66d-7953-b72c-0b2837e0ec21"
		///		}
		///		
		///</remarks>
		///<response code="200">Роль успешно удалена</response>
		///<response code="400">При удалении роли произшла ошибка</response>
		[HttpDelete("delete-role{id}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public async Task<ActionResult<BaseResult<RolesForUsers>>> DeleteRole(Guid id)
		{
			var response = await _roleService.DeleteRoleAsync(id);

			if (response.isSuccess)
			{
				return Ok(response);
			}

			return BadRequest(response);
		}

		/// <summary>
		/// Обновление роли
		/// </summary>
		/// <param name="dto"></param>
		/// <remarks>
		///	Метод для обновления роли по его id:
		///	
		///		PUT
		///		{
		///			"id": "0194c64b-d66d-7953-b72c-0b2837e0ec21",
		///			"name": "Admin",
		///			"description": "Admin role"
		///		}
		///		
		///</remarks>
		///<response code="200">Роль успешно обновлена</response>
		///<response code="400">При обновлении роли произшла ошибка</response>
		[HttpPut("update-role")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public async Task<ActionResult<BaseResult<RolesForUsers>>> UpdateRole([FromBody] RolesForUsersDto dto)
		{
			var response = await _roleService.UpdateRoleAsync(dto);

			if (response.isSuccess)
			{
				return Ok(response);
			}

			return BadRequest(response);
		}

		/// <summary>
		/// Добавление роли для пользователя
		/// </summary>
		/// <param name="dto"></param>
		/// <remarks>
		///	Метод для добавления роли пользователю:
		///	
		///		POST
		///		{
		///			"login": "firstUser",
		///			"role": "Admin"
		///		}
		///		
		///</remarks>
		///<response code="200">Роль для выбранного пользователя успешно добавлена</response>
		///<response code="400">При добавлении роли пользователю произшла ошибка</response>
		[HttpPost("add-userrole")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public async Task<ActionResult<BaseResult<RolesForUsers>>> AddRoleForUser([FromBody] UsersInRolesDto dto)
		{
			var response = await _roleService.AddRoleForUserAsync(dto);

			if (response.isSuccess)
			{
				return Ok(response);
			}

			return BadRequest(response);
		}

		/// <summary>
		/// Удаление роли пользователя
		/// </summary>
		/// <param name="dto"></param>
		/// <returns></returns>
		[HttpDelete("delete-userrole")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public async Task<ActionResult<BaseResult<RolesForUsers>>> DeleteRoleForUser([FromBody] DeleteUserRoleDto dto)
		{
			var response = await _roleService.DeleteRoleForUserAsync(dto);

			if (response.isSuccess)
			{
				return Ok(response);
			}

			return BadRequest(response);
		}

		/// <summary>
		/// Обновление роли пользователя
		/// </summary>
		/// <param name="dto"></param>
		/// <returns></returns>
		[HttpPut("update-userrole")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public async Task<ActionResult<BaseResult<RolesForUsers>>> UpdateRoleForUser([FromBody] UpdateUserRoleDto dto)
		{
			var response = await _roleService.UpdateRoleForUserAsync(dto);

			if (response.isSuccess)
			{
				return Ok(response);
			}

			return BadRequest(response);
		}

		/// <summary>
		/// Получение роли пользователя
		/// </summary>
		/// <param name="dto"></param>
		/// <returns></returns>
		[HttpGet("get-userrole")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public async Task<ActionResult<BaseResult<List<UserWithRolesDto>>>> GetAll()
		{
			var response = await _roleService.GetAllUsersWithRolesAsync();

			if (response.isSuccess)
			{
				return Ok(response);
			}

			return BadRequest(response);
		}
	}
}
