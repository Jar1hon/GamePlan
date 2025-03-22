using GamePlan.Domain.Entity;
using GamePlan.Domain.Interfaces.DataBaseses;
using GamePlan.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore.Storage;

namespace GamePlan.DAL.Repositories
{
	public class UnitOfWork : IUnitOfWork
	{
		private readonly ApplicationDbContext _context;

		public IBaseRepository<Users> Users { get; set; }

		public IBaseRepository<Roles> Roles { get; set; }

		public IBaseRepository<UserInRoles> UserRoles { get; set; }

		public UnitOfWork(ApplicationDbContext context, IBaseRepository<Users> users, IBaseRepository<Roles> roles, IBaseRepository<UserInRoles> userRoles)
		{
			_context = context;
			Users = users;
			Roles = roles;
			UserRoles = userRoles;
		}

		public async Task<IDbContextTransaction> BeginTransactionAsync()
		{
			return await _context.Database.BeginTransactionAsync();
		}

		public async Task<int> SaveChangesAsync()
		{
			return await _context.SaveChangesAsync();
		}
	}
}
