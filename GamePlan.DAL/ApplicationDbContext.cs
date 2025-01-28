using Microsoft.EntityFrameworkCore;

namespace GamePlan.DAL
{
	public class ApplicationDbContext : DbContext
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
		{
			Database.EnsureCreated();
		}
	}
}
