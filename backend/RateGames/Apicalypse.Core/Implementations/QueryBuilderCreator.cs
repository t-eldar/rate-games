using Apicalypse.Core.Interfaces;

namespace Apicalypse.Core.Implementations;

/// <inheritdoc cref="IQueryBuilderCreator" />
public class QueryBuilderCreator : IQueryBuilderCreator
{
	private readonly IExpressionParser _parser;
	public QueryBuilderCreator(IExpressionParser parser) => _parser = parser;
	public IQueryBuilder<TEntity> CreateFor<TEntity>()
		=> new QueryBuilder<TEntity>(_parser);
}
