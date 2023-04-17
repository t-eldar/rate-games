using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

using Apicalypse.Core.Interfaces;
using Apicalypse.Core.Interfaces.ExpressionParsers;
using Apicalypse.Core.StringEnums;

namespace Apicalypse.Core.Implementations.Parsers;

/// <inheritdoc cref="IConstantExpressionParser"/>
internal class ConstantExpressionParser : IConstantExpressionParser
{
	public string Parse(ConstantExpression expression)
	{
		if (expression.Value is string stringValue)
		{
			var length = stringValue.Length + 2;
			return string.Create(length, stringValue, (span, state) =>
			{
				var index = 0;
				span[index++] = QueryChars.QuoteChar;

				for (var i = 0; i < state.Length; i++)
				{
					span[index++] = state[i];
				}
				span[index] = QueryChars.QuoteChar;
			});
		}
		else if (expression.Value is char charValue)
		{
			return string.Create(3, charValue, (span, state) =>
			{
				var index = 0;
				span[index++] = QueryChars.QuoteChar;
				span[index++] = charValue;
				span[index] = QueryChars.QuoteChar;
			});
		}
		else if (expression.Value is double doubleValue)
		{
			return doubleValue.ToString("G", CultureInfo.InvariantCulture);
		}
		else if (expression.Value is decimal decimalValue)
		{
			return decimalValue.ToString("G", CultureInfo.InvariantCulture);
		}
		else if (expression.Value is float floatValue)
		{
			return floatValue.ToString("G", CultureInfo.InvariantCulture);
		}
		return expression.Value?.ToString() ?? "null";
	}
	public string Parse(ConstantExpression expression, StringBuilder stringBuilder)
	{
		stringBuilder.Clear();
		if (expression.Value is string or char)
		{
			stringBuilder.Append(QueryChars.QuoteChar);
			stringBuilder.Append(expression.Value);
			stringBuilder.Append(QueryChars.QuoteChar);

			var result = stringBuilder.ToString();
			stringBuilder.Clear();
			return result;
		}
		else if (expression.Value is double doubleValue)
		{
			return doubleValue.ToString("G", CultureInfo.InvariantCulture);
		}
		else if (expression.Value is decimal decimalValue)
		{
			return decimalValue.ToString("G", CultureInfo.InvariantCulture);
		}
		else if (expression.Value is float floatValue)
		{
			return floatValue.ToString("G", CultureInfo.InvariantCulture);
		}

		return expression.Value?.ToString() ?? QueryKeywords.Null;
	}
}
