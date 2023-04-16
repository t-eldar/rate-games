namespace Apicalypse.Core.Interfaces.ExpressionParsers;

public interface IMemberExpressionParser 
{
    string Parse(MemberExpression expression);
    string Parse(MemberExpression expression, StringBuilder stringBuilder);
}
