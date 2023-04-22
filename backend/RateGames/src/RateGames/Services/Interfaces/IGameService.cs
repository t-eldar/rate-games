using RateGames.Models.Igdb;

namespace RateGames.Services.Interfaces;

/// <summary>
/// Service for receiving game entities from <see href="igdb.com"/>.
/// </summary>
public interface IGameService
{
	Task<Game?> GetByIdAsync(int id);
	Task<IEnumerable<Game>?> GetByIdsAsync(IEnumerable<int> ids, int limit = 10, int offset = 0);
	Task<IEnumerable<Game>?> GetBySearchAsync(string searchQuery, int limit = 10, int offset = 0);

	Task<IEnumerable<Game>?> GetByAllPlatformsAsync(IEnumerable<int> platformIds, int limit = 10, int offset = 0);
	Task<IEnumerable<Game>?> GetByAllGenresAsync(IEnumerable<int> genreIds, int limit = 10, int offset = 0);
	Task<IEnumerable<Game>?> GetByAllGameModesAsync(IEnumerable<int> gamemodeIds, int limit = 10, int offset = 0);
}
