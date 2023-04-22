using Microsoft.AspNetCore.Mvc;

using RateGames.Services.Interfaces;

namespace RateGames.Controllers;

/// <summary>
/// Controller for getting game engines from <see href="igdb.com"/>
/// </summary>
[Route("game-engines")]
[ApiController]
public class GameEngineController : ControllerBase
{
	private readonly IGameEngineService _gameEngineService;

	public GameEngineController(IGameEngineService gameEngineService) => 
		_gameEngineService = gameEngineService;

	[Route("{id}")]
	[HttpGet]
	public async Task<IActionResult> GetByIdAsync(int id)
	{
		var gameEngine = await _gameEngineService.GetByIdAsync(id);

		return gameEngine is null 
			? NotFound() 
			: Ok(gameEngine);
	}
}
