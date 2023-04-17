using System.Text;

namespace Apicalypse.Core.Interfaces.ExpressionParsers;

public interface IBinaryExpressionParser
{
	string Parse(BinaryExpression expression);
	string Parse(BinaryExpression expression, StringBuilder stringBuilder);
}
