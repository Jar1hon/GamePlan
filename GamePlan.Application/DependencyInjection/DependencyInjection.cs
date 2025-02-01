using FluentValidation;
using FluentValidation.AspNetCore;
using GamePlan.Application.Mapping;
using GamePlan.Application.Services;
using GamePlan.Application.Validations.FluentValidations.Users;
using GamePlan.Domain.Dto.User;
using GamePlan.Domain.Interfaces.Services;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace GamePlan.Application.DependencyInjection
{
	public static class DependencyInjection
	{
		public static void AddApplication(this IServiceCollection services)
		{
			services.AddAutoMapper(typeof(UserMapping));
			services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
			services.AddFluentValidationAutoValidation();
			services.AddFluentValidationClientsideAdapters();
			InitServices(services);
		}

		private static void InitServices(this IServiceCollection services)
		{
			services.AddScoped<IValidator<RegisterUserDto>, RegisterUserValidator>();
			services.AddScoped<IAuthService, AuthServices>();
			services.AddScoped<ITokenService, TokenServices>();
		}
	}
}
