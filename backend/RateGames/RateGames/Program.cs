using Apicalypse.Core.Extensions;
using RateGames.Core.Services.Implementations;
using RateGames.Core.Services.Interfaces;
using RateGames.Core.Storages.Implementations;
using RateGames.Core.Storages.Interfaces;
using RateGames.Services.Implementations;
using RateGames.Services.Interfaces;
using RateGames.Storages.Implementations;
using RateGames.Storages.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddApicalypseQueryBuilderCreator();
builder.Services.AddSingleton<ITokenStorage, TokenStorage>();
builder.Services.AddHttpClient<ITwitchTokenService, TwitchTokenService>();
builder.Services.AddHttpClient<IIgdbService, IgdbService>();
builder.Services.AddTransient<IGameService, GameService>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
