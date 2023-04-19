using RateGames.Models.Entities;

namespace RateGames.Models.Requests;

public record SignUpRequest
{
	public required string Email { get; init; }
	public required string Username { get; init; }
	public required string Password { get; init; }
	public required string AvatarUrl { get; init; }

	public User ToUser() =>
		new()
		{
			Email = Email,
			UserName = Username,
			AvatarUrl = AvatarUrl,
		};
}
