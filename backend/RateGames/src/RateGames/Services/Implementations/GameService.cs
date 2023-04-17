using Apicalypse.Core.Interfaces;

using RateGames.Common.Extensions;
using RateGames.Models.Igdb;
using RateGames.Services.Interfaces;

namespace RateGames.Services.Implementations;

/// <inheritdoc cref="IGameService"/>
public class GameService : IGameService
{
	private const string Endpoint = Endpoints.Games;
	
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
	public async Task<Game?> GetByIdAsync(int id)
	{
		var query = _queryBuilderCreator.CreateFor<Game>()
			.Select()
			.Where(g => g.Id == id)
			.Build();

		var response = await _igdbService.GetAsync<IEnumerable<Game>>(query, Endpoint);
		var result = response?.FirstOrDefault();

		return result;
	}
	public async Task<IEnumerable<Game>?> GetBySearchAsync(
		string searchQuery,
		int limit = 10,
		int offset = 0
	)
	{
		var query = _queryBuilderCreator.CreateFor<Game>()
			.Select()
			.Find(searchQuery)
			.Skip(offset)
			.Take(limit)
			.Build();

		var response = await _igdbService.GetAsync<IEnumerable<Game>>(query, Endpoint);

		return response;
	}

	public async Task<IEnumerable<Game>?> GetByAllPlatformsAsync(
		IEnumerable<int> platformIds,
		int limit = 10,
		int offset = 0
	)
	{
		var query = _queryBuilderCreator.CreateFor<Game>()
			.Select()
			.Where(game => game.Platforms!.ContainsAll(platformIds))
			.Skip(offset)
			.Take(limit)
			.Build();

		var response = await _igdbService.GetAsync<IEnumerable<Game>>(query, Endpoint);

		return response;
	}
	public async Task<IEnumerable<Game>?> GetByAllGameModesAsync(
		IEnumerable<int>  gamemodeIds, 
		int limit = 10, 
		int offset = 0
	)
	{
		var query = _queryBuilderCreator.CreateFor<Game>()
			.Select()
			.Where(g => g.GameModes!.ContainsAny(gamemodeIds))
			.Skip(offset)
			.Take(limit)
			.Build();

		var response = await _igdbService.GetAsync<IEnumerable<Game>>(query, Endpoint);

		return response;
	}

	public async Task<IEnumerable<Game>?> GetByAllGenresAsync(
		IEnumerable<int> genreIds, 
		int limit = 10, 
		int offset = 0
	)
	{
		var query = _queryBuilderCreator.CreateFor<Game>()
			.Select()
			.Where(g => g.Genres!.ContainsAny(genreIds))
			.Skip(offset)
			.Take(limit)
			.Build();

		var response = await _igdbService.GetAsync<IEnumerable<Game>>(query, Endpoint);

		return response;
	}
}
