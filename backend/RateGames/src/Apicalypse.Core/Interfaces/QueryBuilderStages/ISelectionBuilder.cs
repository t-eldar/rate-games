using System.Linq.Expressions;

using Apicalypse.Core.Enums;

namespace Apicalypse.Core.Interfaces.QueryBuilderStages;

public interface ISelectionBuilder<TEntity> : IFilterBuilder<TEntity>
{
	/// <summary>
	/// Selects all properties of model if <paramref name="includeType"/> EveryFromModel.
	/// Selects all properties of database model if <paramref name="includeType"/> EveryFromDatabase.
	/// </summary>
	/// <param name="includeType"></param>
	/// <returns></returns>
	IFilterBuilder<TEntity> Select(IncludeType includeType = IncludeType.EveryFromModel);

	/// <summary>
	/// Selects properties passed via <paramref name="selector"/> expression. 
	/// Excludes them from database model if <paramref name="selectionMode"/> is Exclude.
	/// </summary>
	/// <typeparam name="TProp"></typeparam>
	/// <param name="selector"></param>
	/// <param name="selectionMode"></param>
	/// <returns></returns>
	IFilterBuilder<TEntity> Select<TProp>(
		Expression<Func<TEntity, TProp>> selector,
		SelectionMode selectionMode = SelectionMode.Include);
}
