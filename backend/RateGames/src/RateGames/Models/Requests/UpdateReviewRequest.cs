using Azure.Core;

using RateGames.Models.Entities;

namespace RateGames.Models.Requests;

public record UpdateReviewRequest
{
	public required int Id { get; set; }
	public string? Title { get; set; }
	public string? Description { get; set; }
	public int? RatingValue { get; set; }

	public void UpdateReview(Review review, int? ratingId)
	{
		review.Title = Title ?? review.Title;
		review.Description = Description ?? review.Description;
		review.RatingId = ratingId ?? review.RatingId;
	}
}
