﻿using GamePlan.Domain.Dto;
using GamePlan.Domain.Dto.User;
using GamePlan.Domain.Interfaces.Services;
using GamePlan.Domain.Result;
using Microsoft.AspNetCore.Mvc;

namespace GamePlan.Api.Controllers
{
	[ApiController]
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
