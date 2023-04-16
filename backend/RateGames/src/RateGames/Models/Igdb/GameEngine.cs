using RateGames.Common.Contracts;

namespace RateGames.Models.Igdb;

public class GameEngine : IEntity
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public Image? Logo { get; set; }
}