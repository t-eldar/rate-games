using System.Linq.Expressions;

namespace Apicalypse.Core.Interfaces.QueryBuilderStages;

/// <summary>
/// Filter builder, returns <see cref="ISortBuilder{TEntity}"/> stage.
/// </summary>
/// <typeparam name="TEntity"></typeparam>
public interface IFilterBuilder<TEntity> : ISortBuilder<TEntity>
{
	/// <summary>
	/// Filters query by passed <paramref name="predicate"/> expression.
	/// </summary>
	/// <param name="predicate"></param>
	/// <returns></returns>
	ISortBuilder<TEntity> Where(Expression<Func<TEntity, bool>> predicate);
}
