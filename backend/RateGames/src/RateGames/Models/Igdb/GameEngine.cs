using RateGames.Common.Contracts;
using RateGames.Common.Utils;

namespace RateGames.Models.Igdb;

public class GameEngine : IEntity
{
	public int Id { get; set; }
	public string? Name { get; set; }
	public string? Description { get; set; }
	public IdOr<Image>? Logo { get; set; }
}