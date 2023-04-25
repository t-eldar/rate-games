using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

using RateGames.Models.Entities;
using RateGames.Models.Requests;
using RateGames.Services.Interfaces;

namespace RateGames.Controllers;

/// <summary>
/// Controller for logging in and registering users.
/// </summary>
[ApiController]
public class AuthenticationController : ControllerBase
{
	private readonly UserManager<User> _userManager;
	private readonly SignInManager<User> _signInManager;
	private readonly IUserInfoResolver _userInfoResolver;

	public AuthenticationController(
		UserManager<User> userManager, 
		SignInManager<User> signInManager, 
		IUserInfoResolver userInfoResolver
	)
	{
		_userManager = userManager;
		_signInManager = signInManager;
		_userInfoResolver = userInfoResolver;
	}

	[Route("sign-up")]
	[HttpPost]
	public async Task<IActionResult> SignUpAsync(SignUpRequest request)
	{
		var user = request.ToUser();

		var registerResult = await _userManager.CreateAsync(user, request.Password);
		if (!registerResult.Succeeded)
		{
			return BadRequest("Incorrect username, email or password");
		}

		var signInResult = await _signInManager.PasswordSignInAsync(user, request.Password, true, false);

		return signInResult.Succeeded ? Ok() : Unauthorized();
	}

	[Route("sign-in")]
	[HttpPost]
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
	public async Task<IActionResult> SignOutAsync()
	{
		if (HttpContext.User is null)
		{
			return Unauthorized();
		}

		await _signInManager.SignOutAsync();

		return Ok();
	}

	[Route("/user-info")]
	[Authorize]
	[HttpGet]
	public async Task<IActionResult> GetUserInfoAsync()
	{
		var principal = HttpContext.User;
		if (principal?.Claims is null || (!principal?.Identity?.IsAuthenticated ?? true))
		{
			return Unauthorized();
		}
		var userInfo = await _userInfoResolver.ResolveAsync(principal!.Claims);

		return Ok(userInfo);
	}
}
