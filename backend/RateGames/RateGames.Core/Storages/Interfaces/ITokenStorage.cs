namespace RateGames.Core.Storages.Interfaces;
public interface ITokenStorage
{
    TToken? GetToken<TToken>(string key);
    void SetToken<TToken>(string key, TToken value);
}
