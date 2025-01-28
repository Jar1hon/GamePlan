using GamePlan.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GamePlan.DAL.Configurations
{
	public class PrioritiesTypeConfigurations : IEntityTypeConfiguration<PrioritiesType>
	{
		public void Configure(EntityTypeBuilder<PrioritiesType> builder)
		{
			builder.Property(x => x.Id).ValueGeneratedOnAdd();
			builder.Property(x => x.Name).IsRequired().HasMaxLength(100);
			builder.HasMany<Tasks>(x => x.Tasks)
				.WithOne(x => x.Priority)
				.HasForeignKey(x => x.PriorityId)
				.HasPrincipalKey(x => x.Id);
		}
	}
}
