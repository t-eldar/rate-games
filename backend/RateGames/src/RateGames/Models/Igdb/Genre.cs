using RateGames.Common.Contracts;

namespace RateGames.Models.Igdb;
public class Genre : IEntity
{
	public int Id { get; set; }
	public string? Name { get; set; }
}
