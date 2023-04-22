using System.Linq.Expressions;

using RateGames.Common.Contracts;

namespace RateGames.Common.Extensions;

public static class EntityMarkerExtensions
{
	/// <summary>
	/// Use for including all properties of database model in query.
	/// </summary>
	/// <typeparam name="T">Entity type</typeparam>
	/// <param name="entity"></param>
	/// <returns>Unchanged entity</returns>
	public static T IncludeAllProperties<T>(this T entity)
		where T : IEntity => entity;

	/// <summary>
	/// Use for including selected properties in query.
	/// </summary>
	/// <typeparam name="T">Entity type</typeparam>
	/// <param name="entity"></param>
	/// <returns>Unchanged entity</returns>
	public static IEnumerable<TEntity> IncludeProperty<TEntity, TProp>(
		this IEnumerable<TEntity> source,
		Expression<Func<TEntity, TProp>> selector
	)
		where TEntity : IEntity => source;

	/// <summary>
	/// Use for making filter queries. Instead of writing Id == values.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="id"></param>
	/// <param name="values"></param>
	/// <returns></returns>
	public static bool ContainsAny<T>(this int id, IEnumerable<int> values)
		where T : IEntity => values.Contains(id);
}
