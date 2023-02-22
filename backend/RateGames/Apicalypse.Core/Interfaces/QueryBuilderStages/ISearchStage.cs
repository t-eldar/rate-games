namespace Apicalypse.Core.Interfaces.QueryBuilderStages;

public interface ISearchBuilder<TEntity> : IOffsetBuilder<TEntity>
{
    IOffsetBuilder<TEntity> Find(string search);
}
