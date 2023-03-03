using Apicalypse.Core.Enums;
using Apicalypse.Core.Extensions;
using Apicalypse.Core.Interfaces;

using Microsoft.AspNetCore.Mvc;

namespace RateGames.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;
    private readonly IQueryBuilderCreator _builderCreator;
    public WeatherForecastController(ILogger<WeatherForecastController> logger, IQueryBuilderCreator builderCreator)
    {
        _logger = logger;
        _builderCreator = builderCreator;
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public string Get()
    {
        return _builderCreator.CreateFor<Person>()
            .Select(p => p.Name, SelectionMode.Include)
            .Where(p => p.Ints.ContainsAll(new[] { 1, 3, 5, 6, 7 })
                || p.Name.StartsWith("Hello") && p.Name.EndsWith('f') 
                || p.Name.Contains('f'))
            .Find("Hello world")
            .Skip(2)
            .Take(3)
            .Build()!;
    }
    public record Person(string Name, int Age, int[] Ints);
}
