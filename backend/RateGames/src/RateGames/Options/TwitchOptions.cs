namespace RateGames.Options;

public class TwitchOptions
{
	public const string Twitch = nameof(Twitch);
	public required string ClientId { get; set; }
	public required string ClientSecret { get; set; }
	public required string TokenAddress { get; set; }
}
