using GamePlan.Domain.Interfaces;

namespace GamePlan.Domain.Entity
{
	public class Notifications : IEntityId<Guid>
	{
		public Guid Id { get; set; }
		public Users User { get; set; }
		public Guid UserId { get; set; }
		public string Message { get; set; }
		public NotificationsType Type { get; set; }
		public Guid NotificationsTypeId { get; set; }
		public bool IsRead { get; set; }
		public DateTime CreatedAt { get; set; }
	}
}
