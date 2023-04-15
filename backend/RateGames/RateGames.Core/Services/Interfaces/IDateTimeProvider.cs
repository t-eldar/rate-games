using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RateGames.Core.Services.Interfaces;

/// <summary>
/// Service for getting current date or time.
/// </summary>

public interface IDateTimeProvider
{
    /// <summary>
    /// Current <see cref="DateTime"/> set on computer, represented as local time
    /// </summary>
    DateTime Current { get; }
    /// <summary>
    /// Current <see cref="DateTime"/> set on computer, represented as UTC.
    /// </summary>
    DateTime CurrentUtc { get; }

    /// <summary>
    /// Number of seconds that have been elapsed since 1970-01-01T00:00:00Z
    /// </summary>
    long CurrentDateTimeOffset { get; } 
}
