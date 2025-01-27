namespace GamePlan.Domain.Interfaces
{
	/// <summary>
	/// Интерфейс для имплементации id в сущностях
	/// </summary>
	public interface IEntityId<T> where T : struct
	{
		public T Id { get; set; }
	}
}
