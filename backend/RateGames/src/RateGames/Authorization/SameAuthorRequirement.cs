using Microsoft.AspNetCore.Authorization;

namespace RateGames.Authorization;

/// <summary>
/// Requirement for <see cref="SameAuthorAuthorizationHandler"/>
/// </summary>
public class SameAuthorRequirement : IAuthorizationRequirement { }