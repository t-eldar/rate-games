using Microsoft.AspNetCore.Mvc;

using RateGames.Models.Igdb;
using RateGames.Models.Requests;
using RateGames.Services.Interfaces;

namespace RateGames.Controllers;

/// <summary>
/// Controller for getting games from <see href="igdb.com"/>
/// </summary>
[Route("games")]
[ApiController]
public class GameController : ControllerBase
{
	private readonly IGameService _gameService;

	public GameController(IGameService gameService) => _gameService = gameService;

	[Route("{id}")]
	[HttpGet]
	public async Task<IActionResult> GetByIdAsync(int id)
	{
		var game = await _gameService.GetByIdAsync(id);

		return game is null
			? NotFound()
			: Ok(game);
	}

	[Route("by-genres")]
	[HttpGet]
	public async Task<IActionResult> GetByGenresAsync(int[] genreIds)
	{
		var games = await _gameService.GetByAllGenresAsync(genreIds);
		
		return games is null
			? NotFound()
			: Ok(games);
	}

	[Route("by-game-modes")]
	[HttpGet]
	public async Task<IActionResult> GetByGameModesAsync(int[] gameModeIds)
	{
		var games = await _gameService.GetByAllGameModesAsync(gameModeIds);

		return games is null
			? NotFound()
			: Ok(games);
	}

	[Route("by-platforms")]
	[HttpGet]
	public async Task<IActionResult> GetByPlatformsAsync(int[] platformIds)
	{
		var games = await _gameService.GetByAllPlatformsAsync(platformIds);

		return games is null
			? NotFound()
			: Ok(games);
	}

	[Route("by-search")]
	[HttpGet]
	public async Task<IActionResult> GetBySearchAsync(string search)
	{
		var games = await _gameService.GetBySearchAsync(search);

		return games is null
			? NotFound()
			: Ok(games);
	}
}
