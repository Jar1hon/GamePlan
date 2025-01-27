using GamePlan.Domain.Interfaces;

namespace GamePlan.Domain.Entity
{
	public class UsersInTeams : IEntityId<Guid>
	{
		public Guid Id { get; set; }
		public Guid UserId { get; set; }
		public Guid TeamId { get; set; }
		public RolesForTeams Role { get; set; }
	}
}
