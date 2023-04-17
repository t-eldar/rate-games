using RateGames.Models.Entities;
using RateGames.Models.Requests;

namespace RateGames.Repositories.Interfaces;

/// <summary>
/// Repository for <see cref="Review"/> model. 100 is limit per request.
/// </summary>
public interface IReviewRepository
{
	const int Limit = 100;
	Task<IEnumerable<Review>?> GetAllAsync(int limit = Limit, int offset = 0);
	Task<IEnumerable<Review>?> GetByUserAsync(string userId, int limit = Limit, int offset = 0);
	Task<IEnumerable<Review>?> GetByGameAsync(int gameId, int limit = Limit, int offset = 0);
	Task<Review?> GetByIdAsync(int id);

	Task<Review> CreateAsync(CreateReviewRequest request);
	Task UpdateAsync(UpdateReviewRequest request);
	Task DeleteAsync(int id);
}
