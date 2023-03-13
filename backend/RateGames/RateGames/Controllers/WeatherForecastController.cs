using Apicalypse.Core.Enums;
using Apicalypse.Core.Extensions;
using Apicalypse.Core.Interfaces;

using Microsoft.AspNetCore.Mvc;

using RateGames.Common.Extensions;
using RateGames.Core.Services.Implementations;
using RateGames.Core.Services.Interfaces;
using RateGames.Models.Igdb;

namespace RateGames.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;
    private readonly IQueryBuilderCreator _builderCreator;
    private readonly ITwitchTokenService _twitchTokenClient;
    private readonly IIgdbService _igdbClient;
    private readonly IGameService _gameService;
    public WeatherForecastController(
        ILogger<WeatherForecastController> logger,
        IQueryBuilderCreator builderCreator,
        ITwitchTokenService twitchTokenClient,
        IIgdbService igdbClient,
        IGameService gameService)
    {
        _logger = logger;
        _builderCreator = builderCreator;
        _twitchTokenClient = twitchTokenClient;
        _igdbClient = igdbClient;
        _gameService = gameService;
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public string Get()
    {
        var (a, b, c) = (1, 2, 3);
        return _builderCreator.CreateFor<Person>()
            .Select(p => p.Name, SelectionMode.Include)
            .Where(p => p.Ints.ContainsAll(new[] { a, b, c })
                || p.Name.StartsWith("Hello") && p.Name.EndsWith('f')
                || p.Name.Contains('f'))
            .Find("Hello world")
            .Skip(2)
            .Take(3)
            .Build()!;
    }

    [HttpGet(Name = "Token")]
    public async Task<string> GetTokenAsync()
    {
        return await _twitchTokenClient.GetTokenAsync();
    }

    [HttpGet(Name = "GetGame")]
    public async Task<IActionResult> GetGameByGenreAsync(int genreId)
    {
        var result = await _gameService.GetByGenre(genreId);
        return Ok(result);
    }

    public record Person(string Name, int Age, int[] Ints);
}
