using System.Linq.Expressions;

namespace RateGames.Common.Extensions;

public static class IncludeMarkerExtensions
{
    /// <summary>
    /// Use for including all properties of database model in query.
    /// </summary>
    /// <typeparam name="T">Entity type</typeparam>
    /// <param name="entity"></param>
    /// <returns>Unchanged entity</returns>
    public static T IncludeAllProperties<T>(this T entity) => entity;

    /// <summary>
    /// Use for including selected properties in query.
    /// </summary>
    /// <typeparam name="T">Entity type</typeparam>
    /// <param name="entity"></param>
    /// <returns>Unchanged entity</returns>
    public static IEnumerable<TEntity> IncludeProperty<TEntity, TProp>(
        this IEnumerable<TEntity> source, Expression<Func<TEntity, TProp>> selector) => source;
}