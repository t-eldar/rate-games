using Apicalypse.Core.Interfaces.ExpressionParsers;
using Apicalypse.Core.StringEnums;

using RateGames.Common.Extensions;

namespace Apicalypse.Core.Implementations.Parsers;

/// <inheritdoc cref="IMethodCallExpressionParser}"/>
internal class MethodCallExpressionParser : IMethodCallExpressionParser
{
	private readonly IMemberExpressionParser _memberParser;
	private readonly IConstantExpressionParser _constantParser;
	private readonly INewArrayExpressionParser _newArrayParser;

	private readonly string[] _stringMethods = new[]
	{
		nameof(string.StartsWith),
		nameof(string.EndsWith),
		nameof(string.Contains)
	};
	private readonly string[] _enumerableMethods = new[]
	{
		nameof(EnumerableExtensions.ContainsAll),
		nameof(EnumerableExtensions.ContainsAny),
	};
	private readonly string[] _includeMethods = new[]
	{
		nameof(EntityMarkerExtensions.IncludeProperty),
		nameof(EntityMarkerExtensions.IncludeAllProperties)
	};

	public MethodCallExpressionParser(
		IMemberExpressionParser memberParser,
		IConstantExpressionParser constantParser,
		INewArrayExpressionParser newArrayParser
	)
	{
		_memberParser = memberParser;
		_constantParser = constantParser;
		_newArrayParser = newArrayParser;
	}

	public string Parse(MethodCallExpression expression) => Parse(expression, new StringBuilder());
	public string Parse(MethodCallExpression expression, StringBuilder stringBuilder)
	{
		var methodName = expression.Method.Name;
		if (_stringMethods.Contains(methodName))
		{
			return ParseStringMethod(expression, stringBuilder);
		}
		if (_enumerableMethods.Contains(methodName))
		{
			return ParseEnumerableMethod(expression, stringBuilder);
		}
		if (_includeMethods.Contains(methodName))
		{
			return ParseIncludeMethod(expression, stringBuilder);
		}

		throw new ArgumentException($"Cannot parse method {methodName}");
	}

