using System.Text;

using Apicalypse.Core.Interfaces;
using Apicalypse.Core.Interfaces.ExpressionParsers;
using Apicalypse.Core.StringEnums;

namespace Apicalypse.Core.Implementations.Parsers;

/// <inheritdoc cref="INewExpressionParser"/>
internal class NewExpressionParser : INewExpressionParser
{
    private readonly IMemberExpressionParser _memberParser;
    private readonly IMethodCallExpressionParser _methodCallParser;
    private readonly StringBuilder _stringBuilder;

    public NewExpressionParser(
        IMemberExpressionParser memberParser, 
        IMethodCallExpressionParser methodCallParser)
    {
        _memberParser = memberParser;
        _methodCallParser = methodCallParser;
        _stringBuilder = new();
    }

    public string Parse(NewExpression newExpression)
    {
        var arguments = newExpression.Arguments;
        var result = string.Empty;
        foreach (var argument in arguments)
        {
            switch (argument)
            {
                case MemberExpression:
                case MethodCallExpression:
                {
                    var parsed = ParsePart(argument);
                    result += parsed;
                    result += QueryChars.ValueSeparatorChar;
                    break;
                }
                default:
                {
                    throw new ArgumentException("New object should contain only member access expressions.");
                }
            }
        }

        return result[..^1];
    }
    public string Parse(NewExpression expression, StringBuilder stringBuilder)
    {
        stringBuilder.Clear();
        var arguments = expression.Arguments;
        foreach (var argument in arguments)
        {
            switch (argument)
            {
                case MemberExpression:
                case MethodCallExpression:
                {
                    var parsed = ParsePart(argument);
                    stringBuilder.Append(parsed);
                    stringBuilder.Append(QueryChars.ValueSeparatorChar);
                    break;
                }
                default:
                {
                    throw new ArgumentException("New object should contain only member access expressions.");
                }
            }
        }
        var result = stringBuilder.ToString().AsSpan();
        stringBuilder.Clear();

        return result[..^1].ToString();
    }

    private string ParsePart(Expression expression) => expression switch
    {
        MemberExpression member => _memberParser.Parse(member, _stringBuilder),
        MethodCallExpression methodCall => _methodCallParser.Parse(methodCall, _stringBuilder),
        _ => throw new ArgumentException($"Expression {expression} with {expression.NodeType} cannot be part of new object")
    };
}
