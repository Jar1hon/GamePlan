using GamePlan.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GamePlan.DAL.Configurations
{
	public class RolesConfigurations : IEntityTypeConfiguration<Roles>
	{
		public void Configure(EntityTypeBuilder<Roles> builder)
		{
			builder.Property(x => x.Id).ValueGeneratedOnAdd();
			builder.Property(x => x.Name).IsRequired().HasMaxLength(100);
			builder.Property(x => x.Description).HasMaxLength(1000);
			builder.HasMany(x => x.Permissions)
				.WithMany(x => x.Roles)
				.UsingEntity<RolesPermissions>(
					l => l.HasOne<Permissions>().WithMany().HasForeignKey(x => x.PermissionId),
					l => l.HasOne<Roles>().WithMany().HasForeignKey(x => x.RoleId)
				);
			builder.HasMany(x => x.Objects)
				.WithMany(x => x.Roles)
				.UsingEntity<RolesPermissions>(
					l => l.HasOne<Objects>().WithMany().HasForeignKey(x => x.ObjectId),
					l => l.HasOne<Roles>().WithMany().HasForeignKey(x => x.RoleId)
				);
		}
	}
}
