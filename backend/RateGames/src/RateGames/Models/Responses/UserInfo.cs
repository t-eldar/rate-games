namespace RateGames.Models.Responses;

public record UserInfo
{
	public required string Id { get; init; }
	public required string? UserName { get; init; }
	public string? AvatarUrl { get; init; }
}
