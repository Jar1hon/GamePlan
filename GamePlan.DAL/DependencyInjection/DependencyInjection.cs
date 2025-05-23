﻿using GamePlan.DAL.Interceptors;
using GamePlan.DAL.Repositories;
using GamePlan.Domain.Entity;
using GamePlan.Domain.Interfaces.DataBaseses;
using GamePlan.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Data;

namespace GamePlan.DAL.DependencyInjection
{
	public static class DependencyInjection
	{
		public static void AddDataAccessLayer(this IServiceCollection services, IConfiguration configuration)
		{
			var connectionString = configuration.GetConnectionString("PostgresSQL");

			services.AddSingleton<DateInterceptor>();

			services.AddDbContext<ApplicationDbContext>(optionsAction =>
			{
				optionsAction.UseNpgsql(connectionString);
			});

			services.InitRepositories();
		}

		/// <summary>
		/// Регистрация репозиториев
		/// </summary>
		/// <param name="services"></param>
		private static void InitRepositories(this IServiceCollection services)
		{
			services.AddScoped<IUnitOfWork, UnitOfWork>();
			services.AddScoped<IBaseRepository<Achievments>, BaseRepository<Achievments>>();
			services.AddScoped<IBaseRepository<Levels>, BaseRepository<Levels>>();
			services.AddScoped<IBaseRepository<Notifications>, BaseRepository<Notifications>>();
			services.AddScoped<IBaseRepository<NotificationsType>, BaseRepository<NotificationsType>>();
			services.AddScoped<IBaseRepository<Objects>, BaseRepository<Objects>>();
			services.AddScoped<IBaseRepository<Permissions>, BaseRepository<Permissions>>();
			services.AddScoped<IBaseRepository<PrioritiesType>, BaseRepository<PrioritiesType>>();
			services.AddScoped<IBaseRepository<Projects>, BaseRepository<Projects>>();
			services.AddScoped<IBaseRepository<Roles>, BaseRepository<Roles>>();
			services.AddScoped<IBaseRepository<RolesPermissions>, BaseRepository<RolesPermissions>>();
			services.AddScoped<IBaseRepository<StatusesForProjects>, BaseRepository<StatusesForProjects>>();
			services.AddScoped<IBaseRepository<StatusesForTasks>, BaseRepository<StatusesForTasks>>();
			services.AddScoped<IBaseRepository<Tasks>, BaseRepository<Tasks>>();
			services.AddScoped<IBaseRepository<Teams>, BaseRepository<Teams>>();
			services.AddScoped<IBaseRepository<UserRewards>, BaseRepository<UserRewards>>();
			services.AddScoped<IBaseRepository<Users>, BaseRepository<Users>>();
			services.AddScoped<IBaseRepository<UserInRoles>, BaseRepository<UserInRoles>>();
			services.AddScoped<IBaseRepository<UserToken>, BaseRepository<UserToken>>();
		}
	}
}
