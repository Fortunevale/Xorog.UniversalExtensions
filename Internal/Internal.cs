using Xorog.UniversalExtensions.Entities;

namespace Xorog.UniversalExtensions;

internal static class Internal
{
    internal static string GetShortTimeFormat(this TimeSpan _timespan, TimeFormat timeFormat)
    {
        switch (timeFormat)
        {
            case TimeFormat.Days:
                if (_timespan.TotalDays >= 1)
                    return $"{Math.Floor(_timespan.TotalDays).ToString().PadLeft(2, '0')}:{_timespan.Hours.ToString().PadLeft(2, '0')}:{_timespan.Minutes.ToString().PadLeft(2, '0')}:{_timespan.Seconds.ToString().PadLeft(2, '0')}";

                if (_timespan.TotalHours >= 1)
                    return $"{Math.Floor(_timespan.TotalHours).ToString().PadLeft(2, '0')}:{_timespan.Minutes.ToString().PadLeft(2, '0')}:{_timespan.Seconds.ToString().PadLeft(2, '0')}";

                return $"{_timespan.Minutes.ToString().PadLeft(2, '0')}:{_timespan.Seconds.ToString().PadLeft(2, '0')}";

            case TimeFormat.Hours:
                if (_timespan.TotalDays >= 1)
                    return $"{Math.Floor(_timespan.TotalHours).ToString().PadLeft(2, '0')}:" +
                        $"{_timespan.Minutes.ToString().PadLeft(2, '0')}:{_timespan.Seconds.ToString().PadLeft(2, '0')}";

                if (_timespan.TotalHours >= 1)
                    return $"{_timespan.Hours.ToString().PadLeft(2, '0')}:" +
                        $"{_timespan.Minutes.ToString().PadLeft(2, '0')}:{_timespan.Seconds.ToString().PadLeft(2, '0')}";

                return $"{_timespan.Minutes.ToString().PadLeft(2, '0')}:{_timespan.Seconds.ToString().PadLeft(2, '0')}";

            case TimeFormat.Minutes:
                if (_timespan.TotalHours >= 1)
                    return $"{Math.Floor(_timespan.TotalMinutes).ToString().PadLeft(2, '0')}:{_timespan.Seconds.ToString().PadLeft(2, '0')}";

                return $"{_timespan.Minutes.ToString().PadLeft(2, '0')}:{_timespan.Seconds.ToString().PadLeft(2, '0')}";

            default:
                return _timespan.ToString();
        }
    }
    internal static string GetTimeFormat(this TimeSpan _timespan, TimeFormat timeFormat, HumanReadableTimeFormatConfig? config = null)
    {
        config ??= new();

        string returningString = string.Empty;
        Dictionary<string, bool> humanReadable = new();

        switch (timeFormat)
        {
            case TimeFormat.Days:
            {
                humanReadable.Add($"{Math.Floor(_timespan.TotalDays)} {config.DaysString}", _timespan.TotalDays >= 1);
                humanReadable.Add($"{_timespan.Hours} {config.HoursString}", _timespan.TotalHours >= 1);
                humanReadable.Add($"{_timespan.Minutes} {config.MinutesString}", (config.MustIncludeMinutes && _timespan.TotalMinutes >= 1) || (_timespan.TotalMinutes >= 1 && _timespan.TotalDays < 1));
                humanReadable.Add($"{_timespan.Seconds} {config.SecondsString}", config.MustIncludeSeconds || _timespan.TotalHours < 1);

                return string.Join(", ", humanReadable.Where(x => x.Value).Select(x => x.Key));
            }

            case TimeFormat.Hours:
            {
                humanReadable.Add($"{Math.Floor(_timespan.TotalHours)} {config.HoursString}", _timespan.TotalHours >= 1);
                humanReadable.Add($"{_timespan.Minutes} {config.MinutesString}", (config.MustIncludeMinutes && _timespan.TotalMinutes >= 1) || _timespan.TotalHours >= 1);
                humanReadable.Add($"{_timespan.Seconds} {config.SecondsString}", config.MustIncludeSeconds || _timespan.TotalHours < 1);

                return string.Join(", ", humanReadable.Where(x => x.Value).Select(x => x.Key));
            }

            case TimeFormat.Minutes:
            {
                humanReadable.Add($"{Math.Floor(_timespan.TotalMinutes)} {config.MinutesString}", (config.MustIncludeMinutes && _timespan.TotalMinutes >= 1) || _timespan.TotalMinutes >= 1);
                humanReadable.Add($"{_timespan.Seconds} {config.SecondsString}", true);

                return string.Join(", ", humanReadable.Where(x => x.Value).Select(x => x.Key));
            }

            default:
                return _timespan.ToString();
        }
    }
    internal static int GetDiff(Color color, Color baseColor)
    {
        int a = color.A - baseColor.A,
            r = color.R - baseColor.R,
            g = color.G - baseColor.G,
            b = color.B - baseColor.B;
        return a * a + r * r + g * g + b * b;
    }
}

internal class InternalScheduler
{
    public static Dictionary<string, ScheduledTask> RegisteredScheduledTasks { get; internal set; } = new Dictionary<string, ScheduledTask>();
}

public class HumanReadableTimeFormatConfig
{
    /// <summary>
    /// The string used for days.
    /// Defaults to: "days".
    /// </summary>
    public string DaysString { get; set; } = "days";

    /// <summary>
    /// The string used for hours.
    /// Defaults to: "hours".
    /// </summary>
    public string HoursString { get; set; } = "hours";

    /// <summary>
    /// The string used for minutes.
    /// Defaults to: "minutes".
    /// </summary>
    public string MinutesString { get; set; } = "minutes";

    /// <summary>
    /// The string used for seconds.
    /// Defaults to: "seconds".
    /// </summary>
    public string SecondsString { get; set; } = "seconds";

    /// <summary>
    /// Must include minutes if timestamp is >= 1 day.
    /// Defaults to: false.
    /// </summary>
    public bool MustIncludeMinutes { get; set; } = false;

    /// <summary>
    /// Must include seconds if timestamp is >= 1 hour.
    /// Defaults to: false.
    /// </summary>
    public bool MustIncludeSeconds { get; set; } = false;
}