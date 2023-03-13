using System.Linq.Expressions;
using System.Reflection;
using System.Text;

using Apicalypse.Core.Implementations.Parsers;
using Apicalypse.Core.Interfaces;
using Apicalypse.Core.Interfaces.ExpressionParsers;
using Apicalypse.Core.StringEnums;

namespace Apicalypse.Core.Implementations;

/// <inheritdoc cref="IQueryParser"/>
internal class QueryParser : IQueryParser
{
	private readonly IMemberExpressionParser _memberParser;
	private readonly INewExpressionParser _newObjectParser;
	private readonly IBinaryExpressionParser _binaryParser;
	private readonly IConstantExpressionParser _constantParser;
	private readonly IMethodCallExpressionParser _methodParser;

	private readonly StringBuilder _stringBuilder;
    public QueryParser(
		IMemberExpressionParser memberParser, 
		INewExpressionParser newObjectParser, 
		IBinaryExpressionParser binaryParser, 
		IConstantExpressionParser constantParser, 
		IMethodCallExpressionParser methodParser)
    {
        _memberParser = memberParser;
        _newObjectParser = newObjectParser;
        _binaryParser = binaryParser;
        _constantParser = constantParser;
        _methodParser = methodParser;
		_stringBuilder = new();
    }

    //private readonly StringBuilder _newObjectStringBuilder;
    //private readonly StringBuilder _memberStringBuilder;
    //private readonly StringBuilder _binaryStringBuilder;

    public string Parse<TEntity, TProp>(Expression<Func<TEntity, TProp>> expression)
		=> FilterParse(expression.Body);
	private string FilterParse(Expression expression) => expression switch
	{
		ConstantExpression => throw new ArgumentException("First expression should not contain constants"),
		_ => Parse(expression),
	};
	private string Parse(Expression expression) => expression switch
	{
		MemberExpression memberExpression => _memberParser.Parse(memberExpression, _stringBuilder),
		MethodCallExpression methodCallExpression => _methodParser.Parse(methodCallExpression, _stringBuilder),
		NewExpression newExpression => _newObjectParser.Parse(newExpression, _stringBuilder),
		BinaryExpression binaryExpression => _binaryParser.Parse(binaryExpression, _stringBuilder),
		ConstantExpression constantExpression => _constantParser.Parse(constantExpression, _stringBuilder),
		_ => throw new NotImplementedException(),
	};
}
