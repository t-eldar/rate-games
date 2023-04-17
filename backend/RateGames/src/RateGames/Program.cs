using Apicalypse.Core.Extensions;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

using RateGames.DatabaseContext;
using RateGames.Models.Entities;
using RateGames.Options;
using RateGames.Services.Implementations;
using RateGames.Services.Interfaces;
using RateGames.Storages.Implementations;
using RateGames.Storages.Interfaces;

var builder = WebApplication.CreateBuilder(args);	

var dbConnection = builder.Configuration.GetConnectionString("RateGamesDb");

// Add services to the container.

builder.Services.Configure<TwitchOptions>(
	builder.Configuration.GetSection(TwitchOptions.Twitch));

builder.Services.AddCors(options =>
{
	options.AddDefaultPolicy(builder =>
	{
		builder
			.WithOrigins("https://localhost:3000")
			.AllowCredentials()
			.AllowAnyMethod()
			.AllowAnyHeader();
	});
});

builder.Services.AddDbContext<IApplicationContext, ApplicationContext>(options =>
{
	options.UseSqlServer(dbConnection);
});

builder.Services
	.AddIdentity<User, IdentityRole>(options =>
	{
		options.User.RequireUniqueEmail = true;
	})
	.AddEntityFrameworkStores<ApplicationContext>();

builder.Services.ConfigureApplicationCookie(options =>
{
	options.Cookie.SameSite = SameSiteMode.None;
	options.LoginPath = string.Empty;
	options.AccessDeniedPath = string.Empty;

	options.Events.OnRedirectToAccessDenied = context =>
	{
		context.Response.StatusCode = StatusCodes.Status403Forbidden;
		return Task.CompletedTask;
	};
	options.Events.OnRedirectToLogin = context =>
	{
		context.Response.StatusCode = StatusCodes.Status401Unauthorized;
		return Task.CompletedTask;
	};
});

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<IDateTimeProvider, DateTimeProvider>();

builder.Services.AddApicalypseQueryBuilderCreator();

builder.Services.AddSingleton<ITokenStorage, TokenStorage>();

builder.Services.AddHttpClient<ITwitchTokenService, TwitchTokenService>();
builder.Services.AddHttpClient<IIgdbService, IgdbService>();

builder.Services.AddTransient<IGameService, GameService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
