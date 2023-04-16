using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

using RateGames.Models.Entities;
using RateGames.Models.Requests;

namespace RateGames.Controllers;

/// <summary>
/// Controller for logging in and registering users.
/// </summary>
[ApiController]
public class AuthenticationController : ControllerBase
{
	private readonly UserManager<User> _userManager;
	private readonly SignInManager<User> _signInManager;

	public AuthenticationController(UserManager<User> userManager, SignInManager<User> signInManager)
	{
		_userManager = userManager;
		_signInManager = signInManager;
	}

	[Route("/sign-up")]
	[HttpPost]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> SignUpAsync(SignUpRequest request)
	{
		var user = new User 
		{ 
			UserName = request.Username, 
			Email = request.Email, 
		};

		var registerResult = await _userManager.CreateAsync(user, request.Password);
		if (!registerResult.Succeeded)
		{
			return BadRequest("Incorrect username, email or password");
		}

		var signInResult = await _signInManager.PasswordSignInAsync(user, request.Password, true, false);

		return signInResult.Succeeded ? Ok() : Unauthorized();
	}

	[Route("/sign-in")]
	[HttpPost]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> SignInAsync(SignInRequest request)
	{
		var user = await _userManager.FindByEmailAsync(request.UsernameOrEmail)
			?? await _userManager.FindByNameAsync(request.UsernameOrEmail);
		if (user is null)
		{
			return Unauthorized();
		}

		var result = await _signInManager.PasswordSignInAsync(user, request.Password, request.RememberMe, false);

		return result.Succeeded ? Ok() : Unauthorized();
	}

	[Route("/sign-out")]
	[HttpGet]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> SignOutAsync(HttpContext context)
	{
		if (context.User is null)
		{
			return Unauthorized();
		}

		await _signInManager.SignOutAsync();

		return Ok();
	}
}
