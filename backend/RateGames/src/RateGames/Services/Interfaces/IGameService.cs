using RateGames.Models.Igdb;

namespace RateGames.Services.Interfaces;

/// <summary>
/// Service for receiving game entities from <see href="igdb.com"/>.
/// </summary>
public interface IGameService
{
    Task<Game?> GetById(int id);

    Task<IEnumerable<Game>?> GetByPlatforms(
        IEnumerable<int> platformIds,
        int limit = 10,
        int offset = 0
    );

    Task<IEnumerable<Game>?> GetBySearch(string searchQuery, int limit = 10, int offset = 0);

    Task<IEnumerable<Game>?> GetByGenre(int genreId, int limit = 10, int offset = 0);

    Task<IEnumerable<Game>?> GetByGameMode(int gamemodeId, int limit = 10, int offset = 0);
}
