using Microsoft.AspNetCore.Mvc;

using RateGames.Services.Interfaces;

namespace RateGames.Controllers;

/// <summary>
/// Controller for getting game platforms from <see href="igdb.com"/>
/// </summary>
[Route("platforms")]
[ApiController]
public class PlatformController : ControllerBase
{
	private readonly IPlatformService _platformService;

	public PlatformController(IPlatformService platformService) => _platformService = platformService;

	[Route("{id}")]
	[HttpGet]
	public async Task<IActionResult> GetByIdAsync(int id)
	{
		var platform = await _platformService.GetByIdAsync(id);

		return platform is null
			? NotFound() 
			: Ok(platform);
	}
}
