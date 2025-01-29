using GamePlan.Domain.Interfaces;

namespace GamePlan.Domain.Entity
{
	public class StatusesForTasks : IEntityId<Guid>
	{
		public Guid Id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public List<Tasks> Tasks { get; set; }
	}
}
