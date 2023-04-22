using RateGames.Models.Igdb;

namespace RateGames.Services.Interfaces;

/// <summary>
/// Service for receiving genre entities from <see href="igdb.com"/>.
/// </summary>
public interface IGenreService
{
	Task<Genre?> GetByIdAsync(int id);
}
