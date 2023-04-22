using RateGames.Models.Igdb;

namespace RateGames.Services.Interfaces;

/// <summary>
/// Service for receiving images from <see href="igdb.com"/>
/// </summary>
public interface IImageService
{
	Task<Image?> GetScreenshotByIdAsync(int id);
	Task<IEnumerable<Image>?> GetScreenshotsByIdAsync(IEnumerable<int> ids);

	Task<Image?> GetCompanyLogoByIdAsync(int id);
	Task<IEnumerable<Image>?> GetCompanyLogosByIdsAsync(IEnumerable<int> ids);

	Task<Image?> GetGameEngineLogoByIdAsync(int id);
	Task<IEnumerable<Image>?> GetGameEngineLogosByIdsAsync(IEnumerable<int> ids);


	Task<Image?> GetGamePlatformLogoByIdAsync(int id);
	Task<IEnumerable<Image>?> GetGamePlatformLogosByIdsAsync(IEnumerable<int> ids);

}
