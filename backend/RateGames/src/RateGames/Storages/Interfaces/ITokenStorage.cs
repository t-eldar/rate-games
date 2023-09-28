namespace RateGames.Storages.Interfaces;

/// <summary>
/// Storage for tokens.
/// </summary>
public interface ITokenStorage
{
	/// <summary>
	/// Returns token by key.
	/// </summary>
	/// <typeparam name="TToken">Type of token.</typeparam>
	/// <param name="key">Key of token for storage.</param>
	/// <returns>Token.</returns>
	TToken? GetToken<TToken>(string key);

	/// <summary>
	/// Sets token by key.
	/// </summary>
	/// <typeparam name="TToken">Type of token.</typeparam>
	/// <param name="key">Token key for storage.</param>
	/// <param name="value">Token.</param>
	void SetToken<TToken>(string key, TToken value);
}
