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
	private const int DefaultLimit = 10;
	private const int DefaultOffset = 0;
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
	public async Task<IActionResult> GetByGenresAsync(
		[FromQuery] int[] genreIds,
		int limit = DefaultLimit, int
		offset = DefaultOffset
	)
	{
		var games = await _gameService.GetByAllGenresAsync(genreIds, limit, offset);

		return games is null
			? NotFound()
			: Ok(games);
	}

	[Route("by-game-modes")]
	[HttpGet]
	public async Task<IActionResult> GetByGameModesAsync(
		[FromQuery] int[] gameModeIds,
		int limit = DefaultLimit,
		int offset = DefaultOffset
	)
	{
		var games = await _gameService.GetByAllGameModesAsync(gameModeIds, limit, offset);

		return games is null
			? NotFound()
			: Ok(games);
	}

	[Route("by-platforms")]
	[HttpGet]
	public async Task<IActionResult> GetByPlatformsAsync(
		[FromQuery] int[] platformIds,
		int limit = DefaultLimit,
		int offset = DefaultOffset
	)
	{
		var games = await _gameService.GetByAllPlatformsAsync(platformIds, limit, offset);

		return games is null
			? NotFound()
			: Ok(games);
	}

	[Route("by-search")]
	[HttpGet]
	public async Task<IActionResult> GetBySearchAsync(
		string search,
		int limit = DefaultLimit,
		int offset = DefaultOffset
	)
	{
		var games = await _gameService.GetBySearchAsync(search, limit, offset);

		return games is null
			? NotFound()
			: Ok(games);
	}

	[Route("by-ids")]
	[HttpGet]
	public async Task<IActionResult> GetByIdsAsync(
		[FromQuery] int[] ids,
		int limit = DefaultLimit,
		int offset = DefaultOffset
	)
	{
		var games = await _gameService.GetByIdsAsync(ids, limit, offset);
		return games is null
			? NotFound()
			: Ok(games);
	}

	[Route("latest")]
	[HttpGet]
	public async Task<IActionResult> GetLatestAsync(int limit = DefaultLimit, int offset = DefaultOffset)
	{
		var games = await _gameService.GetLatestAsync(limit, offset);

		return games is null
			? NotFound()
			: Ok(games);
	}
}
