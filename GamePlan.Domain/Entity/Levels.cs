using GamePlan.Domain.Interfaces;

namespace GamePlan.Domain.Entity
{
	public class Levels : IEntityId<Guid>
	{
		public Guid Id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public int RequiredPoints { get; set; }
		public string BadgeUrl { get; set; }
	}
}
