using Apicalypse.Core.Interfaces;

namespace Apicalypse.Core.Implementations;

/// <inheritdoc cref="IQueryBuilderCreator" />
public class QueryBuilderCreator : IQueryBuilderCreator
{
	private readonly IQueryParser _parser;
	private readonly IMemberInfoStorage _memberInfoStorage;
	public QueryBuilderCreator(IQueryParser parser, IMemberInfoStorage memberInfoStorage)
	{
		_parser = parser;
		_memberInfoStorage = memberInfoStorage;
	}

	public IQueryBuilder<TEntity> CreateFor<TEntity>() => 
		new QueryBuilder<TEntity>(_parser, _memberInfoStorage);
}
