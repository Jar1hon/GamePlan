using GamePlan.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GamePlan.DAL.Configurations
{
	public class ProjectsConfigurations : IEntityTypeConfiguration<Projects>
	{
		public void Configure(EntityTypeBuilder<Projects> builder)
		{
			builder.Property(x => x.Id).ValueGeneratedOnAdd();
			builder.Property(x => x.Name).IsRequired().HasMaxLength(100);
			builder.Property(x => x.Description).HasMaxLength(1000);
			builder.HasMany<Tasks>(x => x.Tasks)
				.WithOne(x => x.Project)
				.HasForeignKey(x => x.ProjectId)
				.HasPrincipalKey(x => x.Id);
		}
	}
}
