using System.Linq.Expressions;

namespace Apicalypse.Core.Interfaces.QueryBuilderStages;

public interface ISortStage<TEntity> : ISearchStage<TEntity>
{
    ISearchStage<TEntity> OrderBy<TProp>(Expression<Func<TEntity, TProp>> selector);

    ISearchStage<TEntity> OrderByDescending<TProp>(Expression<Func<TEntity, TProp>> selector);
}
