using System.Security.Claims;

using Microsoft.AspNetCore.Authorization;

using RateGames.Models.Contracts;

namespace RateGames.Authorization;

/// <summary>
/// Handler for resource based authorization.
/// </summary>
public class SameAuthorAuthorizationHandler : AuthorizationHandler<SameAuthorRequirement, IResource>
{
	protected override Task HandleRequirementAsync(
		AuthorizationHandlerContext context,
		SameAuthorRequirement requirement,
		IResource resource
	)
	{
		ArgumentNullException.ThrowIfNull(resource);

		var userId = context.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

		if (userId == resource.UserId)
		{
			context.Succeed(requirement);
			return Task.CompletedTask;
		}
		context.Fail();

		return Task.CompletedTask;
	}
}
