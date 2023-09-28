using Apicalypse.Core.Enums;
using Apicalypse.Core.Interfaces;
using Apicalypse.Core.Interfaces.QueryBuilderStages;
using Apicalypse.Core.Interfaces.QueryBuilderSteps;
using Apicalypse.Core.StringEnums;

using RateGames.Common.Extensions;

namespace Apicalypse.Core.Implementations;

/// <inheritdoc cref="IFirstStageQueryBuilder{TEntity}" />
internal class QueryBuilder<TEntity> : IFirstStageQueryBuilder<TEntity>, ISecondStageQueryBuilder<TEntity>
{
	private readonly IQueryParser _parser;
	private readonly IMemberInfoStorage _memberInfoStorage;
	private readonly StringBuilder _stringBuilder;

	public QueryBuilder(IQueryParser parser, IMemberInfoStorage memberInfoStorage)
	{
		_parser = parser;
		_memberInfoStorage = memberInfoStorage;
		_stringBuilder = new();
	}

	IIncludingBuilder<TEntity> ISelectionBuilder<TEntity>.Select(IncludeType includeType)
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
	IIncludingBuilder<TEntity> ISelectionBuilder<TEntity>.Select<TProp>(
		Expression<Func<TEntity, TProp>> selector,
		SelectionMode selectionMode
	)
	{
		var parsed = _parser.Parse(selector);

		if (IsSelectionValid(parsed))
		{
			throw new ArgumentException("Selector is invalid");
		}

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
	IFilterBuilder<TEntity> IIncludingBuilder<TEntity>.Include<TProp>(Expression<Func<TEntity, TProp>> selector)
	{
		var parsed = _parser.Parse(selector);
		if (IsSelectionValid(parsed))
		{
			throw new ArgumentException("Selector is invalid");
		}

		var expreressionParts = parsed.Split(',');
		var newParts = new Dictionary<string, List<string>?>();
		foreach (var part in expreressionParts)
		{
			var expressionSplitted = part.Split('.');
			if (expressionSplitted.Length < 2)
			{
				continue;
			}
			var accessField = expressionSplitted[0];
			if (!newParts.TryGetValue(accessField, out var _) || newParts[accessField] is null)
			{
				newParts[accessField] = new List<string>() { part };
			}
			else
			{
				newParts[accessField]!.Add(part);
			}
		}
		foreach (var item in newParts)
		{
			var values = string.Join(',', item.Value!);
			_stringBuilder.Replace(item.Key, values);
		}

		return this;
	}

	ISortBuilder<TEntity> IFilterBuilder<TEntity>.Where(Expression<Func<TEntity, bool>> predicate)
	{
		var parsed = _parser.Parse(predicate);
		GenerateLine(QueryKeywords.Where, parsed);
		return this;
	}
	ISearchBuilder<TEntity> ISortBuilder<TEntity>.OrderBy<TProp>(
		Expression<Func<TEntity, TProp>> propSelector)
	{
		var parsed = _parser.Parse(propSelector);
		GenerateSortLine(parsed, QueryKeywords.Ascendging);
		return this;
	}
	ISearchBuilder<TEntity> ISortBuilder<TEntity>.OrderByDescending<TProp>(
		Expression<Func<TEntity, TProp>> propSelector)
	{
		var parsed = _parser.Parse(propSelector);
		GenerateSortLine(parsed, QueryKeywords.Descending);
		return this;
	}
	IOffsetBuilder<TEntity> ISearchBuilder<TEntity>.Find(string searchString)
	{
		GenerateSearchLine(searchString);
		return this;
	}
	ILimitBuilder<TEntity> IOffsetBuilder<TEntity>.Skip(int count)
	{
		GenerateLine(QueryKeywords.Offset, count);
		return this;
	}
	IResultBuilder<TEntity> ILimitBuilder<TEntity>.Take(int count)
	{
		GenerateLine(QueryKeywords.Limit, count);
		return this;
	}
	string IResultBuilder<TEntity>.Build()
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
	/// <summary>
	/// Validate <see cref="Include{TProp}(Expression{Func{TEntity, TProp}})"/> 
	/// and <see cref="Select{TProp}(Expression{Func{TEntity, TProp}}, SelectionMode)"/>
	/// </summary>
	/// <param name="parsedExpression"></param>
	/// <returns></returns>
	private bool IsSelectionValid(string parsedExpression) =>
		!parsedExpression.Contains('>')
		&& parsedExpression.Contains('<')
		&& parsedExpression.Contains('=');
}