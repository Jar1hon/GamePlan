﻿using GamePlan.Application.Resources;
using GamePlan.Domain.Dto;
using GamePlan.Domain.Entity;
using GamePlan.Domain.Interfaces.Repositories;
using GamePlan.Domain.Interfaces.Services;
using GamePlan.Domain.Result;
using GamePlan.Domain.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace GamePlan.Application.Services
{
	internal class TokenServices : ITokenService
	{
		private readonly IBaseRepository<Users> _userRepository;
		private readonly string _jwtKey;
		private readonly string _issuer;
		private readonly string _audience;

		public TokenServices(IOptions<JwtSettings> options, IBaseRepository<Users> userRepository)
		{
			_jwtKey = options.Value.JwtKey;
			_issuer = options.Value.Issuer;
			_audience = options.Value.Audience;
			_userRepository = userRepository;
		}

		public string GenerateAccessToken(IEnumerable<Claim> claims)
		{
			var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtKey));
			var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
			var securityToken =
				new JwtSecurityToken(_issuer, _audience, claims, null, DateTime.UtcNow.AddMinutes(10), credentials);
			var token = new JwtSecurityTokenHandler().WriteToken(securityToken);
			return token;
		}

		public string GenerateRefreshToken()
		{
			var randomNumbers = new byte[32];
			using var randomNumberGenerator = RandomNumberGenerator.Create();
			randomNumberGenerator.GetBytes(randomNumbers);
			return Convert.ToBase64String(randomNumbers);
		}

		public ClaimsPrincipal GetPrincipalFromExpiredToken(string accessToken)
		{
			var tokenValidationParameters = new TokenValidationParameters()
			{
				ValidateAudience = true,
				ValidateIssuer = true,
				ValidateIssuerSigningKey = true,
				IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtKey)),
				ValidateLifetime = true,
				ValidAudience = _audience,
				ValidIssuer = _issuer
			};
			var tokenHandler = new JwtSecurityTokenHandler();
			var claimsPrincipal = tokenHandler.ValidateToken(accessToken, tokenValidationParameters, out var securityToken);

			if (securityToken is not JwtSecurityToken jwtSecurityToken || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256,
				StringComparison.InvariantCultureIgnoreCase))
			{
				throw new SecurityTokenException(ErrorMessage.InvalidToken);
			}
			return claimsPrincipal;
		}

		public async Task<BaseResult<TokenDto>> RefreshToken(TokenDto dto)
		{
			var accessToken = dto.AccessToken;
			var refreshToken = dto.RefreshToken;

			var claimsPrincipal = GetPrincipalFromExpiredToken(accessToken);
			var userName = claimsPrincipal.Identity?.Name;

			var user = await _userRepository.GetAll()
				.Include(x => x.UserToken)
				.FirstOrDefaultAsync(x => x.UserName == userName);

			if (user == null || user.UserToken.RefreshToken != refreshToken
				|| user.UserToken.RefreshTokenExpiredTime <= DateTime.UtcNow)
			{
				return new BaseResult<TokenDto>
				{
					ErrorMessage = ErrorMessage.InvalidClientRequest
				};
			}

			var newAccessToken = GenerateAccessToken(claimsPrincipal.Claims);
			var newRefreshToken = GenerateRefreshToken();

			user.UserToken.RefreshToken = newRefreshToken;

			await _userRepository.UpdateAsync(user);

			return new BaseResult<TokenDto>
			{
				Data = new TokenDto(accessToken, newRefreshToken)
			};
		}
	}
}
