namespace Apicalypse.Core.Interfaces;

/// <summary>
/// Parser of <see cref="Expression"/> into Apicalypse queries.
/// </summary>
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