using Microsoft.EntityFrameworkCore;

using RateGames.Models.Entities;

namespace RateGames.DatabaseContext;

public interface IApplicationContext
{
	DbSet<Rating> Ratings { get; set; }
	DbSet<Review> Reviews { get; set; }

	Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
