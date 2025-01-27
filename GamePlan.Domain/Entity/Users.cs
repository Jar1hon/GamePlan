using GamePlan.Domain.Interfaces;

namespace GamePlan.Domain.Entity
{
	public class Users : IEntityId<Guid>, IAuditable
	{
		public Guid Id { get; set; }
		public string UserName { get; set; }
		public string Email { get; set; }
		public string PasswordHash { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public List<RolesForUsers> Role { get; set; }
		public List<Teams> Team { get; set; }
		public List<Achievments> Achievment { get; set; }
		public UserToken UserToken { get; set; }
		public DateTime CreatedAt { get; set; }
		public DateTime? UpdatedAt { get; set; }
	}
}
