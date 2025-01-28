using GamePlan.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GamePlan.DAL.Configurations
{
	public class NotificationsTypeConfigurations : IEntityTypeConfiguration<NotificationsType>
	{
		public void Configure(EntityTypeBuilder<NotificationsType> builder)
		{
			builder.Property(x => x.Id).ValueGeneratedOnAdd();
			builder.Property(x => x.Name).IsRequired().HasMaxLength(100);
			builder.HasMany<Notifications>(x => x.Notifications)
				.WithOne(x => x.Type)
				.HasForeignKey(x => x.NotificationsTypeId)
				.HasPrincipalKey(x => x.Id);
		}
	}
}
