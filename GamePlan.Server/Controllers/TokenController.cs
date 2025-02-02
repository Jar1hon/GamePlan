using Asp.Versioning;
using GamePlan.Domain.Dto;
using GamePlan.Domain.Interfaces.Services;
using GamePlan.Domain.Result;
using Microsoft.AspNetCore.Mvc;

namespace GamePlan.Api.Controllers
{
	[ApiController]
	[ApiVersion("1.0")]
	[Route("api/v{version:apiVersion}/[controller]")]
	public class TokenController : ControllerBase
	{
		private readonly ITokenService _tokenService;

		public TokenController(ITokenService tokenService)
		{
			_tokenService = tokenService;
		}

		/// <summary>
		/// Обновление refresh-токена
		/// </summary>
		/// <param name="dto"></param>
		/// <remarks>
		///	Метод для обновления refresh-токена:
		///	
		///		POST
		///		{
		///			"accessToken": "string",
		///			"refreshToken": "string"
		///		}
		///		
		///</remarks>
		///<response code="200">Токен обновлен</response>
		///<response code="400">При обновлении токена произшла ошибка</response>
		[HttpPost]
		[Route("refresh")]
		public async Task<ActionResult<BaseResult<TokenDto>>> RefreshToken([FromBody] TokenDto dto)
		{
			var response = await _tokenService.RefreshToken(dto);

			if (response.IsSucces)
			{
				return Ok(response);
			}

			return BadRequest(response);
		}
	}
}
