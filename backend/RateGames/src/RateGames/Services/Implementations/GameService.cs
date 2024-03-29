﻿using Apicalypse.Core.Interfaces;

using RateGames.Common.Extensions;
using RateGames.Models.Igdb;
using RateGames.Models.Igdb.Enums;
using RateGames.Services.Interfaces;

namespace RateGames.Services.Implementations;

/// <inheritdoc cref="IGameService"/>
public class GameService : IGameService
{
	private const string Endpoint = Endpoints.Games;

	private readonly IIgdbService _igdbService;
	private readonly IQueryBuilderCreator _queryBuilderCreator;
	private readonly IDateTimeProvider _dateTimeProvider;

	public GameService(
		IIgdbService igdbService,
		IQueryBuilderCreator queryBuilderCreator,
		IDateTimeProvider dateTimeProvider
	)
	{
		_igdbService = igdbService;
		_queryBuilderCreator = queryBuilderCreator;
		_dateTimeProvider = dateTimeProvider;
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
				Cover = g.Cover!.IncludeProperty(c => c.Value!.Url),
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
		int limit,
		int offset
	)
	{
		var query = _queryBuilderCreator.CreateFor<Game>()
			.Select()
			.Include(g => new
			{
				PlatformNames = g.Platforms!.IncludeProperty(p => p.Value!.Name),
				Cover = g.Cover!.IncludeProperty(c => c.Value!.Url),
				GameModes = g.GameModes!.IncludeProperty(gm => gm.Value!.Name),
				Genres = g.Genres!.IncludeProperty(gn => gn.Value!.Name),
				GameEngineNames = g.GameEngines!.IncludeProperty(ge => ge.Value!.Name),
			})
			.Find(searchQuery)
			.Skip(offset)
			.Take(limit)
			.Build();

		var response = await _igdbService.GetAsync<IEnumerable<Game>>(query, Endpoint);

		return response;
	}

	public async Task<IEnumerable<Game>?> GetByAllPlatformsAsync(
		IEnumerable<int> platformIds,
		int limit,
		int offset
	)
	{
		var query = _queryBuilderCreator.CreateFor<Game>()
			.Select()
			.Include(g => new
			{
				PlatformNames = g.Platforms!.IncludeProperty(p => p.Value!.Name),
				Cover = g.Cover!.IncludeProperty(c => c.Value!.Url),
				GameModes = g.GameModes!.IncludeProperty(gm => gm.Value!.Name),
				Genres = g.Genres!.IncludeProperty(gn => gn.Value!.Name),
				GameEngineNames = g.GameEngines!.IncludeProperty(ge => ge.Value!.Name),
			})
			.Where(game => game.Platforms!.ContainsAll(platformIds))
			.Skip(offset)
			.Take(limit)
			.Build();

		var response = await _igdbService.GetAsync<IEnumerable<Game>>(query, Endpoint);

		return response;
	}
	public async Task<IEnumerable<Game>?> GetByAllGameModesAsync(
		IEnumerable<int> gamemodeIds,
		int limit,
		int offset
	)
	{
		var query = _queryBuilderCreator.CreateFor<Game>()
			.Select()
			.Include(g => new
			{
				PlatformNames = g.Platforms!.IncludeProperty(p => p.Value!.Name),
				Cover = g.Cover!.IncludeProperty(c => c.Value!.Url),
				GameModes = g.GameModes!.IncludeProperty(gm => gm.Value!.Name),
				Genres = g.Genres!.IncludeProperty(gn => gn.Value!.Name),
				GameEngineNames = g.GameEngines!.IncludeProperty(ge => ge.Value!.Name),
			})
			.Where(g => g.GameModes!.ContainsAny(gamemodeIds))
			.Skip(offset)
			.Take(limit)
			.Build();

		var response = await _igdbService.GetAsync<IEnumerable<Game>>(query, Endpoint);

		return response;
	}

	public async Task<IEnumerable<Game>?> GetByAllGenresAsync(
		IEnumerable<int> genreIds,
		int limit,
		int offset
	)
	{
		var query = _queryBuilderCreator.CreateFor<Game>()
			.Select()
			.Include(g => new
			{
				PlatformNames = g.Platforms!.IncludeProperty(p => p.Value!.Name),
				Cover = g.Cover!.IncludeProperty(c => c.Value!.Url),
				GameModes = g.GameModes!.IncludeProperty(gm => gm.Value!.Name),
				Genres = g.Genres!.IncludeProperty(gn => gn.Value!.Name),
				GameEngineNames = g.GameEngines!.IncludeProperty(ge => ge.Value!.Name),
			})
			.Where(g => g.Genres!.ContainsAny(genreIds))
			.Skip(offset)
			.Take(limit)
			.Build();

		var response = await _igdbService.GetAsync<IEnumerable<Game>>(query, Endpoint);

		return response;
	}

	public async Task<IEnumerable<Game>?> GetByIdsAsync(
		IEnumerable<int> ids,
		int limit,
		int offset
	)
	{
		var query = _queryBuilderCreator.CreateFor<Game>()
			.Select()
			.Include(g => new
			{
				PlatformNames = g.Platforms!.IncludeProperty(p => p.Value!.Name),
				Cover = g.Cover!.IncludeProperty(c => c.Value!.Url),
				GameModes = g.GameModes!.IncludeProperty(gm => gm.Value!.Name),
				Genres = g.Genres!.IncludeProperty(gn => gn.Value!.Name),
				GameEngineNames = g.GameEngines!.IncludeProperty(ge => ge.Value!.Name),
			})
			.Where(g => g.Id.ContainsAny<Game>(ids))
			.Skip(offset)
			.Take(limit)
			.Build();

		var response = await _igdbService.GetAsync<IEnumerable<Game>>(query, Endpoint);

		return response;
	}

	public async Task<IEnumerable<Game>?> GetLatestAsync(int limit, int offset)
	{
		var currentDate = _dateTimeProvider.CurrentUnixTimestamp;
		var query = _queryBuilderCreator.CreateFor<Game>()
			.Select()
			.Include(g => new
			{
				PlatformNames = g.Platforms!.IncludeProperty(p => p.Value!.Name),
				Cover = g.Cover!.IncludeProperty(c => c.Value!.Url),
				GameModes = g.GameModes!.IncludeProperty(gm => gm.Value!.Name),
				Genres = g.Genres!.IncludeProperty(gn => gn.Value!.Name),
				GameEngineNames = g.GameEngines!.IncludeProperty(ge => ge.Value!.Name),
			})
			.Where(
				g => g.Category == (int)GameCategory.MainGame && 
				g.FirstReleaseDate > 0 && 
				g.FirstReleaseDate < currentDate
			)
			.OrderByDescending(g => g.FirstReleaseDate)
			.Skip(offset)
			.Take(limit)
			.Build();

		var response = await _igdbService.GetAsync<IEnumerable<Game>>(query, Endpoint);

		return response;
	}
}
