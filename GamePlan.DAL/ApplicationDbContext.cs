using GamePlan.DAL.Interceptors;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace GamePlan.DAL
{
	public class ApplicationDbContext : DbContext
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
		{
			Database.EnsureCreated();
		}

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			// Подключение Interceptor
			optionsBuilder.AddInterceptors(new DateInterceptor());
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			// Автоматическое подключение конфигураций из папки Configurations
			modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
		}
	}
}
