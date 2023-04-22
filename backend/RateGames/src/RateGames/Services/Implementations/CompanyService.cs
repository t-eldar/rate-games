using Apicalypse.Core.Interfaces;

using RateGames.Models.Igdb;
using RateGames.Services.Interfaces;

namespace RateGames.Services.Implementations;

/// <inheritdoc <see cref="ICompanyService"/>/>
public class CompanyService : ICompanyService
{
	private const string Endpoint = Endpoints.Companies;
	private readonly IQueryBuilderCreator _queryBuilderCreator;
	private readonly IIgdbService _igdbService;

	public CompanyService(
		IIgdbService igdbService,
		IQueryBuilderCreator queryBuilderCreator 
	)
	{
		_queryBuilderCreator = queryBuilderCreator;
		_igdbService = igdbService;
	}
	public async Task<Company?> GetByIdAsync(int id)
	{
		var query = _queryBuilderCreator.CreateFor<Company>()
			.Select()
			.Where(c => c.Id == id)
			.Build();

		var response = await _igdbService.GetAsync<IEnumerable<Company>>(query, Endpoint);
		var result = response?.FirstOrDefault();

		return result;
	}
	public async Task<IEnumerable<Company>?> GetAllByCountryAsync(int code)
	{
		var query = _queryBuilderCreator.CreateFor<Company>()
			.Select()
			.Where(c => c.Country == code)
			.Build();

		var response = await _igdbService.GetAsync<IEnumerable<Company>>(query, Endpoint);

		return response;
	}
}
