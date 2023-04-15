using RateGames.Storages.Interfaces;

namespace RateGames.Storages.Implementations;

/// <inheritdoc cref="ITokenStorage"/>
public sealed class TokenStorage : ITokenStorage
{
    private readonly Dictionary<string, object> _storage = new();

    public TToken? GetToken<TToken>(string key)
    {
        if (_storage.TryGetValue(key, out var token))
        {
            return (TToken)token;
        }

        return default;
    }

    public void SetToken<TToken>(string key, TToken value) =>
        _storage[key] = value ?? throw new ArgumentNullException(nameof(value));
}