	private string ParseIncludeMethod(MethodCallExpression expression, StringBuilder stringBuilder) => expression.Method.Name switch
	{
		nameof(EntityMarkerExtensions.IncludeAllProperties) => ParseIncludeAllProperties(expression, stringBuilder),
		nameof(EntityMarkerExtensions.IncludeProperty) => ParseIncludeOneProperty(expression, stringBuilder),
		_ => throw new ArgumentException($"Method named {expression.Method.Name} is not available to perform"),
	};
	private string ParseEnumerableMethod(MethodCallExpression expression, StringBuilder stringBuilder)
	{
		stringBuilder.Clear();
		var methodName = expression.Method.Name;
		var (leftBound, rightBound) = methodName switch
		{
			nameof(EnumerableExtensions.ContainsAny) => (QueryChars.ParenthesisLeftChar, QueryChars.ParenthesisRightChar),
			nameof(Enumerable.SequenceEqual) => (QueryChars.CurlyBraceLeftChar, QueryChars.CurlyBraceRightChar),
			nameof(EnumerableExtensions.ContainsAll) => (QueryChars.ParenthesisLeftChar, QueryChars.ParenthesisRightChar),
			_ => throw new ArgumentException("Method is not available to perform"),
		};

		var caller = ParsePart(expression.Arguments[0]);
		var array = ParsePart(expression.Arguments[1], stringBuilder);
		stringBuilder.Append(caller);
		stringBuilder.Append(QueryChars.EqualSpaced);
		stringBuilder.Append(leftBound);
		stringBuilder.Append(array);
		stringBuilder.Append(rightBound);

		var result = stringBuilder.ToString();
		stringBuilder.Clear();

		return result;
	}
	private string ParseStringMethod(MethodCallExpression expression, StringBuilder stringBuilder)
	{
		stringBuilder.Clear();
		var methodName = expression.Method.Name;
		var (startChar, endChar) = methodName switch
		{
			nameof(string.StartsWith) => (string.Empty, QueryChars.StringMatcher),
			nameof(string.EndsWith) => (QueryChars.StringMatcher, string.Empty),
			nameof(string.Contains) => (QueryChars.StringMatcher, QueryChars.StringMatcher),
			_ => throw new NotImplementedException(),
		};

		var caller = ParsePart(expression.Object!, stringBuilder);
		var comparisonChar = string.Empty;

		var firstArgExpression = expression.Arguments[0];
		if (firstArgExpression is not ConstantExpression constantExpression)
		{
			throw new ArgumentException("Method should recieve only constants");
		}

		if (expression.Arguments.Count == 1)
		{
			comparisonChar = QueryChars.EqualSpaced;
		}
		else if (expression.Arguments.Count == 2)
		{
			comparisonChar = GetComparisonCharFromTwoArguments(expression);
		}
		else if (expression.Arguments.Count == 3)
		{
			comparisonChar = GetComparisonCharFromThreeArguments(expression);
		}

		var stringValue = ParsePart(constantExpression, stringBuilder);

		stringBuilder.Append(caller);
		stringBuilder.Append(comparisonChar);
		stringBuilder.Append(startChar);
		stringBuilder.Append(stringValue);
		stringBuilder.Append(endChar);
		var result = stringBuilder.ToString();
		stringBuilder.Clear();

		return result;
	}
	private string ParseIncludeOneProperty(MethodCallExpression expression, StringBuilder stringBuilder)
	{
		var caller = ParsePart(expression.Arguments[0], stringBuilder);
		var including = ParsePart(expression.Arguments[1], stringBuilder);
		stringBuilder.Append(caller);
		stringBuilder.Append(QueryChars.AccessSeparatorChar);
		stringBuilder.Append(including);
		var result = stringBuilder.ToString();
		stringBuilder.Clear();

		return result;
	}
	private string ParseIncludeAllProperties(MethodCallExpression expression, StringBuilder stringBuilder)
	{
		stringBuilder.Clear();
		var caller = ParsePart(expression.Arguments[0]);
		stringBuilder.Append(caller);
		stringBuilder.Append(QueryChars.AccessSeparatorChar);
		stringBuilder.Append(QueryChars.AllProperiesChar);
		var result = stringBuilder.ToString();
		stringBuilder.Clear();

		return result;
	}
	private static string GetComparisonCharFromTwoArguments(MethodCallExpression expression)
	{
		var stringComparisonBoxed = (expression.Arguments[1] as ConstantExpression)?.Value
			?? throw new ArgumentException("Method should contain const arguments");
		var stringComparison = (StringComparison)stringComparisonBoxed;
		var comparisonChar = string.Empty;

		switch (stringComparison)
		{
			case StringComparison.Ordinal:
			case StringComparison.InvariantCulture:
			case StringComparison.CurrentCulture:
			{
				comparisonChar = QueryChars.EqualSpaced;
				break;
			}
			case StringComparison.OrdinalIgnoreCase:
			case StringComparison.InvariantCultureIgnoreCase:
			case StringComparison.CurrentCultureIgnoreCase:
			{
				comparisonChar = QueryChars.ApproximateSpaced;
				break;
			}
		}

		return comparisonChar;
	}
	private static string GetComparisonCharFromThreeArguments(MethodCallExpression expression)
	{
		string comparisonChar;
		var ignoreCaseBoxed = (expression.Arguments[1] as ConstantExpression)?.Value
			?? throw new ArgumentException("Method should contain const arguments");
		var ignoreCase = (bool)ignoreCaseBoxed;
		if (ignoreCase)
		{
			comparisonChar = QueryChars.ApproximateSpaced;
		}
		else
		{
			comparisonChar = QueryChars.EqualSpaced;
		}

		return comparisonChar;
	}

	private string ParsePart(Expression expression, StringBuilder stringBuilder) => expression switch
	{
		MemberExpression member => _memberParser.Parse(member, stringBuilder),
		MethodCallExpression methodCall => Parse(methodCall, stringBuilder),
		ConstantExpression constant => _constantParser.Parse(constant, stringBuilder),
		NewArrayExpression newArray => _newArrayParser.Parse(newArray, stringBuilder),
		UnaryExpression unary => ParseQuoteUnary(unary),
		_ => throw new ArgumentException($"Expression {expression} with {expression.NodeType} cannot be part of new object")
	};
	private string ParseQuoteUnary(UnaryExpression expression)
	{
		if (expression.NodeType != ExpressionType.Quote)
		{
			throw new ArgumentException("Unary expression may only be with node type Quote");
		}
		var lambda = expression.Operand as LambdaExpression
			?? throw new ArgumentException("Only lambda expression could be passed");

		return ParsePart(lambda.Body);
	}
	private string ParsePart(Expression expression) => expression switch
	{
		MemberExpression member => _memberParser.Parse(member),
		MethodCallExpression methodCall => Parse(methodCall),
		ConstantExpression constant => _constantParser.Parse(constant),
		NewArrayExpression newArray => _newArrayParser.Parse(newArray),
		_ => throw new ArgumentException($"Expression {expression} cannot be part of new object")
	};
}
