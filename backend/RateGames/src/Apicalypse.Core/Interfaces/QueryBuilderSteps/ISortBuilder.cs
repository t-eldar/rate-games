namespace Apicalypse.Core.Interfaces.QueryBuilderSteps;

/// <summary>
/// Sort builder, returns <see cref="ISearchBuilder{TEntity}"/> stage.
/// </summary>
/// <typeparam name="TEntity"></typeparam>
public interface ISortBuilder<TEntity> : ISearchBuilder<TEntity>
{
	/// <summary>
	/// Orders query by ascending defined by <paramref name="selector"/>.
	/// </summary>
	/// <typeparam name="TProp"></typeparam>
	/// <param name="selector"></param>
	/// <returns></returns>
	ISearchBuilder<TEntity> OrderBy<TProp>(Expression<Func<TEntity, TProp>> selector);

	/// <summary>
	/// Orders query by descending defined by <paramref name="selector"/>.
	/// </summary>
	/// <typeparam name="TProp"></typeparam>
	/// <param name="selector"></param>
	/// <returns></returns>
	ISearchBuilder<TEntity> OrderByDescending<TProp>(Expression<Func<TEntity, TProp>> selector);
}
