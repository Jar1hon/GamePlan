using Asp.Versioning;
using GamePlan.Domain.Dto;
using GamePlan.Domain.Dto.User;
using GamePlan.Domain.Interfaces.Services;
using GamePlan.Domain.Result;
using Microsoft.AspNetCore.Mvc;

namespace GamePlan.Api.Controllers
{
	[ApiController]
	[ApiVersion("1.0")]
	[Route("api/v{version:apiVersion}/[controller]")]
	public class AuthController : ControllerBase
	{
		private readonly IAuthService _authService;

		public AuthController(IAuthService authService)
		{
			_authService = authService;
		}

		/// <summary>
		/// Регистрация пользователя
		/// </summary>
		/// <param name="dto"></param>
		/// <remarks>
		///	Метод для регистрации пользователя:
		///	
		///		POST
		///		{
		///			"UserName": "test",
		///			"Password": "Test1!",
		///			"ConfirmPassword": "Test1!"
		///		}
		///		
		///</remarks>
		///<response code="200">Пользователь зарегистрирован</response>
		///<response code="400">Возникла ошибка при регистрации</response>
		[HttpPost("register")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public async Task<ActionResult<BaseResult<UserDto>>> Register([FromBody] RegisterUserDto dto)
		{
			var response = await _authService.Register(dto);

			if (response.isSuccess)
			{
				return Ok(response);
			}

			return BadRequest(response);
		}

		/// <summary>
		/// Авторизация пользователя
		/// </summary>
		/// <param name="dto"></param>
		/// <remarks>
		///	Метод для авторизации пользователя:
		///	
		///		POST
		///		{
		///			"UserName": "test",
		///			"Password": "Test1!"
		///		}
		///		
		///</remarks>
		///<response code="200">Успешная авторизация</response>
		///<response code="400">Возникла ошибка при регистрации авторизации</response>
		[HttpPost("login")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public async Task<ActionResult<BaseResult<TokenDto>>> Login([FromBody] LoginUserDto dto)
		{
			var response = await _authService.Login(dto);

			if (response.isSuccess)
			{
				return Ok(response);
			}

			return BadRequest(response);
		}
	}
}
