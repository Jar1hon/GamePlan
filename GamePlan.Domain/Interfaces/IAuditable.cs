namespace GamePlan.Domain.Interfaces
{
	/// <summary>
	/// Интерфейс для имплементации полей создания/обновления в сущностях
	/// </summary>
	public interface IAuditable
	{
		public DateTime CreatedAt { get; set; }

		public DateTime? UpdatedAt { get; set; }
	}
}
