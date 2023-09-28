using RateGames.Common.Contracts;
using RateGames.Common.Utils;
using RateGames.Models.Igdb.Enums;

namespace RateGames.Models.Igdb;

public class Platform : IEntity
{
	public int Id { get; set; }
	public string? Abbreviation { get; set; }
	public string? Name { get; set; }
	public int? Generation { get; set; }
	public PlatformCategory? Category { get; set; }
	public string? Summary { get; set; }
	public IdOr<Image>? PlatformLogo { get; set; }
}