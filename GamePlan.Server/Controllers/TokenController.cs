using GamePlan.Domain.Dto;
using GamePlan.Domain.Interfaces.Services;
using GamePlan.Domain.Result;
using Microsoft.AspNetCore.Mvc;

namespace GamePlan.Api.Controllers
{
	[ApiController]
	public class TokenController : ControllerBase
	{
		private readonly ITokenService _tokenService;

		public TokenController(ITokenService tokenService)
		{
			_tokenService = tokenService;
		}

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
