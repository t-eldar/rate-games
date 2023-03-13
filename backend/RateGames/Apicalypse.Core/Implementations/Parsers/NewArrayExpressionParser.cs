using System.Linq.Expressions;
using System.Text;

using Apicalypse.Core.Interfaces.ExpressionParsers;
using Apicalypse.Core.StringEnums;

namespace Apicalypse.Core.Implementations.Parsers;
internal class NewArrayExpressionParser : INewArrayExpressionParser
{
    private readonly IConstantExpressionParser _constantParser;
    private readonly IMemberExpressionParser _memberParser;
    public NewArrayExpressionParser(IConstantExpressionParser constantParser, IMemberExpressionParser memberParser)
    {
        _constantParser = constantParser;
        _memberParser = memberParser;
    }

    public string Parse(NewArrayExpression expression)
    {
        if (expression.NodeType == ExpressionType.NewArrayBounds)
        {
            throw new ArgumentException("Array should be initialized in the expression");
        }
        var result = string.Empty;

        foreach (var exp in expression.Expressions)
        {
            if (exp is ConstantExpression constantExpression)
            {
                result += _constantParser.Parse(constantExpression);
                result += QueryChars.ValueSeparatorChar;
                continue;
            }
            else if (exp is MemberExpression memberExpression)
            {
                result += _memberParser.Parse(memberExpression);
                result += QueryChars.ValueSeparatorChar;
                continue;
            }
            throw new ArgumentException("Array should be initializd by not null constants");
        }

        return result[..^1];
    }

    public string Parse(NewArrayExpression expression, StringBuilder stringBuilder)
    {
        stringBuilder.Clear();
        var innerStringBuilder = new StringBuilder();
        if (expression.NodeType == ExpressionType.NewArrayBounds)
        {
            throw new ArgumentException("Array should be initialized in the expression");
        }

        foreach (var exp in expression.Expressions)
        {
            if (exp is ConstantExpression constantExpression)
            {
                stringBuilder.Append(_constantParser.Parse(constantExpression, innerStringBuilder));
                stringBuilder.Append(QueryChars.ValueSeparatorChar);
                continue;
            }
            else if (exp is MemberExpression memberExpression)
            {
                stringBuilder.Append(_memberParser.Parse(memberExpression, innerStringBuilder));
                stringBuilder.Append(QueryChars.ValueSeparatorChar);
                continue;
            }
            throw new ArgumentException("Array should be initializd by not null constants");
        }
        var result = stringBuilder.ToString();
        stringBuilder.Clear();

        return result[..^1];
    }
}
