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

			return entity;
		}

		public TEntity Update(TEntity entity)
		{
			ArgumentNullException.ThrowIfNull(entity);

			_dbContext.Update(entity);

			return entity;
		}

		public void Delete(TEntity entity)
		{
			ArgumentNullException.ThrowIfNull(entity);

			_dbContext.Remove(entity);
		}

		public async Task<int> SaveChangeAsync(TEntity entity)
		{
			return await _dbContext.SaveChangesAsync();
		}
	}
}
