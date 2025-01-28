using GamePlan.Domain.Interfaces;

namespace GamePlan.Domain.Entity
{
	public class PrioritiesType : IEntityId<Guid>
	{
		public Guid Id { get; set; }
		public string Name { get; set; }
		public List<Tasks> Tasks { get; set; }
		public List<Projects> Projects { get; set; }
	}
}
