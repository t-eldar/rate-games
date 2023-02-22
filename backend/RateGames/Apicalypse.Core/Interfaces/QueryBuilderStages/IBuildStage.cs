namespace Apicalypse.Core.Interfaces.QueryBuilderStages;

public interface IBuildStage<TEntity>
{
    string? Build();
}
