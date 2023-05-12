using RateGames.Services.Interfaces;

namespace RateGames.Services.Implementations;

/// <inheritdoc cref="IDateTimeProvider"/>
public class DateTimeProvider : IDateTimeProvider
{
	public DateTime Current => DateTime.Now;
	public DateTime CurrentUtc => DateTime.UtcNow;
	public long CurrentUnixTimestamp => DateTimeOffset.UtcNow.ToUnixTimeSeconds();
}
