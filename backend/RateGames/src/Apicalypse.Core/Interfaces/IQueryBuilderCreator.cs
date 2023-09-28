using Apicalypse.Core.Interfaces.QueryBuilderStages;

namespace Apicalypse.Core.Interfaces;

/// <summary>
/// Creator of <see cref="IFirstStageQueryBuilder{TEntity}"/> for different entities.
/// </summary>
public interface IQueryBuilderCreator
{
	/// <summary>
	/// Creates <see cref="IFirstStageQueryBuilder{TEntity}"/> for model <typeparamref name="TEntity"/>.
	/// </summary>
	/// <typeparam name="TEntity"></typeparam>
	/// <returns></returns>
	IFirstStageQueryBuilder<TEntity> CreateFor<TEntity>();
}
