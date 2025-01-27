using GamePlan.Domain.Interfaces;

namespace GamePlan.Domain.Entity
{
	public class UserRewards : IEntityId<Guid>
	{
		public Guid Id { get; set; }
		public Users UserId { get; set; }
		public Achievments AchievmentId { get; set; }
		public DateTime AwardedAt { get; set; }
	}
}
