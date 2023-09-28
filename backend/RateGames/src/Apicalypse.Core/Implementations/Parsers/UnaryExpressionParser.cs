using Apicalypse.Core.Interfaces;

namespace Apicalypse.Core.Implementations.Parsers;
public class UnaryExpressionParser : IExpressionParser<UnaryExpression>
{
	private readonly IExpressionParser<ConstantExpression> _constantExpressionParser;
	private readonly IExpressionParser<MemberExpression> _memberExpressionParser;

	public UnaryExpressionParser(
		IExpressionParser<ConstantExpression> constantExpressionParser,
		IExpressionParser<MemberExpression> memberExpressionParser
	)
	{
		_constantExpressionParser = constantExpressionParser;
		_memberExpressionParser = memberExpressionParser;
	}

	/// <exception cref="NotImplementedException"></exception>
	public string Parse(UnaryExpression expression)
	{
		if (expression.NodeType != ExpressionType.Convert)
		{
			throw new NotImplementedException();
		}

		return expression.Operand switch
		{
			ConstantExpression constant => _constantExpressionParser.Parse(constant),
			MemberExpression member => _memberExpressionParser.Parse(member),
			UnaryExpression unary => Parse(unary),
			_ => throw new NotImplementedException(),
		};
	}
}
