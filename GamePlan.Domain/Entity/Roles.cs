using GamePlan.Domain.Interfaces;

namespace GamePlan.Domain.Entity
{
	public class Roles : IEntityId<Guid>
	{
		public Guid Id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public List<Users> User { get; set; }
		public List<Permissions> Permissions { get; set; }
		public List<Objects> Objects { get; set; }
	}
}
