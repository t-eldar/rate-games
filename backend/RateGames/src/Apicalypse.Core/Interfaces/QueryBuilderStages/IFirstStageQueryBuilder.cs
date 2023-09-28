using Apicalypse.Core.Interfaces.QueryBuilderSteps;

namespace Apicalypse.Core.Interfaces.QueryBuilderStages;

/// <summary>
/// First stage wrapper for query builder for <typeparamref name="TEntity"/>.
/// </summary>
public interface IFirstStageQueryBuilder<TEntity> : ISelectionBuilder<TEntity> { }
