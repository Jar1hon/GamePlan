using GamePlan.Domain.Dto;
using GamePlan.Domain.Result;
using System.Security.Claims;

namespace GamePlan.Domain.Interfaces.Services
{
	/// <summary>
	/// Сервис для управления токенами
	/// </summary>
	public interface ITokenService
	{
		/// <summary>
		/// Генерация access-токена
		/// </summary>
		/// <param name="claims"></param>
		/// <returns></returns>
		string GenerateAccessToken(IEnumerable<Claim> claims);

		/// <summary>
		/// Генерация refresh-токена
		/// </summary>
		/// <returns></returns>
		string GenerateRefreshToken();

		/// <summary>
		/// Получение клаймов истекающего токена
		/// </summary>
		/// <param name="accessToken"></param>
		/// <returns></returns>
		ClaimsPrincipal GetPrincipalFromExpiredToken(string accessToken);

		/// <summary>
		/// Обновление refresh-токена
		/// </summary>
		/// <param name="dto"></param>
		/// <returns></returns>
		Task<BaseResult<TokenDto>> RefreshToken(TokenDto dto);
	}
}
