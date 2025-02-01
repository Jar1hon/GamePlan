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
		/// <returns></returns>
		[HttpPost("register")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public async Task<ActionResult<BaseResult<UserDto>>> Register([FromBody] RegisterUserDto dto)
		{
			var response = await _authService.Register(dto);

			if (response.IsSucces)
			{
				return Ok(response);
			}

			return BadRequest(response);
		}

		/// <summary>
		/// Логин пользователя
		/// </summary>
		/// <param name="dto"></param>
		/// <returns></returns>
		[HttpPost("login")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public async Task<ActionResult<BaseResult<TokenDto>>> Login([FromBody] LoginUserDto dto)
		{
			var response = await _authService.Login(dto);

			if (response.IsSucces)
			{
				return Ok(response);
			}

			return BadRequest(response);
		}
	}
}
