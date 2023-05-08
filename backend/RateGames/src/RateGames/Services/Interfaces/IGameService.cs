using RateGames.Models.Igdb;

namespace RateGames.Services.Interfaces;

/// <summary>
/// Service for receiving game entities from <see href="igdb.com"/>.
/// </summary>
public interface IGameService
{
	Task<Game?> GetByIdAsync(int id);
	Task<IEnumerable<Game>?> GetByIdsAsync(IEnumerable<int> ids, int limit, int offset);
	Task<IEnumerable<Game>?> GetBySearchAsync(string searchQuery, int limit, int offset);
	Task<IEnumerable<Game>?> GetByAllPlatformsAsync(IEnumerable<int> platformIds, int limit, int offset);
	Task<IEnumerable<Game>?> GetByAllGenresAsync(IEnumerable<int> genreIds, int limit, int offset);
	Task<IEnumerable<Game>?> GetByAllGameModesAsync(IEnumerable<int> gamemodeIds, int limit, int offset);
	Task<IEnumerable<Game>?> GetLatestAsync(int limit, int offset);
}
