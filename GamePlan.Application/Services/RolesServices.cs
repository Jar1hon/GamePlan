using AutoMapper;
using AutoMapper.QueryableExtensions;
using GamePlan.Application.Resources;
using GamePlan.Domain.Dto.Role;
using GamePlan.Domain.Dto.RolesForUsers;
using GamePlan.Domain.Dto.User;
using GamePlan.Domain.Dto.UserRole;
using GamePlan.Domain.Entity;
using GamePlan.Domain.Enum;
using GamePlan.Domain.Interfaces.DataBaseses;
using GamePlan.Domain.Interfaces.Repositories;
using GamePlan.Domain.Interfaces.Services;
using GamePlan.Domain.Result;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace GamePlan.Application.Services
{
	public class RolesServices : IRolesService
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IBaseRepository<Users> _userRepository;
		private readonly IBaseRepository<Roles> _roleRepository;
		private readonly IBaseRepository<UserInRoles> _userRoleRepository;
		private readonly IMapper _mapper;

		public RolesServices(IBaseRepository<Roles> roleRepository, IBaseRepository<Users> userRepository, IMapper mapper,
			IBaseRepository<UserInRoles> userRoleRepository, IUnitOfWork unitOfWork)
		{
			_roleRepository = roleRepository;
			_userRepository = userRepository;
			_mapper = mapper;
			_userRoleRepository = userRoleRepository;
			_unitOfWork = unitOfWork;
		}

		public async Task<BaseResult<RolesDto>> CreateRoleAsync(CreateRolesDto dto)
		{
			var role = await _roleRepository.GetAll().FirstOrDefaultAsync(x => x.Name == dto.Name);

			if (role != null)
			{
				return new BaseResult<RolesDto>()
				{
					ErrorMessage = ErrorMessage.RoleAlreadyExists,
					ErrorCode = (int)ErrorCodes.RoleAlreadyExists
				};
			}

			role = new Roles()
			{
				Name = dto.Name,
				Description = dto.Description,
			};

			await _roleRepository.CreateAsync(role);

			return new BaseResult<RolesDto>()
			{
				Data = _mapper.Map<RolesDto>(role)
			};
		}

		public async Task<BaseResult<RolesDto>> DeleteRoleAsync(Guid id)
		{
			var role = await _roleRepository.GetAll().FirstOrDefaultAsync(x => x.Id == id);

			if (role == null)
			{
				return new BaseResult<RolesDto>()
				{
					ErrorMessage = ErrorMessage.RoleNotFound,
					ErrorCode = (int)ErrorCodes.RoleNotFound
				};
			};

			await _roleRepository.DeleteAsync(role);

			return new BaseResult<RolesDto>()
			{
				Data = _mapper.Map<RolesDto>(role)
			};
		}

		public async Task<BaseResult<RolesDto>> UpdateRoleAsync(RolesDto dto)
		{
			var role = await _roleRepository.GetAll().FirstOrDefaultAsync(x => x.Id == dto.Id);

			if (role == null)
			{
				return new BaseResult<RolesDto>()
				{
					ErrorMessage = ErrorMessage.RoleNotFound,
					ErrorCode = (int)ErrorCodes.RoleNotFound
				};
			};

			role.Name = dto.Name;
			role.Description = dto.Description;

			var updatedRole = await _roleRepository.UpdateAsync(role);

			return new BaseResult<RolesDto>()
			{
				Data = _mapper.Map<RolesDto>(updatedRole)
			};
		}

		public async Task<BaseResult<UsersInRolesDto>> AddRoleToUserAsync(UsersInRolesDto dto)
		{
			var user = await _userRepository.GetAll()
				.Include(x => x.Roles)
				.FirstOrDefaultAsync(x => x.UserName == dto.Login);

			if (user == null)
			{
				return new BaseResult<UsersInRolesDto>()
				{
					ErrorMessage = ErrorMessage.UserNotFound,
					ErrorCode = (int)ErrorCodes.UserNotFound
				};
			}

			var roles = user.Roles.Select(x => x.Name).ToArray();

			if (roles.All(x => x != dto.Role))
			{
				var role = await _roleRepository.GetAll().FirstOrDefaultAsync(x => x.Name == dto.Role);

				if (role == null)
				{
					return new BaseResult<UsersInRolesDto>()
					{
						ErrorMessage = ErrorMessage.RoleNotFound,
						ErrorCode = (int)ErrorCodes.RoleNotFound
					};
				}

				UserInRoles userRole = new UserInRoles()
				{
					RoleId = role.Id,
					UserId = user.Id
				};

				await _userRoleRepository.CreateAsync(userRole);

				return new BaseResult<UsersInRolesDto>()
				{
					Data = new UsersInRolesDto(dto.Login, dto.Role)
				};
			}

			return new BaseResult<UsersInRolesDto>()
			{
				ErrorMessage = ErrorMessage.UserAlreadyExistsWithThisRole,
				ErrorCode = (int)ErrorCodes.UserAlreadyExistsWithThisRole
			};
		}

		public async Task<BaseResult<UsersInRolesDto>> DeleteRoleFromUserAsync(DeleteUserRoleDto dto)
		{
			var user = await _userRepository.GetAll()
				.Include(x => x.Roles)
				.FirstOrDefaultAsync(x => x.UserName == dto.Login);

			if (user == null)
			{
				return new BaseResult<UsersInRolesDto>()
				{
					ErrorMessage = ErrorMessage.UserNotFound,
					ErrorCode = (int)ErrorCodes.UserNotFound
				};
			}

			var role = user.Roles.FirstOrDefault(x => x.Name == dto.Role);

			if (role == null)
			{
				return new BaseResult<UsersInRolesDto>()
				{
					ErrorMessage = ErrorMessage.RoleNotFound,
					ErrorCode = (int)ErrorCodes.RoleNotFound
				};
			}

			var userRole = await _userRoleRepository.GetAll()
				.Where(x => x.RoleId == role.Id)
				.FirstOrDefaultAsync(x => x.UserId == user.Id);

			await _userRoleRepository.DeleteAsync(userRole);

			return new BaseResult<UsersInRolesDto>()
			{
				Data = new UsersInRolesDto(user.UserName, role.Name)
			};
		}

		public async Task<BaseResult<UsersInRolesDto>> UpdateRoleToUserAsync(UpdateUserRoleDto dto)
		{
			var user = await _userRepository.GetAll()
				.Include(x => x.Roles)
				.FirstOrDefaultAsync(x => x.UserName == dto.Login);

			if (user == null)
			{
				return new BaseResult<UsersInRolesDto>()
				{
					ErrorMessage = ErrorMessage.UserNotFound,
					ErrorCode = (int)ErrorCodes.UserNotFound
				};
			}

			var role = user.Roles.FirstOrDefault(x => x.Name == dto.FromRole);

			if (role == null)
			{
				return new BaseResult<UsersInRolesDto>()
				{
					ErrorMessage = ErrorMessage.RoleNotFound,
					ErrorCode = (int)ErrorCodes.RoleNotFound
				};
			}

			var newRoleForUser = await _roleRepository.GetAll()
				.FirstOrDefaultAsync(x => x.Name == dto.ToRole);

			if (newRoleForUser == null)
			{
				return new BaseResult<UsersInRolesDto>()
				{
					ErrorMessage = ErrorMessage.RoleNotFound,
					ErrorCode = (int)ErrorCodes.RoleNotFound
				};
			}

			using (var transaction = await _unitOfWork.BeginTransactionAsync())
			{
				try
				{
					var userRole = await _unitOfWork.UserRoles.GetAll()
						.Where(x => x.RoleId == role.Id)
						.FirstAsync(x => x.UserId == user.Id);

					await _unitOfWork.UserRoles.DeleteAsync(userRole);

					var newUserRole = new UserInRoles()
					{
						UserId = user.Id,
						RoleId = newRoleForUser.Id
					};

					await _unitOfWork.UserRoles.CreateAsync(newUserRole);
					await transaction.CommitAsync();
				}
				catch (Exception)
				{
					await transaction.RollbackAsync();
				}
			}

			return new BaseResult<UsersInRolesDto>()
			{
				Data = new UsersInRolesDto(user.UserName, newRoleForUser.Name)
			};
		}

		public async Task<BaseResult<List<RolesDto>>> GetAllRolesAsync()
		{
			var roles = await _roleRepository.GetAll()
				.ToListAsync();

			var roleDtos = _mapper.Map<List<RolesDto>>(roles);

			return new BaseResult<List<RolesDto>>()
			{
				Data = roleDtos
			};
		}

		public async Task<BaseResult<RolesDto>> GetRoleByRoleIdAsync(Guid roleId)
		{
			var role = await _roleRepository.GetByIdAsync(roleId);

			if (role == null)
			{
				return new BaseResult<RolesDto>()
				{
					ErrorMessage = ErrorMessage.RoleNotFound,
					ErrorCode = (int)ErrorCodes.RoleNotFound
				};
			}

			return new BaseResult<RolesDto>()
			{
				Data = _mapper.Map<RolesDto>(role)
			};
		}

		public async Task<BaseResult<List<UserWithRolesDto>>> GetRolesByUserIdAsync(Guid userId)
		{
			var roles = await _userRepository.GetAll()
				.Include(u => u.Roles)
				.Where(u => u.Id == userId)
				.ProjectTo<UserWithRolesDto>(_mapper.ConfigurationProvider)
				.ToListAsync();

			var roleDtos = _mapper.Map<List<UserWithRolesDto>>(roles);

			return new BaseResult<List<UserWithRolesDto>>()
			{
				Data = roleDtos
			};
		}

		public async Task<BaseResult<List<UserDto>>> GetUsersByRoleIdAsync(Guid roleId)
		{
			var users = await _roleRepository.GetAll()
				.Include(x => x.User)
				.Where(x => x.Id == roleId)
				.ToListAsync();

			var userDtos = _mapper.Map<List<UserDto>>(users);

			return new BaseResult<List<UserDto>>()
			{
				Data = userDtos
			};
		}

		public async Task<BaseResult<bool>> IsUserInRoleAsync(Guid userId, Guid roleId)
		{
			var user = await _userRepository.GetAll()
				.Include(x => x.Roles)
				.FirstOrDefaultAsync(x => x.Id == userId);

			var roles = user.Roles.Where(x => x.Id == roleId);

			return new BaseResult<bool>()
			{
				Data = roles.Any()
			};
		}

		public async Task<BaseResult<List<UserWithRolesDto>>> GetAllUsersWithRolesAsync()
		{
			var users = await _userRepository.GetAll()
				.Include(u => u.Roles)
				.ThenInclude(ur => ur.User)
				.ProjectTo<UserWithRolesDto>(_mapper.ConfigurationProvider)
				.ToListAsync();

			var userDtos = _mapper.Map<List<UserWithRolesDto>>(users);

			return new BaseResult<List<UserWithRolesDto>>()
			{
				Data = userDtos
			};
		}

		public async Task<BaseResult<RolesDto>> GetRoleIdByNameAsync(string roleName)
		{
			var role = await _roleRepository.GetAll()
				.FirstOrDefaultAsync(x => x.Name == roleName);

			if (role == null)
			{
				return new BaseResult<RolesDto>()
				{
					ErrorMessage = ErrorMessage.RoleNotFound,
					ErrorCode = (int)ErrorCodes.RoleNotFound
				};
			}

			var roleDto = _mapper.Map<RolesDto>(role);

			return new BaseResult<RolesDto>()
			{
				Data = roleDto
			};
		}
	}
}
