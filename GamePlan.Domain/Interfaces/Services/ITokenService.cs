using GamePlan.Domain.Dto;
using GamePlan.Domain.Result;
using System.Security.Claims;

namespace GamePlan.Domain.Interfaces.Services
{
	public interface ITokenService
	{
		string GenerateAccessToken(IEnumerable<Claim> claims);

		string GenerateRefreshToken();

		ClaimsPrincipal GetPrincipalFromExpiredToken(string accessToken);

		Task<BaseResult<TokenDto>> RefreshToken(TokenDto dto);
	}
}
