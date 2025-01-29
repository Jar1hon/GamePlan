using AutoMapper;
using GamePlan.Application.Resources;
using GamePlan.Domain.Dto.User;
using GamePlan.Domain.Entity;
using GamePlan.Domain.Enum;
using GamePlan.Domain.Interfaces.Repositories;
using GamePlan.Domain.Interfaces.Services;
using GamePlan.Domain.Result;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace GamePlan.Application.Services
{
	public class AuthServices : IAuthService
	{
		private readonly IBaseRepository<Users> _userRepository;
		private readonly IBaseRepository<RolesForUsers> _roleRepository;
		private readonly IBaseRepository<UserInRoles> _userRoleRepository;
		private readonly IMapper _mapper;

		public AuthServices(IBaseRepository<Users> userRepository, IMapper mapper, IBaseRepository<RolesForUsers> roleRepository,
			IBaseRepository<UserInRoles> userRoleRepository)
		{
			_userRepository = userRepository;
			_mapper = mapper;
			_roleRepository = roleRepository;
			_userRoleRepository = userRoleRepository;
		}

		public async Task<BaseResult<UserDto>> Register(RegisterUserDto dto)
		{
			if (dto.Password != dto.PasswordConfirm)
			{
				return new BaseResult<UserDto>()
				{
					ErrorMessage = ErrorMessage.PasswordsNotEquals,
					ErrorCode = (int)ErrorCodes.PasswordsNotEquals
				};
			}

			var user = await _userRepository.GetAll().FirstOrDefaultAsync(x => x.UserName == dto.UserName);

			if (user != null)
			{
				return new BaseResult<UserDto>()
				{
					ErrorMessage = ErrorMessage.UserAlreadyExists,
					ErrorCode = (int)ErrorCodes.UserAlreadyExists
				};
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
				return new BaseResult<UserDto>()
				{
					ErrorMessage = ErrorMessage.RoleNotFound,
					ErrorCode = (int)ErrorCodes.RoleNotFound
				};
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
	}
}
