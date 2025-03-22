using GamePlan.Domain.Interfaces;

namespace GamePlan.Domain.Entity
{
	public class Objects : IEntityId<Guid>, IAuditable
	{
		public Guid Id { get; set; }
		public string Name { get; set; }
		public List<Permissions> Permissions { get; set; }
		public List<Roles> Roles { get; set; }
		public DateTime CreatedAt { get; set; }
		public DateTime? UpdatedAt { get; set; }
	}
}
