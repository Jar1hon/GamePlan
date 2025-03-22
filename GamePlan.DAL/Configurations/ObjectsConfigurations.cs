using GamePlan.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GamePlan.DAL.Configurations
{
	public class ObjectsConfigurations : IEntityTypeConfiguration<Objects>
	{
		public void Configure(EntityTypeBuilder<Objects> builder)
		{
			builder.Property(x => x.Id).ValueGeneratedOnAdd();
			builder.Property(x => x.Name).IsRequired().HasMaxLength(200);
			builder.HasMany(x => x.Permissions)
				.WithMany(x => x.Objects)
				.UsingEntity<RolesPermissions>(
					l => l.HasOne<Permissions>().WithMany().HasForeignKey(x => x.PermissionId),
					l => l.HasOne<Objects>().WithMany().HasForeignKey(x => x.ObjectId)
				);
			builder.HasMany(x => x.Roles)
				.WithMany(x => x.Objects)
				.UsingEntity<RolesPermissions>(
					l => l.HasOne<Roles>().WithMany().HasForeignKey(x => x.RoleId),
					l => l.HasOne<Objects>().WithMany().HasForeignKey(x => x.ObjectId)
				);
		}
	}
}
