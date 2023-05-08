using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apicalypse.Core.Interfaces.ExpressionParsers;
public interface IUnaryExpressionParser
{
	string Parse(UnaryExpression expression);
	string Parse(UnaryExpression expression, StringBuilder stringBuilder);
}
