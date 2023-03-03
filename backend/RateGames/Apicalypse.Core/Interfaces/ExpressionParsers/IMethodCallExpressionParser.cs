namespace Apicalypse.Core.Interfaces.ExpressionParsers;

public interface IMethodCallExpressionParser
{
    string Parse(MethodCallExpression expression);  
    string Parse(MethodCallExpression expression, StringBuilder stringBuilder);
}

