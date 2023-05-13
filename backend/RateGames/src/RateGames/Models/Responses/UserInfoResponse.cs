using RateGames.Models.Entities;

namespace RateGames.Models.Responses;

public record UserInfoResponse
{
	public required string Id { get; init; }
	public required string? UserName { get; init; }
	public string? AvatarUrl { get; init; }

	public static UserInfoResponse GetFromUser(User user) =>
		new()
		{
			Id = user.Id,
			UserName = user.UserName,
			AvatarUrl = user.AvatarUrl,
		};
}
