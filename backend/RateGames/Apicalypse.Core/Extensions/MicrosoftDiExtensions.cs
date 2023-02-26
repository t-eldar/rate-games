using Apicalypse.Core.Implementations;
using Apicalypse.Core.Interfaces;

using Microsoft.Extensions.DependencyInjection;

namespace Apicalypse.Core.Extensions;
public static class MicrosoftDiExtensions
{
	/// <summary>
	/// Adds transient <see cref="IQueryBuilderCreator" /> with its dependencies.
	/// </summary>
	/// <param name="services"></param>
	/// <returns></returns>
	public static IServiceCollection AddApicalypseQueryBuilderCreator(this IServiceCollection services)
	{
		services.AddTransient<IMethodPerformer, MethodPerformer>();
		services.AddTransient<IExpressionParser, ExpressionParser>();
		services.AddTransient<IQueryBuilderCreator, QueryBuilderCreator>();
		return services;
	}
}
