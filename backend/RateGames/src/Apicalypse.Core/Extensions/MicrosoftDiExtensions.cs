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
		services.AddTransient<IExpressionParser<MemberExpression>, MemberExpressionParser>();
		services.AddTransient<IExpressionParser<ConstantExpression>, ConstantExpressionParser>();
		services.AddTransient<IExpressionParser<UnaryExpression>, UnaryExpressionParser>();
		services.AddTransient<IExpressionParser<NewArrayExpression>, NewArrayExpressionParser>();
		services.AddTransient<IExpressionParser<NewExpression>, NewExpressionParser>();
		services.AddTransient<IExpressionParser<BinaryExpression>, BinaryExpressionParser>();
		services.AddTransient<IExpressionParser<MethodCallExpression>, MethodCallExpressionParser>();
		services.AddTransient<IQueryParser, QueryParser>();
		services.AddTransient<IQueryBuilderCreator, QueryBuilderCreator>();

		return services;
	}
}