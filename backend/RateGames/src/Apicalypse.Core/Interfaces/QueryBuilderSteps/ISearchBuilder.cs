namespace Apicalypse.Core.Interfaces.QueryBuilderSteps;

/// <summary>
/// Search builder, returns <see cref="IOffsetBuilder{TEntity}"/> stage.
/// </summary>
/// <typeparam name="TEntity"></typeparam>
public interface ISearchBuilder<TEntity> : IOffsetBuilder<TEntity>
{
	/// <summary>
	/// Adds search string to query.
	/// </summary>
	/// <param name="search"></param>
	/// <returns></returns>
	IOffsetBuilder<TEntity> Find(string search);
}
