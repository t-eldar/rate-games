using RateGames.Models.Entities;

namespace RateGames.Models.Requests;

public record CreateReviewApiRequest
{
	public required string Title { get; init; }
	public required string Description { get; init; }
	public required int Rating { get; init; }
	public required int GameId { get; init; }

	public CreateReviewRequest ToCreateReviewRequest(string userId, int ratingId) =>
		new()
		{
			Title = Title,
			Description = Description,
			RatingId = ratingId,
			GameId = GameId,
			UserId = userId,
		};
}
