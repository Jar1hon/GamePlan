namespace GamePlan.Domain.Interfaces.Repositories
{
	public interface IBaseRepository<TEntity>
	{
		IQueryable<TEntity> GetAll();
		Task<TEntity> CreateAsync(TEntity entity);
		TEntity Update(TEntity entity);
		void Delete(TEntity entity);
		Task<int> SaveChangeAsync(TEntity entity);
	}
}
