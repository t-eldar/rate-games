using FluentValidation;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

using RateGames.Exceptions;

namespace RateGames.Controllers;

/// <summary>
/// Error handling controller
/// </summary>
[ApiController]
[ApiExplorerSettings(IgnoreApi = true)]
public class ErrorController : ControllerBase
{
	[Route("error")]
	[AllowAnonymous]
	public IActionResult HandleError()
	{
		var exceptionHandlerFeature = HttpContext.Features.Get<IExceptionHandlerFeature>();

		if (exceptionHandlerFeature is null)
		{
			return Problem();
		}

		var exception = exceptionHandlerFeature.Error;

		var result = exception switch
		{
			ValidationException => ValidationProblem(),
			EntityNotFoundException => NotFound(),
			InvalidClaimsException => Forbid(),
			_ => Problem(),
		};

		return result;
	}

	[Route("error-development")]
	public IActionResult HandleErrorDevelopment()
	{
		var exceptionHandlerFeature = HttpContext.Features.Get<IExceptionHandlerFeature>();
		if (exceptionHandlerFeature is null)
		{
			return Problem();
		}

		return Problem(
			detail: exceptionHandlerFeature.Error.StackTrace,
			title: exceptionHandlerFeature.Error.Message
		);
	}
}
