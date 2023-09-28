using Apicalypse.Core.Interfaces;
using Apicalypse.Core.Interfaces.QueryBuilderStages;

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

	public IFirstStageQueryBuilder<TEntity> CreateFor<TEntity>() => 
		new QueryBuilder<TEntity>(_parser, _memberInfoStorage);
}
