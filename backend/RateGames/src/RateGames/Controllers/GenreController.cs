using Microsoft.AspNetCore.Mvc;

using RateGames.Services.Interfaces;

namespace RateGames.Controllers;

/// <summary>
/// Controller for getting genres from <see href="igdb.com"/>
/// </summary>
[Route("genres")]
[ApiController]
public class GenreController : ControllerBase
{
	private readonly IGenreService _genreService;
	public GenreController(IGenreService genreService) => _genreService = genreService;

	[Route("{id}")]
	[HttpGet]
	public async Task<IActionResult> GetByIdAsync(int id)
	{
		var genre = await _genreService.GetByIdAsync(id);

		return genre is null
			? NotFound() 
			: Ok(genre);
	}
}
