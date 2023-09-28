namespace Apicalypse.Core.Interfaces;
/// <summary>
/// Parser for converting expressions to an Apicalypse query string
/// </summary>
/// <typeparam name="TExpression">Type of expression</typeparam>
public interface IExpressionParser<TExpression> where TExpression : Expression
{
	/// <summary>
	/// Parses expression to string part of Apicalypse query
	/// </summary>
	/// <param name="expression">Expression for parsing</param>
	/// <returns>Part of Apicalypse query</returns>
	string Parse(TExpression expression);
}
