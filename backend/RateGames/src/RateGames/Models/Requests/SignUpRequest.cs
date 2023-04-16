namespace RateGames.Models.Requests;

public record SignUpRequest
{
	public required string Email { get; init; }
	public required string Username { get; init; }
	public required string Password { get; init; }
}
