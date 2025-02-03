using GamePlan.Domain.Entity;
using GamePlan.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore.Storage;
using System.Data;

namespace GamePlan.Domain.Interfaces.DataBaseses
{
	public interface IUnitOfWork
	{
		Task<IDbContextTransaction> BeginTransactionAsync();

		IBaseRepository<Users> Users { get; set; }

		IBaseRepository<RolesForUsers> Roles { get; set; }

		IBaseRepository<UserInRoles> UserRoles { get; set; }

		Task<int> SaveChangesAsync();
	}
}
