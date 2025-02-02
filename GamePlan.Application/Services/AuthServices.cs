using AutoMapper;
using GamePlan.Application.Resources;
using GamePlan.Domain.Dto;
using GamePlan.Domain.Dto.User;
using GamePlan.Domain.Entity;
using GamePlan.Domain.Enum;
using GamePlan.Domain.Interfaces.Repositories;
using GamePlan.Domain.Interfaces.Services;
using GamePlan.Domain.OperationException;
using GamePlan.Domain.Result;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace GamePlan.Application.Services
{
	public class AuthServices : IAuthService
	{
		private readonly IBaseRepository<Users> _userRepository;
		private readonly IBaseRepository<RolesForUsers> _roleRepository;
		private readonly IBaseRepository<UserInRoles> _userRoleRepository;
		private readonly IBaseRepository<UserToken> _userTokenRepository;
		private readonly ITokenService _tokenService;
		private readonly IMapper _mapper;

		public AuthServices(IBaseRepository<Users> userRepository, IMapper mapper, IBaseRepository<RolesForUsers> roleRepository,
			IBaseRepository<UserInRoles> userRoleRepository, ITokenService tokenService, IBaseRepository<UserToken> userTokenRepository)
		{
			_userRepository = userRepository;
			_mapper = mapper;
			_roleRepository = roleRepository;
			_userRoleRepository = userRoleRepository;
			_tokenService = tokenService;
			_userTokenRepository = userTokenRepository;
		}

		public async Task<BaseResult<UserDto>> Register(RegisterUserDto dto)
		{
			var user = await _userRepository.GetAll().FirstOrDefaultAsync(x => x.UserName == dto.UserName);

			if (user != null)
			{
				throw new OperationException((int)ErrorCodes.UserAlreadyExists);
			}

			var hashUserPassword = HashPassword(dto.Password);
			user = new Users()
			{
				UserName = dto.UserName,
				PasswordHash = hashUserPassword
			};

			await _userRepository.CreateAsync(user);

			var role = await _roleRepository.GetAll().FirstOrDefaultAsync(x => x.Name == "User");

			if (role == null)
			{
				throw new OperationException((int)ErrorCodes.RoleNotFound);
			}

			var userRole = new UserInRoles()
			{
				UserId = user.Id,
				RoleId = role.Id
			};

			await _userRoleRepository.CreateAsync(userRole);

			return new BaseResult<UserDto>()
			{
				Data = _mapper.Map<UserDto>(user)
			};
		}

		public async Task<BaseResult<TokenDto>> Login(LoginUserDto dto)
		{
			var user = await _userRepository.GetAll()
					.Include(x => x.Roles)
					.FirstOrDefaultAsync(x => x.UserName == dto.UserName);

			if (user == null)
			{
				throw new OperationException((int)ErrorCodes.UserNotFound);
			}

			if (!IsVerifiedPassword(user.PasswordHash, dto.Password))
			{
				throw new OperationException((int)ErrorCodes.IncorrectPassword);
			}

			var userToken = await _userTokenRepository.GetAll().FirstOrDefaultAsync(x => x.UserId == user.Id);

			var userRoles = user.Roles;
			var claims = userRoles.Select(x => new Claim(ClaimTypes.Role, x.Name)).ToList();
			claims.Add(new Claim(ClaimTypes.Name, user.UserName));
			var accessToken = _tokenService.GenerateAccessToken(claims);
			var refreshToken = _tokenService.GenerateRefreshToken();

			if (userToken == null)
			{
				userToken = new UserToken()
				{
					UserId = user.Id,
					RefreshToken = refreshToken,
					RefreshTokenExpiredTime = DateTime.UtcNow.AddDays(7)
				};
				await _userTokenRepository.CreateAsync(userToken);
			}
			else
			{
				userToken.RefreshToken = refreshToken;
				userToken.RefreshTokenExpiredTime = DateTime.UtcNow.AddDays(7);

				await _userTokenRepository.UpdateAsync(userToken);
			}

			return new BaseResult<TokenDto>()
			{
				Data = new TokenDto(accessToken, refreshToken)
			};
		}

		private string HashPassword(string password)
		{
			MD5 crypt = MD5.Create();
			byte[] code = crypt.ComputeHash(Encoding.UTF8.GetBytes(password));
			StringBuilder strings = new StringBuilder();
			for (int i = 0; i < code.Length; i++)
			{
				strings.Append(code[i].ToString("x2"));
			}
			return strings.ToString();
		}

		private bool IsVerifiedPassword(string userPasswordHash, string userPassword)
		{
			var hash = HashPassword(userPassword);
			return hash == userPasswordHash;
		}
	}
}
