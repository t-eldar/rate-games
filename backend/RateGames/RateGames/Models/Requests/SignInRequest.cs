namespace RateGames.Models.Requests;

public record SignInRequest
{
	public required string UsernameOrEmail { get; init; }
	public required string Password { get; init; }
	public required bool RememberMe { get; init; }
}
