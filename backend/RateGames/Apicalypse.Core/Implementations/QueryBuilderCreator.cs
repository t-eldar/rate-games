using Apicalypse.Core.Interfaces;

namespace Apicalypse.Core.Implementations;
public class QueryBuilderCreator : IQueryBuilderCreator
{
	private IQueryParser _parser;
	internal QueryBuilderCreator(IQueryParser parser) => _parser = parser;
	public IQueryBuilder<TEntity> CreateFor<TEntity>()
		=> new QueryBuilder<TEntity>(_parser);
}
