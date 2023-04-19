using RateGames.Models.Contracts;

namespace RateGames.Models.Entities;

public class Review : IResource
{
	public int Id { get; set; }
	public required string Title { get; set; }
	public required string Description { get; set; }
	public required DateTime DateCreated { get; set; }

	public required int GameId { get; set; }

	public required string UserId { get; set; }
	public User? User { get; set; }

	public required int RatingId { get; set; }
	public Rating? Rating { get; set; }
}
