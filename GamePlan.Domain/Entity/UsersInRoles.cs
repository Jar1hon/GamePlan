using GamePlan.Domain.Interfaces;

namespace GamePlan.Domain.Entity
{
	public class UserInRoles : IEntityId<Guid>
	{
		public Guid Id { get; set; }
		public Guid UserId { get; set; }
		public Guid RoleId { get; set; }
	}
}
