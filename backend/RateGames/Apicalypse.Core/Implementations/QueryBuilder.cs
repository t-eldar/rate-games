using System.Linq.Expressions;
using System.Text;

using Apicalypse.Core.Enums;
using Apicalypse.Core.Extensions;
using Apicalypse.Core.Interfaces;
using Apicalypse.Core.Interfaces.QueryBuilderStages;
using Apicalypse.Core.StringEnums;

namespace Apicalypse.Core.Implementations;
internal class QueryBuilder<TEntity> : IQueryBuilder<TEntity>
{
	private readonly IQueryParser _parser;
	private readonly StringBuilder _stringBuilder = new();

	public QueryBuilder(IQueryParser parser) => _parser = parser;

	/// <summary>
	/// Use this constructor when DI is not needed.
	/// </summary>
	public QueryBuilder() => _parser = new QueryParser(new MethodPerformer());

	/// <summary>
	/// Selects all properties of object.
	/// </summary>
	/// <param name="includeType"></param>
	/// <returns></returns>
	/// <exception cref="NotImplementedException"></exception>
	public IFilterBuilder<TEntity> Select(IncludeType includeType)
	{
		switch (includeType)
		{
			case IncludeType.EveryFromModel:
			{
				var props = typeof(TEntity).GetProperties();
				_stringBuilder.Append(QueryKeywords.Fields);
				_stringBuilder.Append(QueryChars.SpaceChar);
				foreach (var prop in props)
				{
					_stringBuilder.Append(prop.Name);
					_stringBuilder.Append(QueryChars.ValueSeparator);
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
	/// <summary>
	/// Selects properties of object given in expression. 
	/// Excludes them if <paramref name="selectionMode"/> is Exclude.
	/// For multiple selection create new anonymous object.
	/// </summary>
	/// <typeparam name="TProp"></typeparam>
	/// <param name="selector"></param>
	/// <param name="selectionMode"></param>
	/// <returns></returns>
	/// <exception cref="NotImplementedException"></exception>
	public IFilterBuilder<TEntity> Select<TProp>(
		Expression<Func<TEntity, TProp>> selector,
		SelectionMode selectionMode = SelectionMode.Include)
	{
		var parsed = _parser.Parse(selector);

		switch (selectionMode)
		{
			case SelectionMode.Exclude:
			{
				_stringBuilder.Append(QueryKeywords.Fields);
				_stringBuilder.Append(QueryChars.SpaceChar);
				_stringBuilder.Append(QueryChars.AllProperies);
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
	/// <summary>
	/// Filters selection.
	/// </summary>
	/// <param name="predicate"></param>
	/// <returns></returns>
	public ISortBuilder<TEntity> Where(
		Expression<Func<TEntity, bool>> predicate)
	{
		var parsed = _parser.Parse(predicate);
		GenerateLine(QueryKeywords.Where, parsed);
		return this;
	}
	/// <summary>
	/// Orders selection in ascending order.
	/// </summary>
	/// <typeparam name="TProp"></typeparam>
	/// <param name="propSelector"></param>
	/// <returns></returns>
	public ISearchBuilder<TEntity> OrderBy<TProp>(
		Expression<Func<TEntity, TProp>> propSelector)
	{
		var parsed = _parser.Parse(propSelector);
		GenerateSortLine(parsed, QueryKeywords.Ascendging);
		return this;
	}
	/// <summary>
	/// Orders selection in descending order.
	/// </summary>
	/// <typeparam name="TProp"></typeparam>
	/// <param name="propSelector"></param>
	/// <returns></returns>
	public ISearchBuilder<TEntity> OrderByDescending<TProp>(
		Expression<Func<TEntity, TProp>> propSelector)
	{
		var parsed = _parser.Parse(propSelector);
		GenerateSortLine(parsed, QueryKeywords.Descending);
		return this;
	}
	/// <summary>
	/// Set offset for selection.
	/// </summary>
	/// <param name="count"></param>
	/// <returns></returns>
	public ILimitBuilder<TEntity> Skip(int count)
	{
		GenerateLine(QueryKeywords.Offset, count);
		return this;
	}
	/// <summary>
	/// Set limit for selection.
	/// </summary>
	/// <param name="count"></param>
	/// <returns></returns>
	public IStringBuilder<TEntity> Take(int count)
	{
		GenerateLine(QueryKeywords.Limit, count);
		return this;
	}
	/// <summary>
	/// Searchs <paramref name="searchString"/> in database.
	/// </summary>
	/// <param name="searchString"></param>
	/// <returns></returns>
	public IOffsetBuilder<TEntity> Find(string searchString)
	{
		GenerateSearchLine(searchString);
		return this;
	}
	/// <summary>
	/// Builds Apicalypse query string.
	/// </summary>
	/// <returns></returns>
	public string? Build()
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