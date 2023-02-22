using System.Linq.Expressions;

namespace Apicalypse.Core.Interfaces;

public interface IQueryParser
{
	/// <summary>
	/// Parses <paramref name="expression"/> into query string.
	/// </summary>
	/// <typeparam name="TProp"></typeparam>
	/// <param name="expression"></param>
	/// <returns></returns>
	string Parse<TEntity, TProp>(Expression<Func<TEntity, TProp>> expression);
}