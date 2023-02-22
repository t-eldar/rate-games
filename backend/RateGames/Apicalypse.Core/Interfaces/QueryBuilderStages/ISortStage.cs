using System.Linq.Expressions;

namespace Apicalypse.Core.Interfaces.QueryBuilderStages;

public interface ISortBuilder<TEntity> : ISearchBuilder<TEntity>
{
    ISearchBuilder<TEntity> OrderBy<TProp>(Expression<Func<TEntity, TProp>> selector);

    ISearchBuilder<TEntity> OrderByDescending<TProp>(Expression<Func<TEntity, TProp>> selector);
}
