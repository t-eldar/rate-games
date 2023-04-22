using Apicalypse.Core.Interfaces;

using RateGames.Common.Extensions;
using RateGames.Models.Igdb;
using RateGames.Services.Interfaces;

namespace RateGames.Services.Implementations;

/// <inheritdoc cref="IImageService" />
public class ImageService : IImageService
{
	private readonly IIgdbService _igdbService;
	private readonly IQueryBuilderCreator _queryBuilderCreator;

	private const string ScreenshotsEndpoing = Endpoints.Screenshots;
	private const string PlatformLogosEndpoing = Endpoints.PlatformLogos;
	private const string CompanyLogosEndpoing = Endpoints.CompanyLogos;
	private const string GameEngineLogosEndpoing = Endpoints.GameEngineLogos;

	public ImageService(
		IIgdbService igdbService, 
		IQueryBuilderCreator queryBuilderCreator
	)
	{
		_igdbService = igdbService;
		_queryBuilderCreator = queryBuilderCreator;
	}

	public async Task<Image?> GetCompanyLogoByIdAsync(int id)
	{
		var query = _queryBuilderCreator.CreateFor<Image>()
			.Select()
			.Where(i => i.Id == id)
			.Build();

		var response = await _igdbService.GetAsync<IEnumerable<Image>>(query, CompanyLogosEndpoing);
		var result = response?.FirstOrDefault();

		return result;
	}
	public async Task<IEnumerable<Image>?> GetCompanyLogosByIdsAsync(IEnumerable<int> ids)
	{
		var query = _queryBuilderCreator.CreateFor<Image>()
			.Select()
			.Where(i => i.Id.ContainsAny<Image>(ids))
			.Build();

		var response = await _igdbService.GetAsync<IEnumerable<Image>>(query, CompanyLogosEndpoing);

		return response;
	}
	public async Task<Image?> GetGameEngineLogoByIdAsync(int id)
	{
		var query = _queryBuilderCreator.CreateFor<Image>()
			.Select()
			.Where(i => i.Id == id)
			.Build();

		var response = await _igdbService.GetAsync<IEnumerable<Image>>(query, GameEngineLogosEndpoing);
		var result = response?.FirstOrDefault();

		return result;
	}
	public async Task<IEnumerable<Image>?> GetGameEngineLogosByIdsAsync(IEnumerable<int> ids)
	{
		var query = _queryBuilderCreator.CreateFor<Image>()
			.Select()
			.Where(i => i.Id.ContainsAny<Image>(ids))
			.Build();

		var response = await _igdbService.GetAsync<IEnumerable<Image>>(query, GameEngineLogosEndpoing);

		return response;
	}
	public async Task<Image?> GetGamePlatformLogoByIdAsync(int id)
	{
		var query = _queryBuilderCreator.CreateFor<Image>()
			.Select()
			.Where(i => i.Id == id)
			.Build();

		var response = await _igdbService.GetAsync<IEnumerable<Image>>(query, PlatformLogosEndpoing);
		var result = response?.FirstOrDefault();

		return result;
	}
	public async Task<IEnumerable<Image>?> GetGamePlatformLogosByIdsAsync(IEnumerable<int> ids)
	{
		var query = _queryBuilderCreator.CreateFor<Image>()
			.Select()
			.Where(i => i.Id.ContainsAny<Image>(ids))
			.Build();

		var response = await _igdbService.GetAsync<IEnumerable<Image>>(query, PlatformLogosEndpoing);

		return response;
	}
	public async Task<Image?> GetScreenshotByIdAsync(int id)
	{
		var query = _queryBuilderCreator.CreateFor<Image>()
			.Select()
			.Where(i => i.Id == id)
			.Build();

		var response = await _igdbService.GetAsync<IEnumerable<Image>>(query, ScreenshotsEndpoing);
		var result = response?.FirstOrDefault();

		return result;
	}
	public async Task<IEnumerable<Image>?> GetScreenshotsByIdAsync(IEnumerable<int> ids)
	{
		var query = _queryBuilderCreator.CreateFor<Image>()
			.Select()
			.Where(i => i.Id.ContainsAny<Image>(ids))
			.Build();

		var response = await _igdbService.GetAsync<IEnumerable<Image>>(query, ScreenshotsEndpoing);

		return response;
	}
}
