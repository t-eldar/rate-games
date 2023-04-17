using RateGames.Models.Entities;
using RateGames.Models.Requests;

namespace RateGames.Repositories.Interfaces;

/// <summary>
/// Repository for <see cref="Review"/> model.
/// </summary>
public interface IReviewRepository
{
	Task<IEnumerable<Review>?> GetAllAsync(int limit = 0, int offset = 0);
	Task<IEnumerable<Review>?> GetByUserAsync(string userId, int limit = 0, int offset = 0);
	Task<IEnumerable<Review>?> GetByGameAsync(int gameId, int limit = 0, int offset = 0);
	Task<Review?> GetByIdAsync(int id);

	Task<Review> CreateAsync(CreateReviewRequest request);
	Task UpdateAsync(UpdateReviewRequest request);
	Task DeleteAsync(int id);
}
