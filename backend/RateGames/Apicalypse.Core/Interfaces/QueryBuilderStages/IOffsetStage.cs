namespace Apicalypse.Core.Interfaces.QueryBuilderStages;

public interface IOffsetStage<TEntity> : ILimitStage<TEntity>
{
    ILimitStage<TEntity> Skip(int count);
}
