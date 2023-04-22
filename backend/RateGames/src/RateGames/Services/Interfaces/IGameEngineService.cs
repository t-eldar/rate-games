using RateGames.Models.Igdb;

namespace RateGames.Services.Interfaces;

/// <summary>
/// Service for receiving game engine entity from <see href="igdb.com"/>
/// </summary>
public interface IGameEngineService
{
	Task<GameEngine?> GetByIdAsync(int id);
}
