using System.Reflection;

using Apicalypse.Core.Interfaces.ExpressionParsers;
using Apicalypse.Core.StringEnums;

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
            return ParseConstantMemberAccess(expression);
        }

        while (callerExpression is MemberExpression memberExpression)
        {
            var prop = memberExpression.Member as PropertyInfo
                ?? throw new ArgumentException("Expression should contain only property member access!");

            stringBuilder.Insert(0, prop.Name);
            stringBuilder.Insert(0, QueryChars.AccessSeparatorChar);

            callerExpression = memberExpression.Expression;
        }
        var result = stringBuilder.ToString().AsSpan();
        stringBuilder.Clear();

        return result[1..].ToString();
    }

    private string ParseConstantMemberAccess(MemberExpression expression)
    {
        var objectMember = Expression.Convert(expression, typeof(object));
        var result = Expression.Lambda<Func<object>>(objectMember).Compile().Invoke();

        return result?.ToString() ?? QueryKeywords.Null;
    }
}
