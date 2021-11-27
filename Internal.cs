namespace Xorog.UniversalExtensions;

internal static class Internal
{
    internal static string GetShortTimeFormat(this TimeSpan _timespan, TimeFormat timeFormat)
    {
        switch (timeFormat)
        {
            case TimeFormat.HOURS:
                if (_timespan.TotalDays >= 1)
                    return $"{(Math.Floor(_timespan.TotalHours).ToString().Length == 1 ? $"0{Math.Floor(_timespan.TotalHours)}" : Math.Floor(_timespan.TotalHours))}:" +
                        $"{(_timespan.Minutes.ToString().Length == 1 ? $"0{_timespan.Minutes}" : _timespan.Minutes)}:" +
                        $"{(_timespan.Seconds.ToString().Length == 1 ? $"0{_timespan.Seconds}" : _timespan.Seconds)}";

                if (_timespan.TotalHours >= 1)
                    return $"{(_timespan.Hours.ToString().Length == 1 ? $"0{_timespan.Hours}" : _timespan.Hours)}:" +
                        $"{(_timespan.Minutes.ToString().Length == 1 ? $"0{_timespan.Minutes}" : _timespan.Minutes)}:" +
                        $"{(_timespan.Seconds.ToString().Length == 1 ? $"0{_timespan.Seconds}" : _timespan.Seconds)}";

                return $"{(_timespan.Minutes.ToString().Length == 1 ? $"0{_timespan.Minutes}" : _timespan.Minutes)}:" +
                       $"{(_timespan.Seconds.ToString().Length == 1 ? $"0{_timespan.Seconds}" : _timespan.Seconds)}";
            case TimeFormat.DAYS:
                if (_timespan.TotalDays >= 1)
                    return $"{(Math.Floor(_timespan.TotalDays).ToString().Length == 1 ? $"0{Math.Floor(_timespan.TotalDays)}" : Math.Floor(_timespan.TotalDays))}" +
                            $"{(_timespan.Hours.ToString().Length == 1 ? $"0{_timespan.Hours}" : _timespan.Hours)}:" +
                            $"{(_timespan.Minutes.ToString().Length == 1 ? $"0{_timespan.Minutes}" : _timespan.Minutes)}:" +
                            $"{(_timespan.Seconds.ToString().Length == 1 ? $"0{_timespan.Seconds}" : _timespan.Seconds)}";

                if (_timespan.TotalHours >= 1)
                    return $"{(Math.Floor(_timespan.TotalHours).ToString().Length == 1 ? $"0{Math.Floor(_timespan.TotalHours)}" : Math.Floor(_timespan.TotalHours))}:" +
                            $"{(_timespan.Minutes.ToString().Length == 1 ? $"0{_timespan.Minutes}" : _timespan.Minutes)}:" +
                            $"{(_timespan.Seconds.ToString().Length == 1 ? $"0{_timespan.Seconds}" : _timespan.Seconds)}";

                return $"{(_timespan.Minutes.ToString().Length == 1 ? $"0{_timespan.Minutes}" : _timespan.Minutes)}:" +
                       $"{(_timespan.Seconds.ToString().Length == 1 ? $"0{_timespan.Seconds}" : _timespan.Seconds)}";

            case TimeFormat.MINUTES:
                if (_timespan.TotalHours >= 1)
                    return $"{(Math.Floor(_timespan.TotalMinutes).ToString().Length == 1 ? $"0{Math.Floor(_timespan.TotalMinutes)}" : Math.Floor(_timespan.TotalMinutes))}:" +
                            $"{(_timespan.Seconds.ToString().Length == 1 ? $"0{_timespan.Seconds}" : _timespan.Seconds)}";

                return $"{(_timespan.Minutes.ToString().Length == 1 ? $"0{_timespan.Minutes}" : _timespan.Minutes)}:" +
                       $"{(_timespan.Seconds.ToString().Length == 1 ? $"0{_timespan.Seconds}" : _timespan.Seconds)}";

            default:
                return _timespan.ToString();
        }
    }
    internal static string GetTimeFormat(this TimeSpan _timespan, TimeFormat timeFormat)
    {
        switch (timeFormat)
        {
            case TimeFormat.HOURS:
                if (_timespan.TotalDays >= 1)
                    return $"{Math.Floor(_timespan.TotalHours)} hours, {_timespan.Minutes} minutes";

                if (_timespan.TotalHours >= 1)
                    return $"{_timespan.Hours} hours, {_timespan.Minutes} minutes";

                return $"{_timespan.Minutes} minutes, {_timespan.Seconds} seconds";
            case TimeFormat.DAYS:
                if (_timespan.TotalDays >= 1)
                    return $"{Math.Floor(_timespan.TotalDays)} days, {_timespan.Hours} hours";

                if (_timespan.TotalHours >= 1)
                    return $"{_timespan.Hours} hours, {_timespan.Minutes} minutes";

                return $"{_timespan.Minutes} minutes, {_timespan.Seconds} seconds";

            case TimeFormat.MINUTES:
                if (_timespan.TotalHours >= 1)
                    return $"{Math.Floor(_timespan.TotalMinutes)} minutes, {_timespan.Seconds} seconds";
                return $"{_timespan.Minutes} minutes, {_timespan.Seconds} seconds";

            default:
                return _timespan.ToString();
        }
    }
}