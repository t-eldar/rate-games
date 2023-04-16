using RateGames.Models.Tokens;

namespace RateGames.Models.Dtos;

public record TwitchTokenDto
{
    public required TwitchToken Value { get; set; }

    /// <summary>
    /// Created time represented by the number of seconds since 1970-01-01T00:00:00Z
    /// </summary>
    public long CreatedAt { get; set; }
}