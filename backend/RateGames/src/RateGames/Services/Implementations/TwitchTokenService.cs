using System.Text.Json;

using Microsoft.Extensions.Options;

using RateGames.Common.Utils;
using RateGames.Models.Dtos;
using RateGames.Models.Tokens;
using RateGames.Options;
using RateGames.Services.Interfaces;
using RateGames.Storages.Interfaces;


namespace RateGames.Services.Implementations;

/// <inheritdoc cref="ITwitchTokenService"/>
public class TwitchTokenService : ITwitchTokenService
{
    private readonly HttpClient _httpClient;
    private readonly ITokenStorage _tokenStorage;
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly TwitchOptions _options;

    private const string TokenKey = "TwitchToken";
    public TwitchTokenService(
        HttpClient httpClient,
        ITokenStorage tokenStorage,
        IDateTimeProvider dateTimeProvider,
        IOptions<TwitchOptions> options)
    {
        _httpClient = httpClient;
        _tokenStorage = tokenStorage;
        _dateTimeProvider = dateTimeProvider;
        _options = options.Value;
    }

    public async Task<string> GetTokenAsync()
    {
        var tokenDto = _tokenStorage.GetToken<TwitchTokenDto>(TokenKey);

        if (tokenDto is not null
            && tokenDto.CreatedAt + tokenDto.Value.ExpiresIn < DateTimeOffset.UtcNow.ToUnixTimeSeconds())
        {
            return tokenDto.Value.AccessToken;
        }

        var parameters = new Dictionary<string, string>
        {
            ["client_id"] = _options.ClientId,
            ["client_secret"] = _options.ClientSecret,
            ["grant_type"] = "client_credentials",
        };
        var encodedParams = new FormUrlEncodedContent(parameters);
        var response = await _httpClient.PostAsync(_options.TokenAddress, encodedParams);
        response.EnsureSuccessStatusCode();
        var twitchToken = await response.Content.ReadFromJsonAsync<TwitchToken>(new JsonSerializerOptions()
        {
            PropertyNamingPolicy = new SnakeCaseNamingPolicy(),
        }) ?? throw new Exception();

        tokenDto = new TwitchTokenDto
        {
            Value = twitchToken,
            CreatedAt = _dateTimeProvider.CurrentUnixTimestamp,
        };

        _tokenStorage.SetToken(TokenKey, tokenDto);

        return tokenDto.Value.AccessToken;
    }
}
