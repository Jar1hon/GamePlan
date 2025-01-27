using GamePlan.Domain.Interfaces;

namespace GamePlan.Domain.Entity
{
	public class NotificationsType : IEntityId<Guid>
	{
		public Guid Id { get; set; }
		public string Name { get; set; }
	}
}
