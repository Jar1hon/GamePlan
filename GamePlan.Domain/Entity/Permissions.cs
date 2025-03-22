using GamePlan.Domain.Interfaces;

namespace GamePlan.Domain.Entity
{
	public class Permissions : IEntityId<Guid>
	{
		public Guid Id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public List<Objects> Objects { get; set; }
		public List<Roles> Roles { get; set; }
	}
}
