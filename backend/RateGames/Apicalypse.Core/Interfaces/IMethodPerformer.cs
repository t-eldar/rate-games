using System.Linq.Expressions;

namespace Apicalypse.Core.Interfaces;

/// <summary>
/// Method performer to transform <see cref="MethodCallExpression"/> into valid part of Apicalypse query string.
/// </summary>
public interface IMethodPerformer
{
	/// <summary>
	/// Allowed string methods for using in build queries.
	/// </summary>
	public IReadOnlyCollection<string> StringMethods { get; }

	/// <summary>
	/// Allowed enumerable methods for using in build queries.
	/// </summary>
	public IReadOnlyCollection<string> EnumerableMethods { get; }

	/// <summary>
	/// All allowed methods for using in build queries.
	/// </summary>
	public IReadOnlyCollection<string> Methods { get; }

	/// <summary>
	/// Performs method from expression into string for query.
	/// </summary>
	/// <param name="methodCallExpression"></param>
	string Perform(MethodCallExpression methodCallExpression);
}