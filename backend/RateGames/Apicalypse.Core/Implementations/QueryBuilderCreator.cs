using Apicalypse.Core.Interfaces;

namespace Apicalypse.Core.Implementations;
public class QueryBuilderCreator : IQueryBuilderCreator
{
	private readonly IQueryParser _parser;
	public QueryBuilderCreator(IQueryParser parser) => _parser = parser;
	public IQueryBuilder<TEntity> CreateFor<TEntity>()
		=> new QueryBuilder<TEntity>(_parser);
}
