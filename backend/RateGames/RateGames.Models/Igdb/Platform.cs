using RateGames.Common.Contracts;

namespace RateGames.Models.Igdb;

public class Platform : IEntity
{
    public int Id { get; set; }
    public string? Abbreviation { get; set; }
    public string? Name { get; set; }
    public int? Generation { get; set; }
    public string? Summary { get; set; }
    public Image? PlatformLogo { get; set; }
}