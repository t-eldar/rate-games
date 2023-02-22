using System.Linq.Expressions;

using Apicalypse.Core.Enums;

namespace Apicalypse.Core.Interfaces.QueryBuilderStages;

public interface ISelectionStage<TEntity> : IFilterStage<TEntity>
{
	IFilterStage<TEntity> Select(IncludeType includeType);
	IFilterStage<TEntity> Select<TProp>(
		Expression<Func<TEntity, TProp>> selector,
		SelectionMode selectionMode);
}
