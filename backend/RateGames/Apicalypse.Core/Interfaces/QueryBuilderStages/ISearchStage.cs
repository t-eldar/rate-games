namespace Apicalypse.Core.Interfaces.QueryBuilderStages;

public interface ISearchStage<TEntity> : IOffsetStage<TEntity>
{
    IOffsetStage<TEntity> Find(string search);
}
