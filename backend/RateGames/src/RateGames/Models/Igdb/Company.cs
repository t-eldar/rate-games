using RateGames.Common.Contracts;
using RateGames.Common.Utils;

namespace RateGames.Models.Igdb;

public class Company : IEntity
{
	public int Id { get; set; }
	public string? Name { get; set; }

	/// <summary>
	/// ISO 3166-1 country code
	/// </summary>
	public int? Country { get; set; }
	public long? StartDate { get; set; }
	public IdOr<Image>? Logo { get; set; }
	public IEnumerable<IdOr<Game>>? Published { get; set; }
	public IEnumerable<IdOr<Game>>? Developed { get; set; }
}