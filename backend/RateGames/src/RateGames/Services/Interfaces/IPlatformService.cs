using RateGames.Models.Igdb;

namespace RateGames.Services.Interfaces;

/// <summary>
/// Service for receiving platform entity from <see href="igdb.com"/>
/// </summary>
public interface IPlatformService
{
	Task<Platform?> GetByIdAsync(int id);
}
