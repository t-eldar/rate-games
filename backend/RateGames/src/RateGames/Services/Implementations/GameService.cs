using Apicalypse.Core.Enums;
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
			.Include(g => new
			{
				PlatformNames = g.Platforms!.IncludeProperty(p => p.Value!.Name),
				PlatformLogos = g.Platforms!.IncludeProperty(
					p => p.Value!.PlatformLogo!.Value!.Url
				),
				Screenshots = g.Screenshots!.IncludeProperty(s => s.Value!.Url),
				GameModes = g.GameModes!.IncludeProperty(gm => gm.Value!.Name),
				InvolvedCompanies = g.InvolvedCompanies!.IncludeAllProperties(),
				CompanyNames = g.InvolvedCompanies!.IncludeProperty(
					 g => g.Value!.Company!.Name
				),
				CompanyLogos = g.InvolvedCompanies!.IncludeProperty(
					 g => g.Value!.Company!.Logo!.Value!.Url
				),
				Genres = g.Genres!.IncludeProperty(gn => gn.Value!.Name),
				GameEngineLogos = g.GameEngines!.IncludeProperty(ge => ge.Value!.Logo!.Value!.Url),
				GameEngineNames = g.GameEngines!.IncludeProperty(ge => ge.Value!.Name),
			})
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
			.Select(g => new { g.Screenshots, g.SimilarGames, }, SelectionMode.Exclude)
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
			.Select(g => new { g.Screenshots, g.SimilarGames, }, SelectionMode.Exclude)
			.Where(game => game.Platforms!.ContainsAll(platformIds))
			.Skip(offset)
			.Take(limit)
			.Build();

		var response = await _igdbService.GetAsync<IEnumerable<Game>>(query, Endpoint);

		return response;
	}
	public async Task<IEnumerable<Game>?> GetByAllGameModesAsync(
		IEnumerable<int> gamemodeIds,
		int limit = 10,
		int offset = 0
	)
	{
		var query = _queryBuilderCreator.CreateFor<Game>()
			.Select(g => new { g.Screenshots, g.SimilarGames, }, SelectionMode.Exclude)
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
			.Select(g => new { g.Screenshots, g.SimilarGames, }, SelectionMode.Exclude)
			.Where(g => g.Genres!.ContainsAny(genreIds))
			.Skip(offset)
			.Take(limit)
			.Build();

		var response = await _igdbService.GetAsync<IEnumerable<Game>>(query, Endpoint);

		return response;
	}

	public async Task<IEnumerable<Game>?> GetByIdsAsync(
		IEnumerable<int> ids,
		int limit = 10,
		int offset = 0
	)
	{
		var query = _queryBuilderCreator.CreateFor<Game>()
			.Select(g => new { g.Screenshots, g.SimilarGames, }, SelectionMode.Exclude)
			.Where(g => g.Id.ContainsAny<Game>(ids))
			.Skip(offset)
			.Take(limit)
			.Build();

		var response = await _igdbService.GetAsync<IEnumerable<Game>>(query, Endpoint);

		return response;
	}
}
