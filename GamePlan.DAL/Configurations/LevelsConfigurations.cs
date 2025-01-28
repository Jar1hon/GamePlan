using GamePlan.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GamePlan.DAL.Configurations
{
	public class LevelsConfigurations : IEntityTypeConfiguration<Levels>
	{
		public void Configure(EntityTypeBuilder<Levels> builder)
		{
			builder.Property(x => x.Id).ValueGeneratedOnAdd();
			builder.Property(x => x.Name).IsRequired().HasMaxLength(100);
			builder.Property(x => x.Description).HasMaxLength(2000);
		}
	}
}
