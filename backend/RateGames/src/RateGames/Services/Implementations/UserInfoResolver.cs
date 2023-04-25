using System.Security.Claims;

using Microsoft.AspNetCore.Identity;

using RateGames.Exceptions;
using RateGames.Models.Entities;
using RateGames.Models.Responses;
using RateGames.Services.Interfaces;

namespace RateGames.Services.Implementations;

/// <inheritdoc cref="IUserInfoResolver"/>
public class UserInfoResolver : IUserInfoResolver
{
	private readonly UserManager<User> _userManager;
	public UserInfoResolver(UserManager<User> userManager) => _userManager = userManager;

	public async Task<UserInfo> ResolveAsync(IEnumerable<Claim> claims)
	{
		ArgumentNullException.ThrowIfNull(claims);

		var id = claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value
			?? throw new InvalidClaimsException();
		var user = await _userManager.FindByIdAsync(id) ?? throw new InvalidClaimsException();

		return new()
		{
			Id = id,
			UserName = user.UserName,
			AvatarUrl = user.AvatarUrl ?? "",
		};
	}
}
