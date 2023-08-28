namespace Xorog.UniversalExtensions;

public static class TimeExtensions
{
    /// <summary>
    /// Get a timespan from now to the given time. Negative on values in the past.
    /// </summary>
    /// <param name="until"></param>
    /// <returns></returns>
    public static TimeSpan GetTimespanUntil(this DateTime until) =>
        (until.ToUniversalTime() - DateTime.UtcNow);

    /// <summary>
    /// <inheritdoc cref="UniversalExtensions.GetTimespanSince(DateTime)"/>
    /// </summary>
    /// <param name="until"></param>
    /// <returns></returns>
    public static TimeSpan GetTimespanUntil(this DateTimeOffset until) =>
        (until.ToUniversalTime() - DateTime.UtcNow);

    /// <summary>
    /// Get the total seconds until a given DateTime. Negative on values in the past.
    /// </summary>
    /// <param name="until"></param>
    /// <returns></returns>
    public static long GetTotalSecondsUntil(this DateTime until) =>
        ((long)Math.Ceiling((until.ToUniversalTime() - DateTime.UtcNow).TotalSeconds));

    /// <summary>
    /// <inheritdoc cref="UniversalExtensions.GetTotalSecondsUntil(DateTime)"/>
    /// </summary>
    /// <param name="until"></param>
    /// <returns></returns>
    public static long GetTotalSecondsUntil(this DateTimeOffset until) =>
        ((long)Math.Ceiling((until.ToUniversalTime() - DateTime.UtcNow).TotalSeconds));

    /// <summary>
    /// Get a timespan from now to the given time. Negative on values in the future.
    /// </summary>
    /// <param name="until"></param>
    /// <returns></returns>
    public static TimeSpan GetTimespanSince(this DateTime until) =>
        (DateTime.UtcNow - until.ToUniversalTime());

    /// <summary>
    /// <inheritdoc cref="UniversalExtensions.GetTimespanSince(DateTime)"/>
    /// </summary>
    /// <param name="until"></param>
    /// <returns></returns>
    public static TimeSpan GetTimespanSince(this DateTimeOffset until) =>
        (DateTime.UtcNow - until.ToUniversalTime());

    /// <summary>
    /// Get the total seconds since a given DateTime. Negative on values in the future.
    /// </summary>
    /// <param name="until"></param>
    /// <returns></returns>
    public static long GetTotalSecondsSince(this DateTime until) =>
        ((long)Math.Ceiling((DateTime.UtcNow - until.ToUniversalTime()).TotalSeconds));

    /// <summary>
    /// <inheritdoc cref="UniversalExtensions.GetTotalSecondsUntil(DateTime)"/>
    /// </summary>
    /// <param name="until"></param>
    /// <returns></returns>
    public static long GetTotalSecondsSince(this DateTimeOffset until) =>
        ((long)Math.Ceiling((DateTime.UtcNow - until.ToUniversalTime()).TotalSeconds));

    /// <summary>
    /// Get a short human readable string for the given amount of seconds
    /// </summary>
    /// <param name="seconds"></param>
    /// <param name="timeFormat"></param>
    /// <returns></returns>
    public static string GetShortHumanReadable(this int seconds, TimeFormat timeFormat = TimeFormat.Days) =>
        TimeSpan.FromSeconds(seconds).GetShortTimeFormat(timeFormat);

    /// <summary>
    /// <inheritdoc cref="UniversalExtensions.GetShortHumanReadable(int, TimeFormat)"/>
    /// </summary>
    /// <param name="seconds"></param>
    /// <param name="timeFormat"></param>
    /// <returns></returns>
    public static string GetShortHumanReadable(this long seconds, TimeFormat timeFormat = TimeFormat.Days) =>
        TimeSpan.FromSeconds(seconds).GetShortTimeFormat(timeFormat);

    /// <summary>
    /// Get a short human readable string for the given amount of time
    /// </summary>
    /// <param name="timespan"></param>
    /// <param name="timeFormat"></param>
    /// <returns></returns>
    public static string GetShortHumanReadable(this TimeSpan timespan, TimeFormat timeFormat = TimeFormat.Days) =>
        timespan.GetShortTimeFormat(timeFormat);

    /// <summary>
    /// Get a human readable string for the given amount of time.
    /// </summary>
    /// <param name="seconds"></param>
    /// <param name="timeFormat"></param>
    /// <returns></returns>
    public static string GetHumanReadable(this int seconds, TimeFormat timeFormat = TimeFormat.Days, HumanReadableTimeFormatConfig? config = null) =>
        TimeSpan.FromSeconds(seconds).GetTimeFormat(timeFormat, config);

    /// <inheritdoc cref="UniversalExtensions.GetHumanReadable(int, TimeFormat)"/>
    public static string GetHumanReadable(this long seconds, TimeFormat timeFormat = TimeFormat.Days, HumanReadableTimeFormatConfig? config = null) =>
        TimeSpan.FromSeconds(seconds).GetTimeFormat(timeFormat, config);

    /// <inheritdoc cref="UniversalExtensions.GetHumanReadable(int, TimeFormat)"/>
    public static string GetHumanReadable(this TimeSpan timeSpan, TimeFormat timeFormat = TimeFormat.Days, HumanReadableTimeFormatConfig? config = null) =>
        timeSpan.GetTimeFormat(timeFormat, config);
}
