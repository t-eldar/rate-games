namespace RateGames.Services.Interfaces;

/// <summary>
/// Service for receiving access token from twitch.
/// </summary>
public interface ITwitchTokenService
{
    /// <summary>
    /// Returns token from twitch.
    /// </summary>
    Task<string> GetTokenAsync();
}
