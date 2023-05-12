using System.Security.Claims;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using RateGames.Authorization;
using RateGames.Models.Requests;
using RateGames.Repositories.Interfaces;

namespace RateGames.Controllers;

[ApiController]
[Route("reviews")]
public class ReviewController : ControllerBase
{
	private const int Limit = 100;
	private readonly IReviewRepository _reviewRepository;
	private readonly IAuthorizationService _authorizationService;
	private readonly IRatingRepository _ratingRepository;

	public ReviewController(
		IReviewRepository reviewRepository,
		IAuthorizationService authorizationService,
		IRatingRepository ratingRepository
	)
	{
		_reviewRepository = reviewRepository;
		_authorizationService = authorizationService;
		_ratingRepository = ratingRepository;
	}

	[HttpGet]
	public async Task<IActionResult> GetAllAsync(int limit = Limit, int offset = 0)
	{
		var result = await _reviewRepository.GetAllAsync(limit, offset);

		return result is null
			? NotFound()
			: Ok(result);
	}

	[Route("by-user/{userId}")]
	[HttpGet]
	public async Task<IActionResult> GetByUserAsync(string userId, int limit = Limit, int offset = 0)
	{
		var result = await _reviewRepository.GetByUserAsync(userId, limit, offset);

		return result is null
			? NotFound()
			: Ok(result);
	}

	[Route("by-game/{gameId}")]
	[HttpGet]
	public async Task<IActionResult> GetByGameAsync(int gameId, int limit = Limit, int offset = 0)
	{
		var result = await _reviewRepository.GetByGameAsync(gameId, limit, offset);

		return result is null
			? NotFound()
			: Ok(result);
	}

	[Route("{id}")]
	[HttpGet]
	public async Task<IActionResult> GetByIdAsync(int id)
	{
		var result = await _reviewRepository.GetByIdAsync(id);

		return result is null
			? NotFound()
			: Ok(result);
	}

	[HttpPost]
	[Authorize]
	public async Task<IActionResult> CreateAsync(CreateReviewApiRequest request)
	{
		var idClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
		if (idClaim is null)
		{
			return Unauthorized();
		}

		var rating = await _ratingRepository.GetByValueAsync(request.Rating);

		if (rating is null)
		{
			return BadRequest();
		}
		var serviceRequest = request.ToCreateReviewRequest(idClaim.Value, rating.Id);

		var review = await _reviewRepository.CreateAsync(serviceRequest);

		return review is null
			? StatusCode(StatusCodes.Status500InternalServerError)
			: CreatedAtRoute(new { review.Id }, review);
	}

	[Route("{id}")]
	[HttpPut]
	[Authorize]
	public async Task<IActionResult> UpdateAsync(int id, UpdateReviewRequest request)
	{
		if (HttpContext.User.Identity?.IsAuthenticated ?? true)
		{
			return Challenge();
		}
		if (id != request.Id)
		{
			return BadRequest("Id from request object is not equal to id in params");
		}

		var review = await _reviewRepository.GetByIdAsync(id);

		var authorizationResult = await _authorizationService.AuthorizeAsync(
			HttpContext.User,
			review,
			AuthorizationPolicies.SameAuthor
		);

		if (!authorizationResult.Succeeded)
		{
			return Forbid();
		}
		if (review is null)
		{
			return NotFound(); 
		}

		await _reviewRepository.UpdateAsync(request);

		return Ok();
	}

	[Route("{id}")]
	[HttpDelete]
	[Authorize]
	public async Task<IActionResult> DeleteAsync(int id)
	{
		if (HttpContext.User.Identity?.IsAuthenticated ?? true)
		{
			return Challenge();
		}

		var review = await _reviewRepository.GetByIdAsync(id);

		var authorizationResult = await _authorizationService.AuthorizeAsync(
			HttpContext.User,
			review,
			AuthorizationPolicies.SameAuthor
		);

		if (!authorizationResult.Succeeded)
		{
			return Forbid();
		}
		if (review is null)
		{
			return NoContent();
		}

		await _reviewRepository.DeleteAsync(id);

		return Ok();
	}
}
