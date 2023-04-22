using System.Linq.Expressions;

using RateGames.Common.Contracts;

namespace RateGames.Common.Extensions;

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
	/// Checks if elements contain any value with passed ids.
	/// </summary>
	/// <typeparam name="TEntity"></typeparam>
	/// <param name="source"></param>
	/// <param name="valueIds"></param>
	/// <returns></returns>
	public static bool ContainsAny<TEntity>(this IEnumerable<TEntity> source, IEnumerable<int> valueIds)
		where TEntity : IEntity
	{
		foreach (var id in valueIds)
		{
			if (source.Any(entity => entity.Id == id))
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

	/// <summary>
	/// Checks if elements contain all values with passed ids.
	/// </summary>
	/// <typeparam name="TEntity"></typeparam>
	/// <param name="source"></param>
	/// <param name="valueIds"></param>
	/// <returns></returns>
	public static bool ContainsAll<TEntity>(this IEnumerable<TEntity> source, IEnumerable<int> valueIds)
		where TEntity : IEntity
	{
		foreach (var id in valueIds)
		{
			if (!source.Any(entity => entity.Id == id))
			{
				return false;
			}
		}

		return true;
	}
}