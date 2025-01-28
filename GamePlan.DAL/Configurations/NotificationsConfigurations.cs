using GamePlan.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GamePlan.DAL.Configurations
{
	public class NotificationsConfigurations : IEntityTypeConfiguration<Notifications>
	{
		public void Configure(EntityTypeBuilder<Notifications> builder)
		{
			builder.Property(x => x.Id).ValueGeneratedOnAdd();
		}
	}
}
