using GamePlan.Domain.Interfaces;

namespace GamePlan.Domain.Entity
{
	public class Tasks : IEntityId<Guid>, IAuditable
	{
		public Guid Id { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }
		public PrioritiesType Priority { get; set; }
		public Guid PriorityId { get; set; }
		public StatusesForTasks Status { get; set; }
		public Guid StatusId { get; set; }
		public DateTime DueDate { get; set; }
		public Users AssignedTo { get; set; }
		public Guid AssignedToId { get; set; }
		public Projects Project { get; set; }
		public Guid ProjectId { get; set; }
		public DateTime CreatedAt { get; set; }
		public DateTime? UpdatedAt { get; set; }
	}
}
