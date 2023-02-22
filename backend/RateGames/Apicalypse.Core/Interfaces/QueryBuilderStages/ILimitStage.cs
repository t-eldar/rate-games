namespace Apicalypse.Core.Interfaces.QueryBuilderStages;

public interface ILimitStage<TEntity> : IBuildStage<TEntity>
{
    IBuildStage<TEntity> Take(int count);
}
