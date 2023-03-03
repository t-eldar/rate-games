namespace RateGames.Models;

public class Company
{
	public int Id { get; set; }
	public string? Name { get; set; }

	/// <summary>
	/// ISO 3166-1 country code
	/// </summary>
	public int? Country { get; set; }
	public int? StartDate { get; set; }
	public IEnumerable<Game>? Published { get; set; }
	public IEnumerable<Game>? Developed { get; set; }
}