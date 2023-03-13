using System.Net.Http.Headers;
using System.Net.Http.Json;
using Microsoft.Extensions.Configuration;
using System.Text.Json;
using RateGames.Core.Services.Interfaces;
using RateGames.Common.Utils;
using RateGames.Common.Converters;

namespace RateGames.Core.Services.Implementations;

/// <inheritdoc cref="IIgdbService"/>
public class IgdbService : IIgdbService
{
    private readonly HttpClient _httpClient;
    private readonly ITwitchTokenService _twitchTokenService;
    private readonly IConfiguration _configuration;
    public IgdbService(
        HttpClient httpClient,
        ITwitchTokenService twitterTokenService,
        IConfiguration configuration)
    {
        _httpClient = httpClient;
        _twitchTokenService = twitterTokenService;
        _configuration = configuration;
    }

    public async Task<T?> GetAsync<T>(string query, string endpoint)
    {
        var token = await _twitchTokenService.GetTokenAsync();

        using var requestMessage = new HttpRequestMessage(HttpMethod.Post, endpoint);

        requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
        requestMessage.Headers.Add("Client-ID", _configuration.GetSection("Twitch:ClientId").Value);
        requestMessage.Content = new StringContent(query);

        var response = await _httpClient.SendAsync(requestMessage);
        response.EnsureSuccessStatusCode();

        var result = await response.Content.ReadFromJsonAsync<T>(new JsonSerializerOptions()
        {
            PropertyNamingPolicy = new SnakeCaseNamingPolicy(),
            Converters = { new EntityConverter() }
        });

        return result;
    }
}
