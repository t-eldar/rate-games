using Apicalypse.Core.Extensions;

using FluentValidation;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

using RateGames.Authorization;
using RateGames.DatabaseContext;
using RateGames.Extensions;
using RateGames.Models.Entities;
using RateGames.Options;
using RateGames.Repositories.Implementations;
using RateGames.Repositories.Interfaces;
using RateGames.Services.Implementations;
using RateGames.Services.Interfaces;
using RateGames.Storages.Implementations;
using RateGames.Storages.Interfaces;
using RateGames.Validators;

var builder = WebApplication.CreateBuilder(args);

var dbConnection = builder.Configuration.GetConnectionString("RateGamesDb");

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

// Identity.
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

// Authorization.
builder.Services.AddAuthorization(options =>
{
	options.AddPolicy(AuthorizationPolicies.SameAuthor, policy =>
	{
		policy.Requirements.Add(new SameAuthorRequirement());
		policy.RequireAuthenticatedUser();
	});
});
builder.Services.AddSingleton<IAuthorizationHandler, SameAuthorAuthorizationHandler>();

// Repositories.
builder.Services.AddTransient<IReviewRepository, ReviewRepository>();

// Other.
builder.Services.AddValidatorsFromAssemblyContaining<IValidatorMark>();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<IDateTimeProvider, DateTimeProvider>();

builder.Services.AddApicalypseQueryBuilderCreator();
builder.Services.AddSingleton<ITokenStorage, TokenStorage>();
builder.Services.AddIgdbServices();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
	app.UseExceptionHandler("/error-development");
}
else
{
	app.UseExceptionHandler("/error");
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
