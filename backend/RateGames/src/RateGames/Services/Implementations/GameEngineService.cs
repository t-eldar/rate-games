using Apicalypse.Core.Interfaces;

using RateGames.Models.Igdb;
using RateGames.Services.Interfaces;

namespace RateGames.Services.Implementations;

/// <inheritdoc <see cref="IGameEngineService"/>/>
public class GameEngineService : IGameEngineService
{
	private const string Endpoint = Endpoints.GameEngines;
	private readonly IIgdbService _igdbService;
	private readonly IQueryBuilderCreator _queryBuilderCreator;

	public GameEngineService(
		IIgdbService igdbService,
		IQueryBuilderCreator queryBuilderCreator
	)
	{
		_igdbService = igdbService;
		_queryBuilderCreator = queryBuilderCreator;
	}

	public async Task<GameEngine?> GetByIdAsync(int id)
	{
		var query = _queryBuilderCreator.CreateFor<GameEngine>()
			.Select()
			.Where(ge => ge.Id == id)
			.Build();

		var response = await _igdbService.GetAsync<
			IEnumerable<GameEngine>
		>(query, Endpoint);

		var result = response?.FirstOrDefault();

		return result;
	}
}
