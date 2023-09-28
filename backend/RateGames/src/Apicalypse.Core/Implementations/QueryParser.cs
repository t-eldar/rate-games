using Apicalypse.Core.Interfaces;

namespace Apicalypse.Core.Implementations;

/// <inheritdoc cref="IQueryParser"/>
internal class QueryParser : IQueryParser
{
	private readonly IExpressionParser<MemberExpression> _memberParser;
	private readonly IExpressionParser<NewExpression> _newObjectParser;
	private readonly IExpressionParser<BinaryExpression> _binaryParser;
	private readonly IExpressionParser<ConstantExpression> _constantParser;
	private readonly IExpressionParser<MethodCallExpression> _methodParser;


	public QueryParser(
		IExpressionParser<MemberExpression> memberParser,
		IExpressionParser<NewExpression> newObjectParser,
		IExpressionParser<BinaryExpression> binaryParser,
		IExpressionParser<ConstantExpression> constantParser,
		IExpressionParser<MethodCallExpression> methodParser
	)
	{
		_memberParser = memberParser;
		_newObjectParser = newObjectParser;
		_binaryParser = binaryParser;
		_constantParser = constantParser;
		_methodParser = methodParser;
	}

	public string Parse<TEntity, TProp>(Expression<Func<TEntity, TProp>> expression) => FilterParse(expression.Body);

	private string FilterParse(Expression expression) =>
	expression switch
	{
		ConstantExpression => throw new ArgumentException("First expression should not contain constants"),
		_ => Parse(expression),
	};

	private string Parse(Expression expression) =>
	expression switch
	{
		MemberExpression memberExpression => _memberParser.Parse(memberExpression),
		MethodCallExpression methodCallExpression => _methodParser.Parse(methodCallExpression),
		NewExpression newExpression => _newObjectParser.Parse(newExpression),
		BinaryExpression binaryExpression => _binaryParser.Parse(binaryExpression),
		ConstantExpression constantExpression => _constantParser.Parse(constantExpression),
		_ => throw new NotImplementedException(),
	};
}
