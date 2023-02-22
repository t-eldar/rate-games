using Microsoft.Extensions.DependencyInjection;

using Apicalypse.Core.Interfaces;
using Apicalypse.Core.Implementations;

namespace Apicalypse.Ioc;
public static class MicrosoftDependencyInjectionExtensions
{
	public static IServiceCollection AddApicalypseQueryBuilder(this IServiceCollection services)
	{
		services.AddTransient<IMethodPerformer, MethodPerformer>();
	}
}
