using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using RateGames.Core.Services.Interfaces;
using RateGames.Services.Interfaces;

namespace RateGames.Services.Implementations;

/// <inheritdoc cref="IDateTimeProvider"/>
public class DateTimeProvider : IDateTimeProvider
{
    public DateTime Current => DateTime.Now;
    public DateTime CurrentUtc => DateTime.UtcNow;
    public long CurrentDateTimeOffset => DateTimeOffset.UtcNow.ToUnixTimeSeconds();
}
