using RateGames.Models.Entities;

namespace RateGames.Repositories.Interfaces;

public interface IRatingRepository
{
	Task<IEnumerable<Rating>?> GetAllAsync();
	Task<Rating?> GetByValueAsync(int value);
	Task<Rating?> GetByIdAsync(int id);
}
