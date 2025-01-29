using GamePlan.Domain.Interfaces;

namespace GamePlan.Domain.Entity
{
	public class UserRewards : IEntityId<Guid>
	{
		public Guid Id { get; set; }
		public Guid UserId { get; set; }
		public Guid AchievmentId { get; set; }
		public DateTime AwardedAt { get; set; }
	}
}
