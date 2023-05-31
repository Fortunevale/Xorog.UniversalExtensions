namespace Xorog.UniversalExtensions;

internal static class Internal
{
    internal static string GetShortTimeFormat(this TimeSpan _timespan, TimeFormat timeFormat)
    {
        switch (timeFormat)
        {
            case TimeFormat.DAYS:
                if (_timespan.TotalDays >= 1)
                    return $"{Math.Floor(_timespan.TotalDays).ToString().PadLeft(2, '0')}:{_timespan.Hours.ToString().PadLeft(2, '0')}:{_timespan.Minutes.ToString().PadLeft(2, '0')}:{_timespan.Seconds.ToString().PadLeft(2, '0')}";

                if (_timespan.TotalHours >= 1)
                    return $"{Math.Floor(_timespan.TotalHours).ToString().PadLeft(2, '0')}:{_timespan.Minutes.ToString().PadLeft(2, '0')}:{_timespan.Seconds.ToString().PadLeft(2, '0')}";

                return $"{_timespan.Minutes.ToString().PadLeft(2, '0')}:{_timespan.Seconds.ToString().PadLeft(2, '0')}";

            case TimeFormat.HOURS:
                if (_timespan.TotalDays >= 1)
                    return $"{Math.Floor(_timespan.TotalHours).ToString().PadLeft(2, '0')}:" +
                        $"{_timespan.Minutes.ToString().PadLeft(2, '0')}:{_timespan.Seconds.ToString().PadLeft(2, '0')}";

                if (_timespan.TotalHours >= 1)
                    return $"{_timespan.Hours.ToString().PadLeft(2, '0')}:" +
                        $"{_timespan.Minutes.ToString().PadLeft(2, '0')}:{_timespan.Seconds.ToString().PadLeft(2, '0')}";

                return $"{_timespan.Minutes.ToString().PadLeft(2, '0')}:{_timespan.Seconds.ToString().PadLeft(2, '0')}";

            case TimeFormat.MINUTES:
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

        switch (timeFormat)
        {
            case TimeFormat.DAYS:
                if (_timespan.TotalDays >= 1)
                    return $"{Math.Floor(_timespan.TotalDays)} {config.DaysString}, {_timespan.Hours} {config.HoursString}";

                if (_timespan.TotalHours >= 1)
                    return $"{_timespan.Hours} {config.HoursString}, {_timespan.Minutes} {config.MinutesString}";

                return $"{_timespan.Minutes} {config.MinutesString}, {_timespan.Seconds} {config.SecondsString}";

            case TimeFormat.HOURS:
                if (_timespan.TotalDays >= 1)
                    return $"{Math.Floor(_timespan.TotalHours)} {config.HoursString}, {_timespan.Minutes} {config.MinutesString}";

                if (_timespan.TotalHours >= 1)
                    return $"{_timespan.Hours} {config.HoursString}, {_timespan.Minutes} {config.MinutesString}";

                return $"{_timespan.Minutes} {config.MinutesString}, {_timespan.Seconds} {config.SecondsString}";

            case TimeFormat.MINUTES:
                if (_timespan.TotalHours >= 1)
                    return $"{Math.Floor(_timespan.TotalMinutes)} {config.MinutesString}, {_timespan.Seconds} {config.SecondsString}";
                return $"{_timespan.Minutes} {config.MinutesString}, {_timespan.Seconds} {config.MinutesString}";

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

public class InternalSheduler
{
    public static Dictionary<string, ScheduledTask> RegisteredScheduledTasks { get; internal set; } = new Dictionary<string, ScheduledTask>();

    public class ScheduledTask
    {
        public ScheduledTask()
        {
            this.Uid = Guid.NewGuid().ToString();
        }

        /// <summary>
        /// The unique identifier of this task.
        /// </summary>
        public string Uid { get; internal set; }

        /// <summary>
        /// The custom data asscociated with this task.
        /// </summary>
        public object? CustomData { get; internal set; }

        /// <summary>
        /// The time this task will run.
        /// </summary>
        public DateTime? RunTime { get; internal set; }

        /// <summary>
        /// The <see cref="CancellationTokenSource"/> to prematurely dequeue this task.
        /// </summary>
        internal CancellationTokenSource? TokenSource { get; set; } = new();

        /// <summary>
        /// Delete this task.
        /// </summary>
        public void Delete() =>
            UniversalExtensions.DeleteScheduledTask(Uid);
    }
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
}