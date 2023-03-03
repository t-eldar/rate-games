namespace Apicalypse.Core.Interfaces.ExpressionParsers;
public interface INewArrayExpressionParser 
{
    string Parse(NewArrayExpression expression);
    string Parse(NewArrayExpression expression, StringBuilder stringBuilder);
}
