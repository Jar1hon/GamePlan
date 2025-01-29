using GamePlan.Domain.Interfaces.Repositories;

namespace GamePlan.DAL.Repositories
{
	public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
	{
		private readonly ApplicationDbContext _dbContext;

		public BaseRepository(ApplicationDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		public IQueryable<TEntity> GetAll()
		{
			return _dbContext.Set<TEntity>();
		}

		public async Task<TEntity> CreateAsync(TEntity entity)
		{
			ArgumentNullException.ThrowIfNull(entity);

			await _dbContext.AddAsync(entity);
			await _dbContext.SaveChangesAsync();

			return entity;
		}

		public async Task<TEntity> UpdateAsync(TEntity entity)
		{
			ArgumentNullException.ThrowIfNull(entity);

			_dbContext.Update(entity);
			await _dbContext.SaveChangesAsync();

			return entity;
		}

		public async Task<TEntity> DeleteAsync(TEntity entity)
		{
			ArgumentNullException.ThrowIfNull(entity);

			_dbContext.Remove(entity);
			await _dbContext.SaveChangesAsync();

			return entity;
		}
	}
}
