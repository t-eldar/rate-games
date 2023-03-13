using RateGames.Common.Contracts;

namespace RateGames.Models.Igdb;

public class Company : IEntity
{
    public int Id { get; set; }
    public string? Name { get; set; }

    /// <summary>
    /// ISO 3166-1 country code
    /// </summary>
    public int? Country { get; set; }
    public int? StartDate { get; set; }
    public Image? Logo { get; set; }
    public IEnumerable<Game>? Published { get; set; }
    public IEnumerable<Game>? Developed { get; set; }
}