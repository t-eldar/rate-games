namespace Apicalypse.Core.Interfaces;
public interface IQueryBuilderCreator
{
	IQueryBuilder<TEntity> CreateFor<TEntity>();
}
