using GamePlan.Domain.Interfaces;

namespace GamePlan.Domain.Entity
{
	public class Projects : IEntityId<Guid>, IAuditable
	{
		public Guid Id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public DateTime StartDate { get; set; }
		public DateTime EndDate { get; set; }
		public StatusesForProjects Status { get; set; }
		public Guid StatusId { get; set; }
		public List<Tasks> Tasks { get; set; }
		public Users CreatedBy { get; set; }
		public Guid CreatedById { get; set; }
		public DateTime CreatedAt { get; set; }
		public DateTime? UpdatedAt { get; set; }
	}
}
