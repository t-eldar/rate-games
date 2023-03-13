﻿using RateGames.Common.Contracts;
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
    public IEnumerable<GameMode>? GameModes { get; set; }
    public IEnumerable<Image>? Screenshots { get; set; }
    public IEnumerable<InvolvedCompany>? InvolvedCompanies { get; set; }
    public IEnumerable<Genre>? Genres { get; set; }
    public IEnumerable<GameEngine>? GameEngines {  get; set; }
    public IEnumerable<Game>? SimilarGames { get; set; }
    public IEnumerable<Platform>? Platforms { get; set; }
}
