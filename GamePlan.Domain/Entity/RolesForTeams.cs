using GamePlan.Domain.Interfaces;

namespace GamePlan.Domain.Entity
{
	public class RolesForTeams : IEntityId<Guid>
	{
		public Guid Id { get; set; }
		public string Name { get; set; }
		public string? Description { get; set; }
		public List<Teams> Teams { get; set; }
	}
}
