using GamePlan.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GamePlan.DAL.Configurations
{
	public class UserTokenConfigurations : IEntityTypeConfiguration<UserToken>
	{
		public void Configure(EntityTypeBuilder<UserToken> builder)
		{
			builder.Property(x => x.Id).ValueGeneratedOnAdd();
			builder.Property(x => x.RefreshToken).IsRequired();
			builder.Property(x => x.RefreshTokenExpiredTime).IsRequired();
		}
	}
}
