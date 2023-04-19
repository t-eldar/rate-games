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

	protected override void OnModelCreating(ModelBuilder builder)
	{
		base.OnModelCreating(builder);

		builder
			.Entity<Rating>()
			.HasData(
				new Rating[]
				{
					new Rating { Id = 1, Value = 1 },
					new Rating { Id = 2, Value = 2 },
					new Rating { Id = 3, Value = 3 },
					new Rating { Id = 4, Value = 4 },
					new Rating { Id = 5, Value = 5 },
				}
			);
	}
}
