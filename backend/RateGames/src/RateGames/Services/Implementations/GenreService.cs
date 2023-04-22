using Apicalypse.Core.Interfaces;

using RateGames.Models.Igdb;
using RateGames.Services.Interfaces;

namespace RateGames.Services.Implementations;

/// <inheritdoc cref="IGenreService"/>
public class GenreService : IGenreService
{

	private const string Endpoint = Endpoints.Genres;

	private readonly IIgdbService _igdbService;
	private readonly IQueryBuilderCreator _queryBuilderCreator;

	public GenreService(
		IIgdbService igdbService, 
		IQueryBuilderCreator queryBuilderCreator
	)
	{
		_igdbService = igdbService;
		_queryBuilderCreator = queryBuilderCreator;
	}

	public async Task<Genre?> GetByIdAsync(int id)
	{
		var query = _queryBuilderCreator.CreateFor<Genre>()
			.Select()
			.Where(g => g.Id == id)
			.Build();

		var response = await _igdbService.GetAsync<IEnumerable<Genre>>(query, Endpoint);
		var result = response?.FirstOrDefault();

		return result;
	}
}
