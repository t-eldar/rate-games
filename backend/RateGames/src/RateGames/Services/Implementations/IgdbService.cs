using System.Net.Http.Headers;
using System.Text.Json;

using RateGames.Common.Utils;
using RateGames.Common.Converters;
using RateGames.Services.Interfaces;
using Microsoft.Extensions.Options;
using RateGames.Options;

namespace RateGames.Services.Implementations;

/// <inheritdoc cref="IIgdbService"/>
public class IgdbService : IIgdbService
{
	private readonly HttpClient _httpClient;
	private readonly ITwitchTokenService _twitchTokenService;
	private readonly TwitchOptions _options;
	private readonly ILogger<IgdbService> _logger;
	public IgdbService(
		HttpClient httpClient,
		ITwitchTokenService twitterTokenService,
		IOptions<TwitchOptions> options,
		ILogger<IgdbService> logger
	)
	{
		_httpClient = httpClient;
		_twitchTokenService = twitterTokenService;
		_options = options.Value;
		_logger = logger;
	}

	public async Task<T?> GetAsync<T>(string query, string endpoint)
	{
		_logger.LogInformation("Apicalypse query \n {Query}", query);

		var token = await _twitchTokenService.GetTokenAsync();

		using var requestMessage = new HttpRequestMessage(HttpMethod.Post, endpoint);

		requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
		requestMessage.Headers.Add("Client-ID", _options.ClientId);
		requestMessage.Content = new StringContent(query);

		var response = await _httpClient.SendAsync(requestMessage);
		response.EnsureSuccessStatusCode();

		var result = await response.Content.ReadFromJsonAsync<T>(new JsonSerializerOptions()
		{
			PropertyNamingPolicy = new SnakeCaseNamingPolicy(),
			Converters = { new IdOrConverterFactory() }
		});

		return result;
	}
}
