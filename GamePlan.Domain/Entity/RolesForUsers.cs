using GamePlan.Domain.Interfaces;

namespace GamePlan.Domain.Entity
{
	public class RolesForUsers : IEntityId<Guid>
	{
		public Guid Id { get; set; }
		public string Name { get; set; }
		public string? Description { get; set; }
		public List<Users> User { get; set; }
	}
}
