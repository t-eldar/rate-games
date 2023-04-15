using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using RateGames.Core.Services.Interfaces;

namespace RateGames.Core.Services.Implementations;

/// <inheritdoc cref="IDateTimeProvider"/>
public class DateTimeProvider : IDateTimeProvider
{
    public DateTime Current => DateTime.Now;
    public DateTime CurrentUtc => DateTime.UtcNow;
    public long CurrentDateTimeOffset => DateTimeOffset.UtcNow.ToUnixTimeSeconds();
}
