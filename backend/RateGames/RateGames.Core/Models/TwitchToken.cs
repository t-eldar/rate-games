namespace RateGames.Core.Models;
public sealed class TwitchToken
{
    public required string AccessToken { get; set; }

    /// <summary>
    /// Time in seconds when token expire.
    /// </summary>
    public required int ExpiresIn { get; set; }
    public required string TokenType { get; set; }
}
