using System.Linq.Expressions;

namespace Apicalypse.Core.Interfaces;

internal interface IQueryParser<TEntity>
{
	/// <summary>
	/// Parses <paramref name="expression"/> into query string.
	/// </summary>
	/// <typeparam name="TProp"></typeparam>
	/// <param name="expression"></param>
	/// <returns></returns>
	string Parse<TProp>(Expression<Func<TEntity, TProp>> expression);
}