namespace Apicalypse.Core.Interfaces.QueryBuilderSteps;

/// <summary>
/// Result builder, returns query as <see cref="String?"/>.
/// </summary>
/// <typeparam name="TEntity"></typeparam>
public interface IResultBuilder<TEntity>
{
	/// <summary>
	/// Builds query into string.
	/// </summary>
	/// <returns></returns>
	string Build();
}

