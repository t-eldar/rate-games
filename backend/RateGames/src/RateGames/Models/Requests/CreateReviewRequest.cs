using RateGames.Models.Entities;

namespace RateGames.Models.Requests;

public record CreateReviewRequest
{
	public required string Title { get; init; }
	public required string Description { get; init; }
	public required int RatingId { get; init; }
	public required int GameId { get; init; }
	public required string UserId { get; init; }

	public Review ToReview(DateTime dateCreated) =>
		new()
		{
			Title = Title,
			Description = Description,
			DateCreated = dateCreated,
			RatingId = RatingId,
			GameId = GameId,
			UserId = UserId
		};
}
