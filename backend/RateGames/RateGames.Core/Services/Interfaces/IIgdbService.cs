namespace RateGames.Core.Services.Interfaces;

/// <summary>
/// Service for sendig requests to <seealso href="igdb.com" />.
/// </summary>
public interface IIgdbService
{
    /// <summary>
    /// Receives <typeparamref name="T"/> entity from database.
    /// </summary>
    /// <typeparam name="T">Model of entity from database.</typeparam>
    /// <param name="query">Apicalypse query.</param>
    /// <param name="endpoint">Endpoint of entities.</param>
    /// <returns></returns>
    public Task<T?> GetAsync<T>(string query, string endpoint);
}
