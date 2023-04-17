using RateGames.DatabaseContext;
using RateGames.Models.Entities;
using RateGames.Models.Requests;
using RateGames.Repositories.Interfaces;

namespace RateGames.Repositories.Implementations;

/// <inheritdoc cref="IReviewRepository"/>
public class ReviewRepository : IReviewRepository
{
	private readonly IApplicationContext _applicationContext;

	public ReviewRepository(IApplicationContext applicationContext) => _applicationContext = applicationContext;

	public Task<Review> CreateAsync(CreateReviewRequest request)
	{

	}
	public Task UpdateAsync(UpdateReviewRequest request) => throw new NotImplementedException();
	public Task DeleteAsync(int id) => throw new NotImplementedException();
	
	public Task<IEnumerable<Review>?> GetAllAsync(int limit = 0, int offset = 0) => throw new NotImplementedException();
	public Task<IEnumerable<Review>?> GetByGameAsync(int gameId, int limit = 0, int offset = 0) => throw new NotImplementedException();
	public Task<Review?> GetByIdAsync(int id) => throw new NotImplementedException();
	public Task<IEnumerable<Review>?> GetByUserAsync(string userId, int limit = 0, int offset = 0) => throw new NotImplementedException();
}
