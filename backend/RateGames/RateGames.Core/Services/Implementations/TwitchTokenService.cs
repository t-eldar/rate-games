using System.Net.Http.Json;
using System.Text.Json;

using Microsoft.Extensions.Configuration;

using RateGames.Common.Utils;
using RateGames.Core.Dtos;
using RateGames.Core.Models;
using RateGames.Core.Services.Interfaces;
using RateGames.Core.Storages.Interfaces;

namespace RateGames.Core.Services.Implementations;

/// <inheritdoc cref="ITwitchTokenService"/>
public class TwitchTokenService : ITwitchTokenService
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;
    private readonly ITokenStorage _tokenStorage;
    private readonly IDateTimeProvider _dateTimeProvider;

    private const string TokenKey = "TwitchToken";
    public TwitchTokenService(
        HttpClient httpClient,
        IConfiguration configuration,
        ITokenStorage tokenStorage,
        IDateTimeProvider dateTimeProvider)
    {
        _httpClient = httpClient;
        _configuration = configuration;
        _tokenStorage = tokenStorage;
        _dateTimeProvider = dateTimeProvider;
    }

    public async Task<string> GetTokenAsync()
    {
        var tokenDto = _tokenStorage.GetToken<TwitchTokenDto>(TokenKey);

        if (tokenDto is not null
            && tokenDto.CreatedAt + tokenDto.Value.ExpiresIn < DateTimeOffset.UtcNow.ToUnixTimeSeconds())
        {
            return tokenDto.Value.AccessToken;
        }

        var clientId = _configuration.GetRequiredSection("Twitch:ClientId").Value
            ?? throw new Exception("ClientId is missing in configuration");
        var clientSecret = _configuration.GetRequiredSection("Twitch:ClientSecret").Value
            ?? throw new Exception("ClientSecret is missing in configuration");
        var tokenAddress = _configuration.GetRequiredSection("Twitch:TokenAddress").Value
            ?? throw new Exception("TokenAddress is missing in configuration");

        var parameters = new Dictionary<string, string>
        {
            ["client_id"] = clientId,
            ["client_secret"] = clientSecret,
            ["grant_type"] = "client_credentials",
        };
        var encodedParams = new FormUrlEncodedContent(parameters);
        var response = await _httpClient.PostAsync(tokenAddress, encodedParams);
        response.EnsureSuccessStatusCode();
        var twitchToken = await response.Content.ReadFromJsonAsync<TwitchToken>(new JsonSerializerOptions()
        {
            PropertyNamingPolicy = new SnakeCaseNamingPolicy(),
        }) ?? throw new Exception();

        tokenDto = new TwitchTokenDto
        {
            Value = twitchToken,
            CreatedAt = DateTimeOffset.UtcNow.ToUnixTimeSeconds()
        };

        _tokenStorage.SetToken(TokenKey, tokenDto);

        return tokenDto.Value.AccessToken;
    }
}
