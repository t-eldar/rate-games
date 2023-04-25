using System.Security.Claims;

using RateGames.Models.Responses;

namespace RateGames.Services.Interfaces;

/// <summary>
/// Service for getting user info from <see cref="IEnumerable{T}"/> whose generic type argument is <see cref="Claim"/>
/// </summary>
public interface IUserInfoResolver
{
	Task<UserInfo> ResolveAsync(IEnumerable<Claim> claims);
}
