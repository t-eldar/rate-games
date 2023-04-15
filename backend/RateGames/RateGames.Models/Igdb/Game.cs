using RateGames.Common.Contracts;
using RateGames.Common.Utils;
using RateGames.Models.Enums;

namespace RateGames.Models.Igdb;
public class Game : IEntity
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Summary { get; set; }
    public int? FirstReleaseDate { get; set; }
    public double? AggregatedRating { get; set; }
    public double? Rating { get; set; }
    public GameCategory? Category { get; set; }
    public IEnumerable<IdOr<GameMode>>? GameModes { get; set; }
    public IEnumerable<IdOr<Image>>? Screenshots { get; set; }
    public IEnumerable<IdOr<InvolvedCompany>>? InvolvedCompanies { get; set; }
    public IEnumerable<IdOr<Genre>>? Genres { get; set; }
    public IEnumerable<IdOr<GameEngine>>? GameEngines {  get; set; }
    public IEnumerable<IdOr<Game>>? SimilarGames { get; set; }
    public IEnumerable<IdOr<Platform>>? Platforms { get; set; }
}
