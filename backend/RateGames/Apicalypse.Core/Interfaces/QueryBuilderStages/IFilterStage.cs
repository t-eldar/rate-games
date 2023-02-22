using System.Linq.Expressions;

namespace Apicalypse.Core.Interfaces.QueryBuilderStages;

public interface IFilterStage<TEntity> : ISortStage<TEntity>
{
    ISortStage<TEntity> Where(Expression<Func<TEntity, bool>> predicate);
}
