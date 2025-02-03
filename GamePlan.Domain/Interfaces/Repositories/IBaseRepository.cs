using System.Linq.Expressions;

namespace GamePlan.Domain.Interfaces.Repositories
{
	/// <summary>
	/// Базовый интерфейс репозитория для работы с сущностями типа <typeparamref name="TEntity"/>.
	/// </summary>
	/// <typeparam name="TEntity">Тип сущности.</typeparam>
	public interface IBaseRepository<TEntity> where TEntity : class
	{
		/// <summary>
		/// Получает все сущности типа <typeparamref name="TEntity"/>.
		/// </summary>
		/// <returns>Запрос для получения всех сущностей.</returns>
		IQueryable<TEntity> GetAll();

		/// <summary>
		/// Получает сущность по её уникальному идентификатору.
		/// </summary>
		/// <param name="id">Уникальный идентификатор сущности.</param>
		/// <returns>Сущность типа <typeparamref name="TEntity"/>.</returns>
		/// <exception cref="OperationException">Выбрасывается, если сущность не найдена.</exception>
		Task<TEntity> GetByIdAsync(Guid id);

		/// <summary>
		/// Создаёт новую сущность типа <typeparamref name="TEntity"/>.
		/// </summary>
		/// <param name="entity">Сущность для создания.</param>
		/// <returns>Созданная сущность.</returns>
		/// <exception cref="ArgumentNullException">Выбрасывается, если <paramref name="entity"/> равен <c>null</c>.</exception>
		Task<TEntity> CreateAsync(TEntity entity);

		/// <summary>
		/// Создаёт несколько сущностей типа <typeparamref name="TEntity"/>.
		/// </summary>
		/// <param name="entities">Коллекция сущностей для создания.</param>
		/// <returns>Коллекция созданных сущностей.</returns>
		/// <exception cref="ArgumentNullException">Выбрасывается, если <paramref name="entities"/> равен <c>null</c>.</exception>
		Task<IEnumerable<TEntity>> CreateRangeAsync(IEnumerable<TEntity> entities);

		/// <summary>
		/// Обновляет существующую сущность типа <typeparamref name="TEntity"/>.
		/// </summary>
		/// <param name="entity">Сущность для обновления.</param>
		/// <returns>Обновлённая сущность.</returns>
		/// <exception cref="ArgumentNullException">Выбрасывается, если <paramref name="entity"/> равен <c>null</c>.</exception>
		/// <exception cref="NotFoundException">Выбрасывается, если сущность не найдена.</exception>
		Task<TEntity> UpdateAsync(TEntity entity);

		/// <summary>
		/// Удаляет сущность типа <typeparamref name="TEntity"/>.
		/// </summary>
		/// <param name="entity">Сущность для удаления.</param>
		/// <returns>Удалённая сущность.</returns>
		/// <exception cref="ArgumentNullException">Выбрасывается, если <paramref name="entity"/> равен <c>null</c>.</exception>
		Task<TEntity> DeleteAsync(TEntity entity);

		/// <summary>
		/// Удаляет сущность по её уникальному идентификатору.
		/// </summary>
		/// <param name="id">Уникальный идентификатор сущности.</param>
		/// <exception cref="NotFoundException">Выбрасывается, если сущность не найдена.</exception>
		Task DeleteByIdAsync(Guid id);

		/// <summary>
		/// Проверяет, существует ли сущность с указанным уникальным идентификатором.
		/// </summary>
		/// <param name="id">Уникальный идентификатор сущности.</param>
		/// <returns><c>true</c>, если сущность существует; иначе <c>false</c>.</returns>
		Task<bool> ExistsAsync(Guid id);

		/// <summary>
		/// Получает все сущности типа <typeparamref name="TEntity"/> с включением связанных данных.
		/// </summary>
		/// <param name="includeProperties">Свойства для включения в запрос.</param>
		/// <returns>Запрос для получения сущностей с включёнными данными.</returns>
		IQueryable<TEntity> GetAllWithInclude(params Expression<Func<TEntity, object>>[] includeProperties);

		/// <summary>
		/// Получает сущности типа <typeparamref name="TEntity"/> с пагинацией.
		/// </summary>
		/// <param name="page">Номер страницы.</param>
		/// <param name="pageSize">Количество элементов на странице.</param>
		/// <returns>Кортеж, содержащий коллекцию сущностей и общее количество элементов.</returns>
		Task<(IEnumerable<TEntity> Items, int TotalCount)> GetPagedAsync(int page, int pageSize);

		/// <summary>
		/// Ищет сущности типа <typeparamref name="TEntity"/> по указанному условию.
		/// </summary>
		/// <param name="predicate">Условие для поиска.</param>
		/// <returns>Коллекция сущностей, удовлетворяющих условию.</returns>
		Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate);

		/// <summary>
		/// Получает количество сущностей типа <typeparamref name="TEntity"/>.
		/// </summary>
		/// <param name="predicate">Условие для фильтрации (опционально).</param>
		/// <returns>Количество сущностей.</returns>
		Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate = null);

		/// <summary>
		/// Получает первую сущность типа <typeparamref name="TEntity"/>, удовлетворяющую указанному условию.
		/// </summary>
		/// <param name="predicate">Условие для поиска.</param>
		/// <returns>Первая сущность, удовлетворяющая условию, или <c>null</c>, если такая сущность не найдена.</returns>
		Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate);
	}
}
