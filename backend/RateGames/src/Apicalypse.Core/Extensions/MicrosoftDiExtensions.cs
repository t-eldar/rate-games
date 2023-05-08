using Apicalypse.Core.Implementations;
using Apicalypse.Core.Implementations.Parsers;
using Apicalypse.Core.Interfaces;
using Apicalypse.Core.Interfaces.ExpressionParsers;

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
		services.AddSingleton<IMemberInfoStorage, MemberInfoStorage>();
		services.AddTransient<IMemberExpressionParser, MemberExpressionParser>();
		services.AddTransient<IConstantExpressionParser, ConstantExpressionParser>();
		services.AddTransient<IUnaryExpressionParser, UnaryExpressionParser>();
		services.AddTransient<INewArrayExpressionParser, NewArrayExpressionParser>();
		services.AddTransient<INewExpressionParser, NewExpressionParser>();
		services.AddTransient<IBinaryExpressionParser, BinaryExpressionParser>();
		services.AddTransient<IMethodCallExpressionParser, MethodCallExpressionParser>();
		services.AddTransient<IQueryParser, QueryParser>();
		services.AddTransient<IQueryBuilderCreator, QueryBuilderCreator>();

		return services;
	}
}