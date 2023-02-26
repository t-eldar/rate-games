using System.Linq.Expressions;

using Apicalypse.Core.Extensions;
using Apicalypse.Core.Interfaces;
using Apicalypse.Core.StringEnums;

namespace Apicalypse.Core.Implementations;

/// <inheritdoc cref="IQueryBuilder{TEntity}"/>
internal class MethodPerformer : IMethodPerformer
{
	public IReadOnlyCollection<string> StringMethods { get; private set; }
	public IReadOnlyCollection<string> EnumerableMethods { get; private set; }
	public IReadOnlyCollection<string> Methods { get; private set; }

	public MethodPerformer()
	{
		StringMethods = _stringMethods.AsReadOnly();
		EnumerableMethods = _enumerableMethods.AsReadOnly();
		Methods = _enumerableMethods
			.Concat(_stringMethods)
			.Append(nameof(ObjectExtensions.IncludeProperties))
			.ToArray()
			.AsReadOnly();
	}

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
	public string Perform(MethodCallExpression methodCallExpression)
	{
		var methodName = methodCallExpression.Method.Name;
		if (StringMethods.Contains(methodName))
		{
			return PerformStringComparison(methodCallExpression);
		}
		else if (methodName == nameof(ObjectExtensions.IncludeProperties))
		{
			return PerformIncludeProperties();
		}
		else if (EnumerableMethods.Contains(methodName))
		{
			return PerformArrayComparison(methodCallExpression);
		}
		throw new ArgumentException($"Method named {methodName} is not available to perform");
	}
	public string PerformIncludeProperties()
		=> QueryChars.AccessSeparator + QueryChars.AllProperies;
	public string PerformStringComparison(MethodCallExpression methodCallExpression)
	{
		var methodName = methodCallExpression.Method.Name;

		var comparisonChar = string.Empty;
		var (startChar, endChar) = methodName switch
		{
			nameof(string.StartsWith) => (string.Empty, QueryChars.StringMatcher),
			nameof(string.EndsWith) => (QueryChars.StringMatcher, string.Empty),
			nameof(string.Contains) => (QueryChars.StringMatcher, QueryChars.StringMatcher),
			_ => throw new ArgumentException($"Method named {methodName} is not available to perform"),
		};
		var firstArgExpression = methodCallExpression.Arguments[0];
		if (firstArgExpression is not ConstantExpression constantExpression)
		{
			throw new ArgumentException("Method should recieve only constants");
		}

		if (methodCallExpression.Arguments.Count == 1)
		{
			comparisonChar = QueryChars.EqualSpaced;
		}
		else if (methodCallExpression.Arguments.Count == 2)
		{
			var stringComparisonBoxed = (methodCallExpression.Arguments[1] as ConstantExpression)?.Value
				?? throw new ArgumentException("Method should contain const arguments");
			var stringComparison = (StringComparison)stringComparisonBoxed;
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
		}
		else if (methodCallExpression.Arguments.Count == 3)
		{
			var ignoreCaseBoxed = (methodCallExpression.Arguments[1] as ConstantExpression)?.Value
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
		}

		var stringValue = string.Empty;
		if (constantExpression.Value is char character)
		{
			stringValue = character.ToString();
		}
		else
		{
			stringValue = constantExpression.Value as string;
		}
		return CreateStringComparisonString(comparisonChar, startChar, endChar, stringValue!);
	}

	public string PerformArrayComparison(MethodCallExpression methodCallExpression)
	{
		var methodName = methodCallExpression.Method.Name;
		var (leftBrace, rightBrace) = methodName switch
		{
			nameof(EnumerableExtensions.ContainsAny) => (QueryChars.ParenthesisLeft, QueryChars.ParenthesisRight),
			nameof(Enumerable.SequenceEqual) => (QueryChars.CurlyBraceLeft, QueryChars.CurlyBraceRight),
			nameof(EnumerableExtensions.ContainsAll) => (QueryChars.SquareBracketLeft, QueryChars.SquareBracketRight),
			_ => throw new ArgumentException("Method is not available to perform"),
		};
		if (methodCallExpression.Arguments[1] is not NewArrayExpression newArrayExpression)
		{
			throw new ArgumentException("Only new arrays are able to parse.");
		}
		if (newArrayExpression.NodeType == ExpressionType.NewArrayInit)
		{
			var values = new List<string>();
			foreach (var expression in newArrayExpression.Expressions)
			{
				if (expression is not ConstantExpression constantExpression)
				{
					throw new ArgumentException("Array should be initializd by not null constants");
				}
				values.Add(constantExpression.Value?.ToString()
					?? throw new ArgumentException("Array should be initializd by not null constants"));
			}
			return CreateArrayComparisonString(leftBrace, rightBrace, values);
		}
		throw new ArgumentException("Array should be initialized in the expression");
	}

	private string CreateArrayComparisonString(string leftBrace, string rightBrace, IList<string> values)
	{
		var length = QueryChars.EqualSpaced.Length + leftBrace.Length + rightBrace.Length + values.Count - 1;
		foreach (var value in values)
		{
			length += value.Length;
		}
		return string.Create(length, values, (span, state) =>
		{
			var index = 0;
			for (var i = 0; i < QueryChars.EqualSpaced.Length; i++, index++)
			{
				span[index] = QueryChars.EqualSpaced[i];
			}
			for (var i = 0; i < leftBrace.Length; i++, index++)
			{
				span[index] = leftBrace[i];
			}
			foreach (var str in state)
			{
				for (var i = 0; i < str.Length; i++, index++)
				{
					span[index] = str[i];
				}
				span[index++] = QueryChars.ValueSeparatorChar;
			}
			index--;
			for (var i = 0; i < rightBrace.Length; i++, index++)
			{
				span[index] = rightBrace[i];
			}
		});
	}
	private string CreateStringComparisonString(string comparisonChar, string startChar, string endChar, string value)
	{
		var length = comparisonChar.Length + startChar.Length + endChar.Length
			+ 2 * QueryChars.Quote.Length + value.Length;
		return string.Create(length, value, (span, state) =>
		{
			var index = 0;
			for (var i = 0; i < comparisonChar.Length; i++, index++)
			{
				span[index] = comparisonChar[i];
			}
			for (var i = 0; i < startChar.Length; i++, index++)
			{
				span[index] = startChar[i];
			}
			for (var i = 0; i < QueryChars.Quote.Length; i++, index++)
			{
				span[index] = QueryChars.Quote[i];
			}
			for (var i = 0; i < value.Length; i++, index++)
			{
				span[index] = value[i];
			}
			for (var i = 0; i < QueryChars.Quote.Length; i++, index++)
			{
				span[index] = QueryChars.Quote[i];
			}
			for (var i = 0; i < endChar.Length; i++, index++)
			{
				span[index] = endChar[i];
			}
		});
	}
}