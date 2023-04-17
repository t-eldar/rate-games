using Apicalypse.Core.Enums;
using Apicalypse.Core.Interfaces;
using Apicalypse.Core.Interfaces.QueryBuilderStages;
using Apicalypse.Core.StringEnums;

using RateGames.Common.Extensions;

namespace Apicalypse.Core.Implementations;

/// <inheritdoc cref="IQueryBuilder{TEntity}" />
internal class QueryBuilder<TEntity> : IQueryBuilder<TEntity>
{
	private readonly IQueryParser _parser;
	private readonly IMemberInfoStorage _memberInfoStorage;
	private readonly StringBuilder _stringBuilder = new();

	public QueryBuilder(IQueryParser parser, IMemberInfoStorage memberInfoStorage)
	{
		_parser = parser;
		_memberInfoStorage = memberInfoStorage;
	}

	public IFilterBuilder<TEntity> Select(IncludeType includeType)
	{
		switch (includeType)
		{
			case IncludeType.EveryFromModel:
			{
				var props = _memberInfoStorage.GetProperties<TEntity>();
				_stringBuilder.Append(QueryKeywords.Fields);
				_stringBuilder.Append(QueryChars.SpaceChar);
				foreach (var prop in props)
				{
					_stringBuilder.Append(prop.Name);
					_stringBuilder.Append(QueryChars.ValueSeparatorChar);
				}
				_stringBuilder.Remove(_stringBuilder.Length - 1, 1);
				_stringBuilder.AppendLine(QueryChars.LineSeparator);
				return this;
			}
			case IncludeType.EveryFromDatabase:
			{
				GenerateLine(QueryKeywords.Fields, QueryChars.AllProperies);
				return this;
			}
			default:
			{
				throw new NotImplementedException();
			}
		}
	}
	public IFilterBuilder<TEntity> Select<TProp>(
		Expression<Func<TEntity, TProp>> selector,
		SelectionMode selectionMode = SelectionMode.Include
	)
	{
		var parsed = _parser.Parse(selector);

		switch (selectionMode)
		{
			case SelectionMode.Exclude:
			{
				_stringBuilder.Append(QueryKeywords.Fields);
				_stringBuilder.Append(QueryChars.SpaceChar);
				_stringBuilder.Append(QueryChars.AllProperiesChar);
				_stringBuilder.AppendLine(QueryChars.LineSeparator);
				GenerateLine(QueryKeywords.Exclude, parsed);
				return this;
			}
			case SelectionMode.Include:
			{
				GenerateLine(QueryKeywords.Fields, parsed);
				return this;
			}
			default:
			{
				throw new NotImplementedException();
			}
		}
	}
	public ISortBuilder<TEntity> Where(Expression<Func<TEntity, bool>> predicate)
	{
		var parsed = _parser.Parse(predicate);
		GenerateLine(QueryKeywords.Where, parsed);
		return this;
	}
	public ISearchBuilder<TEntity> OrderBy<TProp>(
		Expression<Func<TEntity, TProp>> propSelector)
	{
		var parsed = _parser.Parse(propSelector);
		GenerateSortLine(parsed, QueryKeywords.Ascendging);
		return this;
	}
	public ISearchBuilder<TEntity> OrderByDescending<TProp>(
		Expression<Func<TEntity, TProp>> propSelector)
	{
		var parsed = _parser.Parse(propSelector);
		GenerateSortLine(parsed, QueryKeywords.Descending);
		return this;
	}
	public ILimitBuilder<TEntity> Skip(int count)
	{
		GenerateLine(QueryKeywords.Offset, count);
		return this;
	}
	public IResultBuilder<TEntity> Take(int count)
	{
		GenerateLine(QueryKeywords.Limit, count);
		return this;
	}
	public IOffsetBuilder<TEntity> Find(string searchString)
	{
		GenerateSearchLine(searchString);
		return this;
	}
	public string Build()
	{
		var result = _stringBuilder.ToString();
		_stringBuilder.Clear();
		result = result.ToSnakeCase();
		return result;
	}

	private void GenerateLine<T>(string keyword, T value)
	{
		_stringBuilder.Append(keyword);
		_stringBuilder.Append(QueryChars.SpaceChar);
		_stringBuilder.Append(value);
		_stringBuilder.AppendLine(QueryChars.LineSeparator);
	}
	private void GenerateSortLine(string parsed, string order)
	{
		_stringBuilder.Append(QueryKeywords.Sort);
		_stringBuilder.Append(QueryChars.SpaceChar);
		_stringBuilder.Append(parsed);
		_stringBuilder.Append(QueryChars.SpaceChar);
		_stringBuilder.Append(order);
		_stringBuilder.AppendLine(QueryChars.LineSeparator);
	}
	private void GenerateSearchLine(string searchString)
	{
		_stringBuilder.Append(QueryKeywords.Search);
		_stringBuilder.Append(QueryChars.SpaceChar);
		_stringBuilder.Append(QueryChars.QuoteChar);
		_stringBuilder.Append(searchString);
		_stringBuilder.Append(QueryChars.QuoteChar);
		_stringBuilder.AppendLine(QueryChars.LineSeparator);
	}
}