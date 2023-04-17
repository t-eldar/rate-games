using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

using RateGames.Models.Entities;

namespace RateGames.DatabaseContext;

public class ApplicationContext : IdentityDbContext<User>, IApplicationContext
{
	public DbSet<Rating> Ratings { get; set; }
	public DbSet<Review> Reviews { get; set; }
	public ApplicationContext(DbContextOptions<ApplicationContext> options)
		: base(options) 
	{
		Database.EnsureCreated();
	}
}
