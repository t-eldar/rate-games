using System.Collections;
using System.Reflection;

using Apicalypse.Core.Interfaces;
using Apicalypse.Core.StringEnums;

using RateGames.Common.Contracts;
using RateGames.Common.Utils;

namespace Apicalypse.Core.Implementations.Parsers;

/// <inheritdoc cref="IExpressionParser{MemberExpression}"/>
internal class MemberExpressionParser : IExpressionParser<MemberExpression>
{
	/// <exception cref="ArgumentException">Throws is <see cref="MemberExpression"/> chain contains not member access expressions</exception>
	/// <exception cref="InvalidOperationException">Throws by inner methods</exception>
	/// <exception cref="NotSupportedException">Throws by inner methods</exception>
	/// <exception cref="ArgumentOutOfRangeException">Throws by inner <see cref="StringBuilder"/></exception>
	public string Parse(MemberExpression expression)
	{
		if (expression.Expression?.NodeType == ExpressionType.Constant)
		{
			return ParseConstantMemberAccess(expression);
		}

		var callerExpression = expression as Expression;
		var stringBuilder = new StringBuilder();
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

		return stringBuilder.ToString()[1..];
	}

	/// <summary>
	/// Parses constant member access.
	/// </summary>
	/// <param name="expression">Expression for parsing</param>
	/// <exception cref="ArgumentOutOfRangeException">Throws by inner <see cref="StringBuilder"/></exception>
	/// <returns></returns>
	private static string ParseConstantMemberAccess(MemberExpression expression)
	{
		var objectMember = Expression.Convert(expression, typeof(object));
		var constant = Expression.Lambda<Func<object>>(objectMember).Compile().Invoke();

		if (constant is not IEnumerable enumerable)
		{
			return constant?.ToString() ?? QueryKeywords.Null;
		}

		var stringBuilder = new StringBuilder();
		foreach (var item in enumerable)
		{
			stringBuilder.Append(item.ToString());
			stringBuilder.Append(QueryChars.ValueSeparatorChar);
		}

		return stringBuilder.ToString()[..^1];
	}
}
