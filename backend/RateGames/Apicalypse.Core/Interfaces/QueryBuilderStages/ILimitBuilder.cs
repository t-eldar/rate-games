namespace Apicalypse.Core.Interfaces.QueryBuilderStages;

/// <summary>
/// Limit builder, returns <see cref="IResultBuilder{TEntity}"/> stage.
/// </summary>
/// <typeparam name="TEntity"></typeparam>
public interface ILimitBuilder<TEntity> : IResultBuilder<TEntity>
{
	/// <summary>
	/// Limits query by <paramref name="count"/>.
	/// </summary>
	/// <param name="count"></param>
	/// <returns></returns>
	IResultBuilder<TEntity> Take(int count);
}
