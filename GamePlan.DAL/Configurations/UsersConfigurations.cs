using GamePlan.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GamePlan.DAL.Configurations
{
	public class UsersConfigurations : IEntityTypeConfiguration<Users>
	{
		public void Configure(EntityTypeBuilder<Users> builder)
		{
			builder.Property(x => x.Id).ValueGeneratedOnAdd();
			builder.Property(x => x.UserName).IsRequired().HasMaxLength(100);
			builder.Property(x => x.PasswordHash).IsRequired();

			builder.HasMany<Notifications>(x => x.Notifications)
				.WithOne(x => x.User)
				.HasForeignKey(x => x.UserId)
				.HasPrincipalKey(x => x.Id);
			builder.HasMany<Tasks>(x => x.Tasks)
				.WithOne(x => x.AssignedTo)
				.HasForeignKey(x => x.AssignedToId)
				.HasPrincipalKey(x => x.Id);
			builder.HasMany<Projects>(x => x.Projects)
				.WithOne(x => x.CreatedBy)
				.HasForeignKey(x => x.CreatedById)
				.HasPrincipalKey(x => x.Id);
			builder.HasMany(x => x.Roles)
				.WithMany(x => x.User)
				.UsingEntity<UserInRoles>(
					l => l.HasOne<RolesForUsers>().WithMany().HasForeignKey(x => x.RoleId),
					l => l.HasOne<Users>().WithMany().HasForeignKey(x => x.UserId)
				);
			builder.HasMany(x => x.Teams)
				.WithMany(x => x.Users)
				.UsingEntity<UsersInTeams>(
					l => l.HasOne<Teams>().WithMany().HasForeignKey(x => x.TeamId),
					l => l.HasOne<Users>().WithMany().HasForeignKey(x => x.UserId)
				);
			builder.HasMany(x => x.Achievments)
				.WithMany(x => x.User)
				.UsingEntity<UserRewards>(
					l => l.HasOne<Achievments>().WithMany().HasForeignKey(x => x.AchievmentId),
					l => l.HasOne<Users>().WithMany().HasForeignKey(x => x.UserId)
				);
		}
	}
}
