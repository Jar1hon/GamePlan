using GamePlan.Domain.Interfaces;

namespace GamePlan.Domain.Entity
{
	public class UserToken : IEntityId<Guid>
	{
		public Guid Id { get; set; }
		public string RefreshToken { get; set; }
		public DateTime RefreshTokenExpiredTime { get; set; }
		public Users User { get; set; }
		public Guid UserId { get; set; }
	}
}
