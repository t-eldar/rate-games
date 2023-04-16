namespace Apicalypse.Core.Interfaces.ExpressionParsers;

public interface IConstantExpressionParser
{
    string Parse(ConstantExpression expression);
    string Parse(ConstantExpression expression, StringBuilder stringBuilder);
}