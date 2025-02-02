namespace GamePlan.Domain.Interfaces.Repositories
{
	public interface IBaseRepository<TEntity>
	{
		IQueryable<TEntity> GetAll();

		Task<TEntity> GetById(Guid id);

		Task<TEntity> CreateAsync(TEntity entity);

		Task<TEntity> UpdateAsync(TEntity entity);

		Task<TEntity> DeleteAsync(TEntity entity);
	}
}
