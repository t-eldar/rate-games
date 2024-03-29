﻿using RateGames.Repositories.Implementations;
using RateGames.Repositories.Interfaces;
using RateGames.Services.Implementations;
using RateGames.Services.Interfaces;

namespace RateGames.Extensions;

public static class ServiceCollectionExtensions
{
	public static IServiceCollection AddIgdbServices(this IServiceCollection services)
	{
		services.AddHttpClient<ITwitchTokenService, TwitchTokenService>();
		services.AddHttpClient<IIgdbService, IgdbService>();

		services.AddTransient<IImageService, ImageService>();
		services.AddTransient<IGameService, GameService>();
		services.AddTransient<IGenreService, GenreService>();
		services.AddTransient<IPlatformService, PlatformService>();
		services.AddTransient<IGameEngineService, GameEngineService>();
		services.AddTransient<ICompanyService, CompanyService>();

		return services;
	}
	public static IServiceCollection AddRepositories(this IServiceCollection services)
	{
		services.AddTransient<IReviewRepository, ReviewRepository>();
		services.AddTransient<IRatingRepository, RatingRepository>();

		return services;
	}
}
