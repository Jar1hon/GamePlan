using GamePlan.Application.Mapping;
using GamePlan.Application.Services;
using GamePlan.Domain.Interfaces.Services;
using Microsoft.Extensions.DependencyInjection;

namespace GamePlan.Application.DependencyInjection
{
	public static class DependencyInjection
	{
		public static void AddApplication(this IServiceCollection services)
		{
			services.AddAutoMapper(typeof(UserMapping));
			InitServices(services);
		}

		private static void InitServices(this IServiceCollection services)
		{
			services.AddScoped<IAuthService, AuthServices>();
			services.AddScoped<ITokenService, TokenServices>();
		}
	}
}
