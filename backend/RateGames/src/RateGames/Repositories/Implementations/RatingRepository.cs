using Microsoft.EntityFrameworkCore;

using RateGames.DatabaseContext;
using RateGames.Models.Entities;
using RateGames.Repositories.Interfaces;

namespace RateGames.Repositories.Implementations;

public class RatingRepository : IRatingRepository
{
	private readonly IApplicationContext _applicationContext;

	public RatingRepository(IApplicationContext applicationContext) =>
		_applicationContext = applicationContext;

	public async Task<IEnumerable<Rating>?> GetAllAsync() =>
		await _applicationContext.Ratings.ToListAsync();

	public async Task<Rating?> GetByIdAsync(int id) =>
		await _applicationContext.Ratings.FirstOrDefaultAsync(r => r.Id == id);

	public async Task<Rating?> GetByValueAsync(int value) =>
		await _applicationContext.Ratings.FirstOrDefaultAsync(r => r.Value == value);
}
