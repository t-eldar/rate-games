using Apicalypse.Core.Interfaces.QueryBuilderStages;

namespace Apicalypse.Core.Interfaces;

/// <summary>
/// Query builder for <typeparamref name="TEntity"/>.
/// </summary>
/// <typeparam name="TEntity"></typeparam>
public interface IQueryBuilder<TEntity> : ISelectionStage<TEntity> { }
