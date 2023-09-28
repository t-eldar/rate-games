using Apicalypse.Core.Interfaces;
using Apicalypse.Core.StringEnums;

namespace Apicalypse.Core.Implementations.Parsers;

/// <inheritdoc cref="IExpressionParser{BinaryExpression}"/>
internal class BinaryExpressionParser : IExpressionParser<BinaryExpression>
{
	private readonly IExpressionParser<MemberExpression> _memberParser;
	private readonly IExpressionParser<ConstantExpression> _constantParser;
	private readonly IExpressionParser<MethodCallExpression> _methodCallParser;
	private readonly IExpressionParser<UnaryExpression> _unaryParser;

	public BinaryExpressionParser(
		IExpressionParser<MemberExpression> memberParser,
		IExpressionParser<ConstantExpression> constantParser, 
		IExpressionParser<MethodCallExpression> methodCallParser,
		IExpressionParser<UnaryExpression> unaryParser
	)
	{
		_memberParser = memberParser;
		_constantParser = constantParser;
		_methodCallParser = methodCallParser;
		_unaryParser = unaryParser;
	}

	/// <exception cref="ArgumentException">Throws by inner methods</exception>"
	/// <exception cref="ArgumentOutOfRangeException">Throws by inner methods</exception>"
	public string Parse(BinaryExpression expression)
	{
		var stringBuilder = new StringBuilder();
		
		var binary = expression;
		var left = ParsePart(binary.Left);
		var right = ParsePart(binary.Right);
		var sign = GetSign(expression.NodeType);

		stringBuilder.Insert(0, left);
		stringBuilder.Append(sign);
		stringBuilder.Append(right);

		var result = stringBuilder.ToString();

		return result;
	}

	/// <exception cref="ArgumentException">If expression cannot be parsed as part of binary expression</exception>"
	private string ParsePart(Expression expression) => expression switch
	{
		MemberExpression member => _memberParser.Parse(member),
		ConstantExpression constant => _constantParser.Parse(constant),
		MethodCallExpression methodCall => _methodCallParser.Parse(methodCall),
		UnaryExpression unary => _unaryParser.Parse(unary),
		BinaryExpression binary => Parse(binary),
		_ => throw new ArgumentException($"Expression {expression} cannot be part of binary")
	};

	/// <exception cref="ArgumentException">If cannot get sign from expression</exception>"
	private static string GetSign(ExpressionType expressionType) => expressionType switch
	{
		ExpressionType.AndAlso => QueryChars.AndSpaced,
		ExpressionType.OrElse => QueryChars.OrSpaced,
		ExpressionType.Equal => QueryChars.EqualSpaced,
		ExpressionType.NotEqual => QueryChars.NotEqualSpaced,
		ExpressionType.GreaterThan => QueryChars.GreaterThanSpaced,
		ExpressionType.GreaterThanOrEqual => QueryChars.GreaterThanOrEqualSpaced,
		ExpressionType.LessThan => QueryChars.LessThanSpaced,
		ExpressionType.LessThanOrEqual => QueryChars.LessThanOrEqualSpaced,
		_ => throw new ArgumentException("Expression type should be logical except denial ('!')"),
	};
}
