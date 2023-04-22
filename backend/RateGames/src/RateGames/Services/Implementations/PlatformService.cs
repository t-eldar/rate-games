using Apicalypse.Core.Interfaces;

using RateGames.Models.Igdb;
using RateGames.Services.Interfaces;

namespace RateGames.Services.Implementations;

/// <inheritdoc cref="IPlatformService"/>
public class PlatformService : IPlatformService
{
	private const string Endpoint = Endpoints.Platforms;

	private readonly IIgdbService _igdbService;
	private readonly IQueryBuilderCreator _queryBuilderCreator;
	public PlatformService(
		IIgdbService igdbService, 
		IQueryBuilderCreator queryBuilderCreator
	)
	{
		_igdbService = igdbService;
		_queryBuilderCreator = queryBuilderCreator;
	}

	public async Task<Platform?> GetByIdAsync(int id)
	{
		var query = _queryBuilderCreator.CreateFor<Platform>()
			.Select()
			.Where(p => p.Id == id)
			.Build();

		var response = await _igdbService.GetAsync<IEnumerable<Platform>>(query, Endpoint);
		var result = response?.FirstOrDefault();

		return result;
	}
}
