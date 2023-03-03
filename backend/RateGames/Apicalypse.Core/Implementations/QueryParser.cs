﻿using System.Linq.Expressions;
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

	//private string ParseMethodCall(MethodCallExpression methodCallExpression)
	//{
	//	var performed = _methodPerformer.Perform(methodCallExpression);
	//	_memberStringBuilder.Append(performed);
	//	if (methodCallExpression.Object is not null)
	//	{
	//		return Parse(methodCallExpression.Object);
	//	}
	//	else if (methodCallExpression.Arguments.Count > 0)
	//	{
	//		return Parse(methodCallExpression.Arguments[0]);
	//	}

	//	throw new ArgumentException("Method should be accessed via object or be static extension");
	//}
	//private string ParseConstant(ConstantExpression constantExpression)
	//{
	//	if (constantExpression.Value is string stringValue)
	//	{
	//		var length = stringValue.Length + 2;
	//		return string.Create(length, stringValue, (span, state) =>
	//		{
	//			var index = 0;
	//			span[index++] = QueryChars.QuoteChar;

	//			for (var i = 0; i < state.Length; i++)
	//			{
	//				span[index++] = state[i];
	//			}
	//			span[index] = QueryChars.QuoteChar;
	//		});
	//	}

	//	return constantExpression.Value?.ToString() ?? "null";
	//}
	//private string ParseBinary(BinaryExpression expression)
	//{
	//	var binary = expression;
	//	var left = Parse(binary.Left);
	//	var right = Parse(binary.Right);

	//	var sign = expression.NodeType switch
	//	{
	//		ExpressionType.AndAlso => QueryChars.AndSpaced,
	//		ExpressionType.OrElse => QueryChars.OrSpaced,
	//		ExpressionType.Equal => QueryChars.EqualSpaced,
	//		ExpressionType.NotEqual => QueryChars.NotEqualSpaced,
	//		ExpressionType.GreaterThan => QueryChars.GreaterThanSpaced,
	//		ExpressionType.GreaterThanOrEqual => QueryChars.GreaterThanOrEqualSpaced,
	//		ExpressionType.LessThan => QueryChars.LessThanSpaced,
	//		ExpressionType.LessThanOrEqual => QueryChars.LessThanOrEqualSpaced,
	//		_ => throw new Exception("ExpressionType should be logical, except denial ('!')!"),
	//	};

	//	_binaryStringBuilder.Insert(0, left);
	//	_binaryStringBuilder.Append(sign);
	//	_binaryStringBuilder.Append(right);

	//	var result = _binaryStringBuilder.ToString();
	//	_binaryStringBuilder.Clear();
	//	return result;
	//}
	//private string ParseNewObject(NewExpression newExpression)
	//{
	//	var arguments = newExpression.Arguments;
	//	foreach (var argument in arguments)
	//	{
	//		switch (argument)
	//		{
	//			case MemberExpression:
	//			case MethodCallExpression:
	//			{
	//				var parsed = Parse(argument);
	//				_newObjectStringBuilder.Append(parsed);
	//				_newObjectStringBuilder.Append(QueryChars.ValueSeparatorChar);
	//				break;
	//			}
	//			default:
	//			{
	//				throw new ArgumentException("New object should contain only member access expressions.");
	//			}
	//		}
	//	}
	//	var result = _newObjectStringBuilder.ToString().AsSpan();
	//	_newObjectStringBuilder.Clear();

	//	return result[..^1].ToString();
	//}
	//private string ParseMemberAccess(MemberExpression memberExpression)
	//{
	//	var expression = memberExpression as Expression;
	//	while (expression is MemberExpression member)
	//	{
	//		var prop = member.Member as PropertyInfo
	//			?? throw new ArgumentException("Expression should contain only property member access!");

	//		_memberStringBuilder.Insert(0, prop.Name);
	//		_memberStringBuilder.Insert(0, QueryChars.AccessSeparatorChar);

	//		expression = member.Expression;
	//	}
	//	var result = _memberStringBuilder.ToString().AsSpan();
	//	_memberStringBuilder.Clear();

	//	return result[1..].ToString();
	//}
}
