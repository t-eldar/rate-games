using Apicalypse.Core.Interfaces;

using RateGames.Common.Extensions;
using RateGames.Models.Igdb;
using RateGames.Services.Interfaces;

namespace RateGames.Services.Implementations;

/// <inheritdoc cref="IGameService"/>
public class GameService : IGameService
{
	private readonly IIgdbService _igdbService;
	private readonly IQueryBuilderCreator _queryBuilderCreator;
	public GameService(
		IIgdbService igdbService,
		IQueryBuilderCreator queryBuilderCreator
	)
	{
		_igdbService = igdbService;
		_queryBuilderCreator = queryBuilderCreator;
	}

	public async Task<Game?> GetById(int id)
	{
		var query = _queryBuilderCreator.CreateFor<Game>()
			.Select()
			.Where(g => g.Id == id)
			.Build();

		var response = await _igdbService.GetAsync<IEnumerable<Game>>(query, Endpoints.Games);
		var result = response?.FirstOrDefault();

		return result;
	}

	public async Task<IEnumerable<Game>?> GetByGenre(int genreId, int limit = 10, int offset = 0)
	{
		var query = _queryBuilderCreator.CreateFor<Game>()
			.Select(game => new
			{
				game.Id,
				game.Name,
				game.FirstReleaseDate,
				GameEngines = game.GameEngines!.IncludeProperty(ge => ge.Id),
			})
			.Where(g => g.Genres!.ContainsAny(new[] { genreId }))
			.Skip(offset)
			.Take(limit)
			.Build();

		var response = await _igdbService.GetAsync<IEnumerable<Game>>(query, Endpoints.Games);

		return response;
	}
	public async Task<IEnumerable<Game>?> GetByPlatforms(IEnumerable<int> platformIds, int limit = 10, int offset = 0)
	{
		var query = _queryBuilderCreator.CreateFor<Game>()
			.Select()
			.Where(game => game.Platforms!.ContainsAny(platformIds))
			.Skip(offset)
			.Take(limit)
			.Build();

		var response = await _igdbService.GetAsync<IEnumerable<Game>>(query, Endpoints.Games);

		return response;
	}
	public Task<IEnumerable<Game>?> GetBySearch(string searchQuery, int limit = 10, int offset = 0) => throw new NotImplementedException();
	public Task<IEnumerable<Game>?> GetByGameMode(int gamemodeId, int limit = 10, int offset = 0) => throw new NotImplementedException();
}
