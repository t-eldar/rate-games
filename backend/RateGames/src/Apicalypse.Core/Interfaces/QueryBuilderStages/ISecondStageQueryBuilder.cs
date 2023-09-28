using Apicalypse.Core.Interfaces.QueryBuilderSteps;

namespace Apicalypse.Core.Interfaces.QueryBuilderStages;

/// <summary>
/// Second stage wrapper for query builder for <typeparamref name="TEntity"/>.
/// </summary>
public interface ISecondStageQueryBuilder<TEntity> : IIncludingBuilder<TEntity> { }
