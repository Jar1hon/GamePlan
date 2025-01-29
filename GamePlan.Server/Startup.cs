using GamePlan.Domain.Settings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace GamePlan.Api
{
	public static class Startup
	{
		/// <summary>
		/// Подключение аутентификации и авторизации
		/// </summary>
		/// <param name="services"></param>
		public static void AddAuthenticationAndAuthorization(this IServiceCollection services, WebApplicationBuilder builder)
		{
			services.AddAuthorization();
			services.AddAuthentication(options =>
			{
				options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
				options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
			}).AddJwtBearer(o =>
			{
				var options = builder.Configuration.GetSection(JwtSettings.DefaultSections).Get<JwtSettings>();
				var jwtKey = options.JwtKey;
				var issuer = options.Issuer;
				var audience = options.Audience;
				o.Authority = options.Authority;
				o.RequireHttpsMetadata = false;
				o.TokenValidationParameters = new TokenValidationParameters()
				{
					ValidIssuer = issuer,
					ValidAudience = audience,
					IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey)),
					ValidateAudience = true,
					ValidateIssuer = true,
					ValidateLifetime = true,
					ValidateIssuerSigningKey = true
				};
			});
		}
	}
}
