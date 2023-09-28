using Apicalypse.Core.Interfaces;
using Apicalypse.Core.StringEnums;

namespace Apicalypse.Core.Implementations.Parsers;


/// <inheritdoc cref="IExpressionParser{NewArrayExpression}"/>
internal class NewArrayExpressionParser : IExpressionParser<NewArrayExpression>
{
	private readonly IExpressionParser<ConstantExpression> _constantParser;
	private readonly IExpressionParser<MemberExpression> _memberParser;

	public NewArrayExpressionParser(
		IExpressionParser<ConstantExpression> constantParser,
		IExpressionParser<MemberExpression> memberParser
	)
	{
		_constantParser = constantParser;
		_memberParser = memberParser;
	}

	/// <exception cref="ArgumentOutOfRangeException">Throws by inner <see cref="StringBuilder"/></exception>
	/// <exception cref="ArgumentException">Throws if array initialized wrong</exception>
	public string Parse(NewArrayExpression expression)
	{
		var stringBuilder = new StringBuilder();
		if (expression.NodeType == ExpressionType.NewArrayBounds)
		{
			throw new ArgumentException("Array should be initialized in the expression");
		}

		foreach (var exp in expression.Expressions)
		{
			if (exp is ConstantExpression constantExpression)
			{
				stringBuilder.Append(_constantParser.Parse(constantExpression));
				stringBuilder.Append(QueryChars.ValueSeparatorChar);
				continue;
			}
			else if (exp is MemberExpression memberExpression)
			{
				stringBuilder.Append(_memberParser.Parse(memberExpression));
				stringBuilder.Append(QueryChars.ValueSeparatorChar);
				continue;
			}
			throw new ArgumentException("Array should be initializd by not null constants");
		}

		return stringBuilder.ToString()[..^1];
	}
}
