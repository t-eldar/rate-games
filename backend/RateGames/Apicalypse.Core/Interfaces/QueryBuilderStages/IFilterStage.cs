using System.Linq.Expressions;

namespace Apicalypse.Core.Interfaces.QueryBuilderStages;

public interface IFilterBuilder<TEntity> : ISortBuilder<TEntity>
{
    ISortBuilder<TEntity> Where(Expression<Func<TEntity, bool>> predicate);
}
