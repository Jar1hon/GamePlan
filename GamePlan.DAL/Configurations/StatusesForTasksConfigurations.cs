using GamePlan.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GamePlan.DAL.Configurations
{
	public class StatusesForTasksConfigurations : IEntityTypeConfiguration<StatusesForTasks>
	{
		public void Configure(EntityTypeBuilder<StatusesForTasks> builder)
		{
			builder.Property(x => x.Id).ValueGeneratedOnAdd();
			builder.Property(x => x.Name).IsRequired().HasMaxLength(100);
			builder.Property(x => x.Description).HasMaxLength(1000);
			builder.HasMany<Tasks>(x => x.Tasks)
				.WithOne(x => x.Status)
				.HasForeignKey(x => x.StatusId)
				.HasPrincipalKey(x => x.Id);
		}
	}
}
