namespace Apicalypse.Core.Interfaces.ExpressionParsers;

public interface INewExpressionParser
{
    string Parse(NewExpression expression);
    string Parse(NewExpression expression, StringBuilder stringBuilder);
}
