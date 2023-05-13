using RateGames.Models.Entities;

namespace RateGames.Models.Responses;

public record ReviewResponse
{
	public int Id { get; init; }
	public required string Title { get; init; }
	public required string Description { get; init; }
	public required DateTime DateCreated { get; init; }

	public required int GameId { get; init; }

	public required string UserId { get; init; }
	public UserInfoResponse? UserInfo { get; init; }
	public int Rating { get; init; }
	public static ReviewResponse GetFromReview(Review review) =>
		new()
		{
			Id = review.Id,
			Title = review.Title,
			Description = review.Description,
			DateCreated = review.DateCreated,
			GameId = review.GameId,
			UserInfo = UserInfoResponse.GetFromUser(
				review.User
				?? throw new Exception("User object should have User prop initialized")
			),
			UserId = review.UserId,
			Rating = review.Rating?.Value
				?? throw new Exception("Review should have Rating prop initialized"),
		};
}
