using GamePlan.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GamePlan.DAL.Configurations
{
	public class TasksConfigurations : IEntityTypeConfiguration<Tasks>
	{
		public void Configure(EntityTypeBuilder<Tasks> builder)
		{
			builder.Property(x => x.Id).ValueGeneratedOnAdd();
			builder.Property(x => x.Title).IsRequired().HasMaxLength(100);
			builder.Property(x => x.Description).HasMaxLength(1000);
		}
	}
}
