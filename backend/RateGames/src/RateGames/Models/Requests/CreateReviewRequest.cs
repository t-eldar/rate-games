using RateGames.Models.Entities;

namespace RateGames.Models.Requests;

public record CreateReviewRequest
{
	public required string Title { get; set; }
	public required string Description { get; set; }
	public required string UserId { get; set; }
	public required int RatingId { get; set; }

	public Review ToReview(DateTime dateCreated) =>
		new()
		{
			Description = Description,
			DateCreated = dateCreated,
			RatingId = RatingId,
			Title = Title,
			UserId = UserId
		};
}
