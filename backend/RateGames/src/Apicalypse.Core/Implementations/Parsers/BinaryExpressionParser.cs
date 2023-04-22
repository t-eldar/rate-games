using System.Text;

using Apicalypse.Core.Interfaces.ExpressionParsers;
using Apicalypse.Core.StringEnums;

namespace Apicalypse.Core.Implementations.Parsers;

/// <inheritdoc cref="IBinaryExpressionParser"/>
internal class BinaryExpressionParser : IBinaryExpressionParser
{
	private readonly IMemberExpressionParser _memberParser;
	private readonly IConstantExpressionParser _constantParser;
	private readonly IMethodCallExpressionParser _methodCallParser;

	public BinaryExpressionParser(
		IMemberExpressionParser memberParser,
		IConstantExpressionParser constantParser,
		IMethodCallExpressionParser methodCallParser
	)
	{
		_memberParser = memberParser;
		_constantParser = constantParser;
		_methodCallParser = methodCallParser;
	}

	public string Parse(BinaryExpression expression)
	{
		var binary = expression;
		var left = ParsePart(binary.Left);
		var right = ParsePart(binary.Right);
		var sign = GetSign(expression.NodeType);

		return $"{left}{sign}{right}";
	}
	public string Parse(BinaryExpression expression, StringBuilder stringBuilder)
	{
		stringBuilder.Clear();
		var binary = expression;
		var left = ParsePart(binary.Left, stringBuilder);
		var right = ParsePart(binary.Right, stringBuilder);
		var sign = GetSign(expression.NodeType);

		stringBuilder.Insert(0, left);
		stringBuilder.Append(sign);
		stringBuilder.Append(right);

		var result = stringBuilder.ToString();
		stringBuilder.Clear();

		return result;
	}

	private string ParsePart(Expression expression, StringBuilder stringBuilder) => expression switch
	{
		MemberExpression member => _memberParser.Parse(member, stringBuilder),
		ConstantExpression constant => _constantParser.Parse(constant, stringBuilder),
		MethodCallExpression methodCall => _methodCallParser.Parse(methodCall, stringBuilder),
		BinaryExpression binary => Parse(binary, stringBuilder),
		_ => throw new ArgumentException($"Expression {expression} cannot be part of binary")
	};
	private string ParsePart(Expression expression) => expression switch
	{
		MemberExpression member => _memberParser.Parse(member),
		ConstantExpression constant => _constantParser.Parse(constant),
		MethodCallExpression methodCall => _methodCallParser.Parse(methodCall),
		BinaryExpression binary => Parse(binary),
		_ => throw new ArgumentException($"Expression {expression} cannot be part of binary")
	};

	private string GetSign(ExpressionType expressionType) => expressionType switch
	{
		ExpressionType.AndAlso => QueryChars.AndSpaced,
		ExpressionType.OrElse => QueryChars.OrSpaced,
		ExpressionType.Equal => QueryChars.EqualSpaced,
		ExpressionType.NotEqual => QueryChars.NotEqualSpaced,
		ExpressionType.GreaterThan => QueryChars.GreaterThanSpaced,
		ExpressionType.GreaterThanOrEqual => QueryChars.GreaterThanOrEqualSpaced,
		ExpressionType.LessThan => QueryChars.LessThanSpaced,
		ExpressionType.LessThanOrEqual => QueryChars.LessThanOrEqualSpaced,
		_ => throw new Exception("ExpressionType should be logical, except denial ('!')!"),
	};
}
