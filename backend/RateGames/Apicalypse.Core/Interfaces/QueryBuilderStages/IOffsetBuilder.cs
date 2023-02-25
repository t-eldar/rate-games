namespace Apicalypse.Core.Interfaces.QueryBuilderStages;

/// <summary>
/// Offset builder, returns <see cref="ILimitBuilder{TEntity}"/> stage.
/// </summary>
/// <typeparam name="TEntity"></typeparam>
public interface IOffsetBuilder<TEntity> : ILimitBuilder<TEntity>
{
	/// <summary>
	/// Makes offset to query defined by <paramref name="count"/>.
	/// </summary>
	/// <param name="count"></param>
	/// <returns></returns>
	ILimitBuilder<TEntity> Skip(int count);
}
