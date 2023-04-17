using System.Collections;
using System.Reflection;

using Apicalypse.Core.Interfaces.ExpressionParsers;
using Apicalypse.Core.StringEnums;

using RateGames.Common.Contracts;
using RateGames.Common.Utils;

namespace Apicalypse.Core.Implementations.Parsers;

/// <inheritdoc cref="IMemberExpressionParser"/>
internal class MemberExpressionParser : IMemberExpressionParser
{
	public string Parse(MemberExpression expression)
	{
		var callerExpression = expression as Expression;
		var result = string.Empty;
		while (callerExpression is MemberExpression memberExpression)
		{
			var prop = memberExpression.Member as PropertyInfo
				?? throw new ArgumentException("Expression should contain only property member access!");
			if (
				prop.DeclaringType!.IsGenericType
				&& prop.DeclaringType.GetGenericTypeDefinition() == typeof(IdOr<>)
				&& prop.Name != nameof(IEntity.Id)
			)
			{
				callerExpression = memberExpression.Expression;
				continue;
			}
			result = result.Insert(0, prop.Name);
			result = result.Insert(0, QueryChars.AccessSeparator);
			callerExpression = memberExpression.Expression;
		}

		return result[1..];
	}
	public string Parse(MemberExpression expression, StringBuilder stringBuilder)
	{
		stringBuilder.Clear();
		var callerExpression = expression as Expression;

		if (expression.Expression?.NodeType == ExpressionType.Constant)
		{
			return ParseConstantMemberAccess(expression, stringBuilder);
		}

		while (callerExpression is MemberExpression memberExpression)
		{
			var prop = memberExpression.Member as PropertyInfo
				?? throw new ArgumentException("Expression should contain only property member access!");
			if (prop.DeclaringType!.IsGenericType
				&& prop.DeclaringType.GetGenericTypeDefinition() == typeof(IdOr<>)
				&& prop.Name != nameof(IEntity.Id))
			{
				callerExpression = memberExpression.Expression;
				continue;
			}
			stringBuilder.Insert(0, prop.Name);
			stringBuilder.Insert(0, QueryChars.AccessSeparatorChar);

			callerExpression = memberExpression.Expression;
		}
		var result = stringBuilder.ToString().AsSpan();
		stringBuilder.Clear();

		return result[1..].ToString();
	}

	private string ParseConstantMemberAccess(MemberExpression expression, StringBuilder stringBuilder)
	{
		var objectMember = Expression.Convert(expression, typeof(object));
		var constant = Expression.Lambda<Func<object>>(objectMember).Compile().Invoke();

		if (constant is not IEnumerable enumerable)
		{
			return constant?.ToString() ?? QueryKeywords.Null;
		}

		foreach (var item in enumerable)
		{
			stringBuilder.Append(item.ToString());
			stringBuilder.Append(QueryChars.ValueSeparatorChar);
		}
		var result = stringBuilder.ToString();
		stringBuilder.Clear();

		return result[..^1];
	}
}
