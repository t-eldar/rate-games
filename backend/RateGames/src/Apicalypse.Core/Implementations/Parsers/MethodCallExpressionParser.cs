using Apicalypse.Core.Interfaces;
using Apicalypse.Core.StringEnums;

using RateGames.Common.Extensions;

namespace Apicalypse.Core.Implementations.Parsers;

/// <inheritdoc cref="IExpressionParser{MethodCallExpression}"/>
internal class MethodCallExpressionParser : IExpressionParser<MethodCallExpression>
{
	private readonly IExpressionParser<MemberExpression> _memberParser;
	private readonly IExpressionParser<ConstantExpression> _constantParser;
	private readonly IExpressionParser<NewArrayExpression> _newArrayParser;

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
		IExpressionParser<MemberExpression> memberParser,
		IExpressionParser<ConstantExpression> constantParser, 
		IExpressionParser<NewArrayExpression> newArrayParser
	)
	{
		_memberParser = memberParser;
		_constantParser = constantParser;
		_newArrayParser = newArrayParser;
	}

	/// <exception cref="ArgumentOutOfRangeException">Throws by inner <see cref="StringBuilder"/></exception>
	/// <exception cref="ArgumentException">Throws by inner methods</exception>
	/// <exception cref="NotImplementedException"></exception>
	public string Parse(MethodCallExpression expression)
	{
		var methodName = expression.Method.Name;
		if (_stringMethods.Contains(methodName))
		{
			return ParseStringMethod(expression);
		}
		if (_enumerableMethods.Contains(methodName))
		{
			return ParseEnumerableMethod(expression);
		}
		if (_includeMethods.Contains(methodName))
		{
			return ParseIncludeMethod(expression);
		}

		throw new NotImplementedException($"Cannot parse method {methodName}");
	}

	private string ParseIncludeMethod(MethodCallExpression expression) => expression.Method.Name switch
	{
		nameof(EntityMarkerExtensions.IncludeAllProperties) => ParseIncludeAllProperties(expression),
		nameof(EntityMarkerExtensions.IncludeProperty) => ParseIncludeOneProperty(expression),
		_ => throw new ArgumentException($"Method named {expression.Method.Name} is not available to perform"),
	};

	/// <exception cref="ArgumentOutOfRangeException">Throws by inner <see cref="StringBuilder"/></exception>
	/// <exception cref="ArgumentException">Throws by inner methods</exception>
	private string ParseEnumerableMethod(MethodCallExpression expression)
	{
		var stringBuilder = new StringBuilder();
		var methodName = expression.Method.Name;
		var (leftBound, rightBound) = methodName switch
		{
			nameof(EnumerableExtensions.ContainsAny) => (QueryChars.ParenthesisLeftChar, QueryChars.ParenthesisRightChar),
			nameof(Enumerable.SequenceEqual) => (QueryChars.CurlyBraceLeftChar, QueryChars.CurlyBraceRightChar),
			nameof(EnumerableExtensions.ContainsAll) => (QueryChars.ParenthesisLeftChar, QueryChars.ParenthesisRightChar),
			_ => throw new ArgumentException("Method is not available to perform"),
		};

		var caller = ParsePart(expression.Arguments[0]);
		var array = ParsePart(expression.Arguments[1]);
		stringBuilder.Append(caller);
		stringBuilder.Append(QueryChars.EqualSpaced);
		stringBuilder.Append(leftBound);
		stringBuilder.Append(array);
		stringBuilder.Append(rightBound);

		return stringBuilder.ToString();
	}

	/// <exception cref="ArgumentOutOfRangeException">Throws by inner <see cref="StringBuilder"/></exception>
	/// <exception cref="ArgumentException"></exception>
	private string ParseStringMethod(MethodCallExpression expression)
	{
		var stringBuilder = new StringBuilder();
		var methodName = expression.Method.Name;
		var (startChar, endChar) = methodName switch
		{
			nameof(string.StartsWith) => (string.Empty, QueryChars.StringMatcher),
			nameof(string.EndsWith) => (QueryChars.StringMatcher, string.Empty),
			nameof(string.Contains) => (QueryChars.StringMatcher, QueryChars.StringMatcher),
			_ => throw new NotImplementedException(),
		};

		var caller = ParsePart(expression.Object!);
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

		var stringValue = ParsePart(constantExpression);

		stringBuilder.Append(caller);
		stringBuilder.Append(comparisonChar);
		stringBuilder.Append(startChar);
		stringBuilder.Append(stringValue);
		stringBuilder.Append(endChar);

		return stringBuilder.ToString();
	}

	/// <exception cref="ArgumentOutOfRangeException">Throws by inner <see cref="StringBuilder"/></exception>
	/// <exception cref="ArgumentException">Throws by inner methods</exception>
	private string ParseIncludeOneProperty(MethodCallExpression expression)
	{
		var stringBuilder = new StringBuilder();
		var caller = ParsePart(expression.Arguments[0]);
		var including = ParsePart(expression.Arguments[1]);
		stringBuilder.Append(caller);
		stringBuilder.Append(QueryChars.AccessSeparatorChar);
		stringBuilder.Append(including);
		
		return stringBuilder.ToString();
	}

	/// <exception cref="ArgumentOutOfRangeException">Throws by inner <see cref="StringBuilder"/></exception>
	/// <exception cref="ArgumentException">Throws by inner method</exception>
	private string ParseIncludeAllProperties(MethodCallExpression expression)
	{
		var stringBuilder = new StringBuilder();
		var caller = ParsePart(expression.Arguments[0]);
		stringBuilder.Append(caller);
		stringBuilder.Append(QueryChars.AccessSeparatorChar);
		stringBuilder.Append(QueryChars.AllProperiesChar);
		
		return stringBuilder.ToString();
	}

	/// <exception cref="ArgumentException"></exception>
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

	/// <exception cref="ArgumentException"></exception>
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

	/// <exception cref="ArgumentException"></exception>
	private string ParsePart(Expression expression) => expression switch
	{
		MemberExpression member => _memberParser.Parse(member),
		MethodCallExpression methodCall => Parse(methodCall),
		ConstantExpression constant => _constantParser.Parse(constant),
		NewArrayExpression newArray => _newArrayParser.Parse(newArray),
		UnaryExpression unary => ParseQuoteUnary(unary),
		_ => throw new ArgumentException($"Expression {expression} with {expression.NodeType} cannot be part of new object")
	};

	/// <exception cref="ArgumentException"></exception>
	private string ParseQuoteUnary(UnaryExpression expression)
	{
		if (expression.NodeType != ExpressionType.Quote)
		{
			throw new ArgumentException("Unary expression may only be with node type Quote");
		}
		var lambda = expression.Operand as LambdaExpression
			?? throw new ArgumentException("Only lambda expression can be parsed");

		return ParsePart(lambda.Body);
	}
}
