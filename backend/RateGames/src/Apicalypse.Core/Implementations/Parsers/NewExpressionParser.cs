using Apicalypse.Core.Interfaces;
using Apicalypse.Core.StringEnums;

namespace Apicalypse.Core.Implementations.Parsers;

/// <inheritdoc cref="IExpressionParser{NewExpression}"/>
internal class NewExpressionParser : IExpressionParser<NewExpression>
{
	private readonly IExpressionParser<MemberExpression> _memberParser;
	private readonly IExpressionParser<MethodCallExpression> _methodCallParser;

	public NewExpressionParser(
		IExpressionParser<MemberExpression> memberParser, 
		IExpressionParser<MethodCallExpression> methodCallParser
	)
	{
		_memberParser = memberParser;
		_methodCallParser = methodCallParser;
	}

	/// <exception cref="ArgumentException">Throws if object initialized wrong</exception>
	/// <exception cref="ArgumentOutOfRangeException">Throws by inner <see cref="StringBuilder"/></exception>
	public string Parse(NewExpression expression)
	{
		var stringBuilder = new StringBuilder();
		var arguments = expression.Arguments;
		foreach (var argument in arguments)
		{
			switch (argument)
			{
				case MemberExpression:
				case MethodCallExpression:
				{
					var parsed = ParsePart(argument);
					stringBuilder.Append(parsed);
					stringBuilder.Append(QueryChars.ValueSeparatorChar);
					break;
				}
				default:
				{
					throw new ArgumentException("New object should contain only member access expressions.");
				}
			}
		}

		return stringBuilder.ToString()[..^1];
	}


	/// <exception cref="ArgumentException"></exception>
	private string ParsePart(Expression expression) => expression switch
	{
		MemberExpression member => _memberParser.Parse(member),
		MethodCallExpression methodCall => _methodCallParser.Parse(methodCall),
		_ => throw new ArgumentException($"Expression {expression} with {expression.NodeType} cannot be part of new object")
	};
}
