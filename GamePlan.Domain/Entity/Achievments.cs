using GamePlan.Domain.Interfaces;

namespace GamePlan.Domain.Entity
{
	public class Achievments : IEntityId<Guid>
	{
		public Guid Id { get; set; }
		public string Name { get; set; }
		public string? Description { get; set; }
		public string? BadgeUrl { get; set; }
		public int Points { get; set; }
		public List<Users> User { get; set; }
	}
}
