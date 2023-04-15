using RateGames.Common.Contracts;

namespace RateGames.Models.Igdb;

public class Image : IEntity
{
    public int Id { get; set; }
    public int? Width { get; set; }
    public int? Height { get; set; }
    public string? Url { get; set; }
    public string? ImageId { get; set; }
}