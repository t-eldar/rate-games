namespace Apicalypse.Core.Interfaces.QueryBuilderStages;

public interface IOffsetBuilder<TEntity> : ILimitBuilder<TEntity>
{
    ILimitBuilder<TEntity> Skip(int count);
}
