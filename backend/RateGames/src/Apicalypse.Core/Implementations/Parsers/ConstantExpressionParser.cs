using System.Globalization;

using Apicalypse.Core.Interfaces;
using Apicalypse.Core.StringEnums;

namespace Apicalypse.Core.Implementations.Parsers;

/// <inheritdoc cref="IExpressionParser{ConstantExpression}"/>
internal class ConstantExpressionParser : IExpressionParser<ConstantExpression>
{
	/// <exception cref="ArgumentOutOfRangeException">Throws by inner <see cref="StringBuilder"/></exception>
	public string Parse(ConstantExpression expression)
	{
		var stringBuilder = new StringBuilder();
		if (expression.Value is string or char)
		{
			stringBuilder.Append(QueryChars.QuoteChar);
			stringBuilder.Append(expression.Value);
			stringBuilder.Append(QueryChars.QuoteChar);

			return stringBuilder.ToString();
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
		else if (expression.Value?.GetType().IsEnum ?? false)
		{
			return ((int)expression.Value).ToString();
		}

		return expression.Value?.ToString() ?? QueryKeywords.Null;
	}
}
