using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Apicalypse.Core.Interfaces.ExpressionParsers;

namespace Apicalypse.Core.Implementations.Parsers;
public class UnaryExpressionParser : IUnaryExpressionParser
{
	private readonly IConstantExpressionParser _constantExpressionParser;
	private readonly IMemberExpressionParser _memberExpressionParser;

	public UnaryExpressionParser(
		IConstantExpressionParser constantExpressionParser, 
		IMemberExpressionParser memberExpressionParser
	)
	{
		_constantExpressionParser = constantExpressionParser;
		_memberExpressionParser = memberExpressionParser;
	}

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
			_ => throw new NotImplementedException(),
		};
	}
	public string Parse(UnaryExpression expression, StringBuilder stringBuilder)
	{
		if (expression.NodeType != ExpressionType.Convert)
		{
			throw new NotImplementedException();
		}

		return expression.Operand switch
		{
			ConstantExpression constant => _constantExpressionParser.Parse(constant, stringBuilder),
			MemberExpression member => _memberExpressionParser.Parse(member, stringBuilder),
			_ => throw new NotImplementedException(),
		};
	}
}
