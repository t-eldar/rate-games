using System.Linq.Expressions;

using Apicalypse.Core.Enums;

namespace Apicalypse.Core.Interfaces.QueryBuilderStages;

public interface ISelectionBuilder<TEntity> : IFilterBuilder<TEntity>
{
	IFilterBuilder<TEntity> Select(IncludeType includeType);
	IFilterBuilder<TEntity> Select<TProp>(
		Expression<Func<TEntity, TProp>> selector,
		SelectionMode selectionMode = SelectionMode.Include);
}
