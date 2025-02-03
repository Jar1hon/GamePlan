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
	public class RolesForUsersServices : IRolesForUsersService
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IBaseRepository<Users> _userRepository;
		private readonly IBaseRepository<RolesForUsers> _roleRepository;
		private readonly IBaseRepository<UserInRoles> _userRoleRepository;
		private readonly IMapper _mapper;

		public RolesForUsersServices(IBaseRepository<RolesForUsers> roleRepository, IBaseRepository<Users> userRepository, IMapper mapper,
			IBaseRepository<UserInRoles> userRoleRepository, IUnitOfWork unitOfWork)
		{
			_roleRepository = roleRepository;
			_userRepository = userRepository;
			_mapper = mapper;
			_userRoleRepository = userRoleRepository;
			_unitOfWork = unitOfWork;
		}

		public async Task<BaseResult<RolesForUsersDto>> CreateRoleAsync(CreateRolesForUsersDto dto)
		{
			var role = await _roleRepository.GetAll().FirstOrDefaultAsync(x => x.Name == dto.Name);

			if (role != null)
			{
				return new BaseResult<RolesForUsersDto>()
				{
					ErrorMessage = ErrorMessage.RoleAlreadyExists,
					ErrorCode = (int)ErrorCodes.RoleAlreadyExists
				};
			}

			role = new RolesForUsers()
			{
				Name = dto.Name,
				Description = dto.Description,
			};

			await _roleRepository.CreateAsync(role);

			return new BaseResult<RolesForUsersDto>()
			{
				Data = _mapper.Map<RolesForUsersDto>(role)
			};
		}

		public async Task<BaseResult<RolesForUsersDto>> DeleteRoleAsync(Guid id)
		{
			var role = await _roleRepository.GetAll().FirstOrDefaultAsync(x => x.Id == id);

			if (role == null)
			{
				return new BaseResult<RolesForUsersDto>()
				{
					ErrorMessage = ErrorMessage.RoleNotFound,
					ErrorCode = (int)ErrorCodes.RoleNotFound
				};
			};

			await _roleRepository.DeleteAsync(role);

			return new BaseResult<RolesForUsersDto>()
			{
				Data = _mapper.Map<RolesForUsersDto>(role)
			};
		}

		public async Task<BaseResult<RolesForUsersDto>> UpdateRoleAsync(RolesForUsersDto dto)
		{
			var role = await _roleRepository.GetAll().FirstOrDefaultAsync(x => x.Id == dto.Id);

			if (role == null)
			{
				return new BaseResult<RolesForUsersDto>()
				{
					ErrorMessage = ErrorMessage.RoleNotFound,
					ErrorCode = (int)ErrorCodes.RoleNotFound
				};
			};

			role.Name = dto.Name;
			role.Description = dto.Description;

			var updatedRole = await _roleRepository.UpdateAsync(role);

			return new BaseResult<RolesForUsersDto>()
			{
				Data = _mapper.Map<RolesForUsersDto>(updatedRole)
			};
		}

		public async Task<BaseResult<UsersInRolesDto>> AddRoleForUserAsync(UsersInRolesDto dto)
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

		public async Task<BaseResult<UsersInRolesDto>> DeleteRoleForUserAsync(DeleteUserRoleDto dto)
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

		public async Task<BaseResult<UsersInRolesDto>> UpdateRoleForUserAsync(UpdateUserRoleDto dto)
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

		public Task<BaseResult<List<RolesForUsersDto>>> GetAllRolesAsync()
		{
			throw new NotImplementedException();
		}

		public Task<BaseResult<RolesForUsersDto>> GetRoleByIdAsync(Guid id)
		{
			throw new NotImplementedException();
		}

		public Task<BaseResult<List<RolesForUsersDto>>> GetUserRolesAsync(Guid userId)
		{
			throw new NotImplementedException();
		}

		public Task<BaseResult<List<UserDto>>> GetUsersInRoleAsync(Guid roleId)
		{
			throw new NotImplementedException();
		}

		public Task<BaseResult<bool>> IsUserInRoleAsync(Guid userId, Guid roleId)
		{
			throw new NotImplementedException();
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
				Data = _mapper.Map<List<UserWithRolesDto>>(users)
			};
		}

		public Task<BaseResult<RolesForUsersDto>> GetRoleByNameAsync(string roleName)
		{
			throw new NotImplementedException();
		}

		public Task<BaseResult<bool>> DeleteAllRolesForUserAsync(Guid userId)
		{
			throw new NotImplementedException();
		}

		public Task<BaseResult<List<RolesForUsersDto>>> GetRolesWithPaginationAsync(int page, int pageSize)
		{
			throw new NotImplementedException();
		}

		public Task<BaseResult<List<UserWithRolesDto>>> GetUsersWithRolesPaginationAsync(int page, int pageSize)
		{
			throw new NotImplementedException();
		}

		public Task<BaseResult<int>> GetUserCountInRoleAsync(Guid roleId)
		{
			throw new NotImplementedException();
		}

		public Task<BaseResult<List<RoleWithUserCountDto>>> GetAllRolesWithUserCountAsync()
		{
			throw new NotImplementedException();
		}

		public Task<BaseResult<List<RolesForUsersDto>>> GetRolesByFilterAsync(RoleFilterDto filter)
		{
			throw new NotImplementedException();
		}

		public Task<BaseResult<List<UserWithRolesDto>>> GetUsersWithRolesByFilterAsync(UserRoleFilterDto filter)
		{
			throw new NotImplementedException();
		}
	}
}
