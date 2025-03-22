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
		public List<Roles> Roles { get; set; }
		public List<Teams> Teams { get; set; }
		public List<Achievments> Achievments { get; set; }
		public List<Notifications> Notifications { get; set; }
		public List<Tasks> Tasks { get; set; }
		public List<Projects> Projects { get; set; }
		public UserToken UserToken { get; set; }
		public DateTime CreatedAt { get; set; }
		public DateTime? UpdatedAt { get; set; }
	}
}
