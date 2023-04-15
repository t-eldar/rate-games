using RateGames.Core.Models;

namespace RateGames.Core.Dtos;
public sealed class TwitchTokenDto
{
    public required TwitchToken Value { get; set; }

    /// <summary>
    /// Created time represented by the number of seconds since 1970-01-01T00:00:00Z
    /// </summary>
    public required long CreatedAt { get; set; }
}
