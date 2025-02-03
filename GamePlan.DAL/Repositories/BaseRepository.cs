using GamePlan.Domain.Enum;
using GamePlan.Domain.Interfaces.Repositories;
using GamePlan.Domain.OperationException;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

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

		public async Task<TEntity> GetByIdAsync(Guid id)
		{
			var entity = await _dbContext.FindAsync<TEntity>(id);
			if (entity == null)
			{
				throw new OperationException((int)ErrorCodes.EntityNotFound, $"Entity with id {id} not found.");
			}
			return entity;
		}

		public async Task<IEnumerable<TEntity>> CreateRangeAsync(IEnumerable<TEntity> entities)
		{
			ArgumentNullException.ThrowIfNull(entities);

			await _dbContext.AddRangeAsync(entities);
			await _dbContext.SaveChangesAsync();

			return entities;
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

			var existingEntity = await _dbContext.FindAsync<TEntity>(entity.GetType().GetProperty("Id")?.GetValue(entity)) ?? 
				throw new OperationException((int)ErrorCodes.EntityNotFound);
			_dbContext.Entry(existingEntity).CurrentValues.SetValues(entity);
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

		public async Task DeleteByIdAsync(Guid id)
		{
			var entity = await GetByIdAsync(id);
			if (entity != null)
			{
				_dbContext.Remove(entity);
				await _dbContext.SaveChangesAsync();
			}
		}

		public async Task<bool> ExistsAsync(Guid id)
		{
			return await _dbContext.Set<TEntity>().AnyAsync(e => EF.Property<Guid>(e, "Id") == id);
		}

		public IQueryable<TEntity> GetAllWithInclude(params Expression<Func<TEntity, object>>[] includeProperties)
		{
			var query = _dbContext.Set<TEntity>().AsQueryable();
			return includeProperties.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
		}

		public async Task<(IEnumerable<TEntity> Items, int TotalCount)> GetPagedAsync(int page, int pageSize)
		{
			var totalCount = await _dbContext.Set<TEntity>().CountAsync();
			var items = await _dbContext.Set<TEntity>()
				.Skip((page - 1) * pageSize)
				.Take(pageSize)
				.ToListAsync();

			return (items, totalCount);
		}

		public async Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate)
		{
			return await _dbContext.Set<TEntity>().Where(predicate).ToListAsync();
		}

		public async Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate = null)
		{
			return predicate == null
				? await _dbContext.Set<TEntity>().CountAsync()
				: await _dbContext.Set<TEntity>().CountAsync(predicate);
		}

		public async Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
		{
			return await _dbContext.Set<TEntity>().FirstOrDefaultAsync(predicate);
		}
	}
}
