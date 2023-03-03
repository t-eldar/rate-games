using System.Linq.Expressions;
using System.Text;

using Apicalypse.Core.Interfaces.ExpressionParsers;

namespace Apicalypse.Core.Implementations.Parsers;
internal class NewArrayExpressionParser : INewArrayExpressionParser
{
    private readonly IConstantExpressionParser _constantParser;
    public NewArrayExpressionParser(IConstantExpressionParser constantParser) => _constantParser = constantParser;

    public string Parse(NewArrayExpression expression)
    {
        if (expression.NodeType == ExpressionType.NewArrayBounds)
        {
            throw new ArgumentException("Array should be initialized in the expression");
        }
        var result = string.Empty;

        foreach (var exp in expression.Expressions)
        {
            if (exp is not ConstantExpression constantExpression)
            {
                throw new ArgumentException("Array should be initializd by not null constants");
            }
            result += _constantParser.Parse(constantExpression) + ",";
        }

        return result[..^1];
    }

    public string Parse(NewArrayExpression expression, StringBuilder stringBuilder)
    {
        if (expression.NodeType == ExpressionType.NewArrayBounds)
        {
            throw new ArgumentException("Array should be initialized in the expression");
        }

        foreach (var exp in expression.Expressions)
        {
            if (exp is not ConstantExpression constantExpression)
            {
                throw new ArgumentException("Array should be initializd by not null constants");
            }
            stringBuilder.Append(_constantParser.Parse(constantExpression));
            stringBuilder.Append(',');
        }
        var result = stringBuilder.ToString();
        stringBuilder.Clear();

        return result[..^1];
    }
}
