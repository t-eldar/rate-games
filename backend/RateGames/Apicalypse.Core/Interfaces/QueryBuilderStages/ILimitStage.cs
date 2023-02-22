namespace Apicalypse.Core.Interfaces.QueryBuilderStages;

public interface ILimitBuilder<TEntity> : IStringBuilder<TEntity>
{
    IStringBuilder<TEntity> Take(int count);
}
