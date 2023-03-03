using System.Linq.Expressions;

namespace Apicalypse.Core.Extensions;

public static class EnumerableExtensions
{

	/// <summary>
	/// Checks if any of passed values are contained in source enumerable.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="source"></param>
	/// <param name="values"></param>
	/// <returns></returns>
	public static bool ContainsAny<T>(this IEnumerable<T> source, IEnumerable<T> values)
	{
		foreach (var value in values)
		{
			if (source.Contains(value))
			{
				return true;
			}
		}
		return false;
	}
	/// <summary>
	/// Checks if all of passed values are contained in source enumerable.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="source"></param>
	/// <param name="values"></param>
	/// <returns></returns>
	public static bool ContainsAll<T>(this IEnumerable<T> source, IEnumerable<T> values)
	{
		foreach (var value in values)
		{
			if (!source.Contains(value))
			{
				return false;
			}
		}
		return true;
	}
}