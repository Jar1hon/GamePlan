using GamePlan.Domain.Interfaces;

namespace GamePlan.Domain.Entity
{
	public class Teams : IEntityId<Guid>, IAuditable
	{
		public Guid Id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public List<Users> Users { get; set; }
		public DateTime CreatedAt { get; set; }
		public DateTime? UpdatedAt { get; set; }
	}
}
