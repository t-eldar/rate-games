namespace RateGames.Models.Requests;

public record GetGamesByForeignIdsRequest
{
	public required IEnumerable<int> Ids { get; init; }
}
