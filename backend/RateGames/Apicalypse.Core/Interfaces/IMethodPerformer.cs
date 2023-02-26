using System.Linq.Expressions;

using Apicalypse.Core.Extensions;

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
	/// Available methods are in <seealso cref="Methods"/>.
	/// </summary>
	/// <param name="methodCallExpression"></param>
	string Perform(MethodCallExpression methodCallExpression);

	/// <summary>
	/// Performs <see cref="string"/> comparison methods.
	/// Available methods are in <seealso cref="StringMethods"/>.
	/// </summary>
	/// <param name="methodCallExpression"></param>
	/// <returns></returns>
	string PerformStringComparison(MethodCallExpression methodCallExpression);

	/// <summary>
	/// Performs <see cref="IEnumerable{T}"/> comparison methods. 
	/// Available methods are in <seealso cref="EnumerableMethods"/>.
	/// </summary>
	/// <param name="methodCallExpression"></param>
	/// <returns></returns>
	string PerformArrayComparison(MethodCallExpression methodCallExpression);

	/// <summary>
	/// Performs <see cref="ObjectExtensions.IncludeProperties{T}"/>.
	/// </summary>
	/// <returns></returns>
	string PerformIncludeProperties();
}