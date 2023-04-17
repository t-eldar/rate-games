using RateGames.Models.Entities;

namespace RateGames.Models.Requests;

public record UpdateReviewRequest
{
	public required int Id { get; set; }
	public string? Title { get; set; }
	public string? Description { get; set; }
	public int? RatingId { get; set; }
}
