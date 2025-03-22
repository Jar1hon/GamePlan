using GamePlan.Domain.Interfaces;

namespace GamePlan.Domain.Entity
{
	public class RolesPermissions : IEntityId<Guid>
	{
		public Guid Id { get; set; }
		public Guid RoleId { get; set; }
		public Guid PermissionId { get; set; }
		public Guid ObjectId { get; set; }
	}
}
