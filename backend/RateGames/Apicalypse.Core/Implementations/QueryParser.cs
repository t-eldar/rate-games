using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using Apicalypse.Core.Interfaces;
using Apicalypse.Core.StringEnums;

namespace Apicalypse.Core.Implementations;
internal class QueryParser : IQueryParser
{
    public QueryParser(IMethodPerformer methodPerformer)
    {
        _newObjectStringBuilder = new StringBuilder();
        _memberStringBuilder = new StringBuilder();
        _binaryStringBuilder = new StringBuilder();
        _methodPerformer = methodPerformer;
    }

    private readonly StringBuilder _newObjectStringBuilder;
    private readonly StringBuilder _memberStringBuilder;
    private readonly StringBuilder _binaryStringBuilder;
    private readonly IMethodPerformer _methodPerformer;

    /// <summary>
    /// Parses expression into part of query string.
    /// </summary>
    /// <typeparam name="TProp"></typeparam>
    /// <param name="expression"></param>
    /// <returns></returns>
    public string Parse<TEntity, TProp>(Expression<Func<TEntity, TProp>> expression)
        => FirstParse(expression.Body);
    private string FirstParse(Expression expression) => expression switch
    {
        ConstantExpression => throw new ArgumentException("First expression should not contain constants"),
        _ => Parse(expression),
    };
    private string Parse(Expression expression) => expression switch
    {
        MemberExpression memberExpression => ParseMemberAccess(memberExpression),
        MethodCallExpression methodCallExpression => ParseMethodCall(methodCallExpression),
        NewExpression newExpression => ParseNewObject(newExpression),
        BinaryExpression binaryExpression => ParseBinary(binaryExpression),
        ConstantExpression constantExpression => constantExpression.Value?.ToString()
            ?? throw new ArgumentException("Constant should not be null"),
        _ => throw new NotImplementedException(),
    };
    private string ParseMethodCall(MethodCallExpression methodCallExpression)
    {
        var performed = _methodPerformer.Perform(methodCallExpression);
        _memberStringBuilder.Append(performed);
        if (methodCallExpression.Object is not null)
        {
            return Parse(methodCallExpression.Object);
        }
        else if (methodCallExpression.Arguments.Count > 0)
        {
            return Parse(methodCallExpression.Arguments[0]);
        }
        else
        {
            throw new ArgumentException("Method should be accessed via object or be static extension");
        }
    }
    private string ParseBinary(BinaryExpression expression)
    {
        var binary = expression;
        var left = Parse(binary.Left);
        var right = Parse(binary.Right);

        var sign = expression.NodeType switch
        {
            ExpressionType.AndAlso => QueryChars.AndSpaced,
            ExpressionType.OrElse => QueryChars.OrSpaced,
            ExpressionType.Equal => QueryChars.EqualSpaced,
            ExpressionType.NotEqual => QueryChars.NotEqualSpaced,
            ExpressionType.GreaterThan => QueryChars.GreaterThanSpaced,
            ExpressionType.GreaterThanOrEqual => QueryChars.GreaterThanOrEqualSpaced,
            ExpressionType.LessThan => QueryChars.LessThanSpaced,
            ExpressionType.LessThanOrEqual => QueryChars.LessThanOrEqualSpaced,
            _ => throw new Exception("ExpressionType should be logical, except denial ('!')!"),
        };

        _binaryStringBuilder.Insert(0, left);
        _binaryStringBuilder.Append(sign);
        _binaryStringBuilder.Append(right);

        var result = _binaryStringBuilder.ToString();
        _binaryStringBuilder.Clear();
        return result;
    }
    private string ParseNewObject(NewExpression newExpression)
    {
        var arguments = newExpression.Arguments;
        foreach (var argument in arguments)
        {
            switch (argument)
            {
                case MemberExpression:
                case MethodCallExpression:
                    {
                        var parsed = Parse(argument);
                        _newObjectStringBuilder.Append(parsed);
                        _newObjectStringBuilder.Append(QueryChars.ValueSeparatorChar);
                        break;
                    }
                default:
                    {
                        throw new ArgumentException("New object should contain only member access expressions.");
                    }
            }
        }
        var result = _newObjectStringBuilder.ToString().AsSpan();
        _newObjectStringBuilder.Clear();

        return result[..^1].ToString();
    }
    private string ParseMemberAccess(MemberExpression memberExpression)
    {
        var expression = memberExpression as Expression;
        while (expression is MemberExpression member)
        {
            var prop = member.Member as PropertyInfo
                ?? throw new ArgumentException("Expression should contain only property member access!");

            _memberStringBuilder.Insert(0, prop.Name);
            _memberStringBuilder.Insert(0, QueryChars.AccessSeparatorChar);

            expression = member.Expression;
        }
        var result = _memberStringBuilder.ToString().AsSpan();
        _memberStringBuilder.Clear();
        return result[1..].ToString();
    }
}
