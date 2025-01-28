using GamePlan.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GamePlan.DAL.Configurations
{
	public class StatusesForProjectsConfigurations : IEntityTypeConfiguration<StatusesForProjects>
	{
		public void Configure(EntityTypeBuilder<StatusesForProjects> builder)
		{
			builder.Property(x => x.Id).ValueGeneratedOnAdd();
			builder.Property(x => x.Name).IsRequired().HasMaxLength(100);
			builder.Property(x => x.Description).HasMaxLength(1000);
			builder.HasMany<Projects>(x => x.Projects)
				.WithOne(x => x.Status)
				.HasForeignKey(x => x.StatusId)
				.HasPrincipalKey(x => x.Id);
		}
	}
}
