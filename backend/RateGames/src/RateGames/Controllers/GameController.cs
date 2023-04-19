using Microsoft.AspNetCore.Mvc;

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
		return NotFound();
	}
}
