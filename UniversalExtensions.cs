﻿namespace Xorog.UniversalExtensions;

public static class UniversalExtensions
{
    /// <summary>
    /// Get the current CPU Usage on all plattforms
    /// </summary>
    /// <returns></returns>
    public static async Task<double> GetCpuUsageForProcess()
    {
        var startTime = DateTime.UtcNow;
        var startCpuUsage = Process.GetCurrentProcess().TotalProcessorTime;
        await Task.Delay(500);

        var endTime = DateTime.UtcNow;
        var endCpuUsage = Process.GetCurrentProcess().TotalProcessorTime;
        var cpuUsedMs = (endCpuUsage - startCpuUsage).TotalMilliseconds;
        var totalMsPassed = (endTime - startTime).TotalMilliseconds;
        var cpuUsageTotal = cpuUsedMs / (Environment.ProcessorCount * totalMsPassed);
        return cpuUsageTotal * 100;
    }



    /// <summary>
    /// Copy a directory recursively
    /// </summary>
    /// <param name="sourceDirName"></param>
    /// <param name="destDirName"></param>
    /// <param name="copySubDirs"></param>
    /// <exception cref="DirectoryNotFoundException"></exception>
    public static void DirectoryCopy(string sourceDirName, string destDirName, bool copySubDirs)
    {
        // Get the subdirectories for the specified directory.
        DirectoryInfo dir = new(sourceDirName);

        if (!dir.Exists)
        {
            throw new DirectoryNotFoundException(
                "Source directory does not exist or could not be found: "
                + sourceDirName);
        }

        DirectoryInfo[] dirs = dir.GetDirectories();

        // If the destination directory doesn't exist, create it.
        Directory.CreateDirectory(destDirName);

        // Get the files in the directory and copy them to the new location.
        FileInfo[] files = dir.GetFiles();
        foreach (FileInfo file in files)
        {
            string tempPath = Path.Combine(destDirName, file.Name);
            file.CopyTo(tempPath, false);
        }

        // If copying subdirectories, copy them and their contents to new location.
        if (copySubDirs)
        {
            foreach (DirectoryInfo subdir in dirs)
            {
                string tempPath = Path.Combine(destDirName, subdir.Name);
                DirectoryCopy(subdir.FullName, tempPath, copySubDirs);
            }
        }
    }



    /// <summary>
    /// Generate an ASCII Progressbar
    /// </summary>
    /// <param name="current">The current progress</param>
    /// <param name="max">The maximum progress</param>
    /// <param name="charlength">How long the ASCII Progressbar should be (default: <code>44</code>)</param>
    /// <param name="fill">What character the filled part should be (default: <code>█</code>)</param>
    /// <param name="empty">What character the not-filled part should be (default: <code>∙</code>)</param>
    /// <param name="start">What character the start-part should be (default: <code>[</code>)</param>
    /// <param name="end">What character the end-part part should be (default: <code>]</code>)</param>
    /// <returns>A progressbar</returns>
    public static string GenerateASCIIProgressbar(double current, double max, int charlength = 44, char fill = '█', char empty = '∙', char start = '[', char end = ']')
    {
        long first = (long)Math.Round((current / max) * charlength, 0);

        long second = charlength - first;

        string mediadisplay = start.ToString();

        for (long i = 0; i < first; i++)
            mediadisplay += fill;

        for (long i = 0; i < second; i++)
            mediadisplay += empty;

        mediadisplay += end;

        return mediadisplay;
    }



    /// <summary>
    /// Get the URL a redirect leads to (limited to StatusCodes 301, 303, 307, 308)
    /// </summary>
    /// <param name="url">The shortened URL</param>
    /// <returns>The URL the redirect leads to</returns>
    public static async Task<string> UnshortenUrl(string url)
    {
        HttpClient client = new(new HttpClientHandler() { AllowAutoRedirect = false });
        var request = await client.GetAsync(url);

        if (request.StatusCode is HttpStatusCode.Found 
            or HttpStatusCode.Redirect 
            or HttpStatusCode.RedirectKeepVerb
            or HttpStatusCode.RedirectMethod
            or HttpStatusCode.PermanentRedirect
            or HttpStatusCode.TemporaryRedirect)
        {
            if (request.Headers is not null && request.Headers.Location is not null)
                return await UnshortenUrl(request.Headers.Location.AbsoluteUri);
            else
                return url;
        }
        else
            return url;
    }



    /// <summary>
    /// Try deleting the given files and directories until able to
    /// </summary>
    /// <param name="DirectoryPaths">A list of directories to clean up</param>
    /// <param name="FilePaths">A list of files to clean up</param>
    /// <returns></returns>
    public static async Task CleanupFilesAndDirectories(List<string> DirectoryPaths, List<string> FilePaths)
    {
        foreach (string DirectoryPath in DirectoryPaths)
        {
            while (Directory.Exists(DirectoryPath))
            {
                try
                {
                    Directory.Delete(DirectoryPath, true);
                }
                catch (Exception)
                {
                    await Task.Delay(5000);
                }
            }
        }

        foreach (string FilePath in FilePaths)
        {
            while (File.Exists(FilePath))
            {
                try
                {
                    File.Delete(FilePath);
                }
                catch (Exception)
                {
                    await Task.Delay(5000);
                }
            }
        }
    }



    /// <summary>
    /// Create a scheduled task
    /// </summary>
    /// <param name="task">The task to run</param>
    /// <param name="runTime">The time to run the task</param>
    /// <returns>An unique identifier of the task</returns>
    public static string CreateScheduleTask(this Task task, DateTime runTime, string UserId = "")
    {
        string UID = Guid.NewGuid().ToString();
        CancellationTokenSource CancellationToken = new CancellationTokenSource();

        _ = Task.Delay(runTime.GetTimespanUntil().Milliseconds, CancellationToken.Token).ContinueWith(x =>
        {
            if (registeredScheduledTasks.ContainsKey(UID))
                registeredScheduledTasks.Remove(UID);

            if (x.IsCompletedSuccessfully)
                task.Start();
        });

        registeredScheduledTasks.Add(UID, new taskInfo { tokenSource = CancellationToken, userId = UserId, runTime = runTime});
        return UID;
    }



    /// <summary>
    /// Deletes a scheduled task
    /// </summary>
    /// <param name="UID">The task's unique identifier</param>
    /// <exception cref="Exception">Throws if the task hasn't been found or if an internal error occured</exception>
    public static void DeleteSheduleTask(string UID)
    {
        if (!registeredScheduledTasks.ContainsKey(UID))
            throw new Exception($"No sheduled task has been found with UID '{UID}'");

        if (registeredScheduledTasks[ UID ].tokenSource is null)
            throw new Exception($"Internal: There is no token source registered the specified task.");

        registeredScheduledTasks[ UID ].tokenSource?.Cancel();
        registeredScheduledTasks.Remove(UID);
        return;
    }



    /// <summary>
    /// Gets a list of all registered tasks
    /// </summary>
    /// <returns>A list of all registered tasks</returns>
    public static IReadOnlyDictionary<string, taskInfo>? GetSheduleTasks()
    {
        return registeredScheduledTasks.ToList() as IReadOnlyDictionary<string, taskInfo>;
    }



    /// <summary>
    /// Gets a specific task
    /// </summary>
    /// <param name="UID">The unique identifier of what task to get</param>
    /// <returns>The task</returns>
    /// <exception cref="Exception">Throws if the task has not been found</exception>
    public static taskInfo GetSheduleTask(string UID)
    {
        if (!registeredScheduledTasks.ContainsKey(UID))
            throw new Exception($"The specified task doesn't exist.");

        return registeredScheduledTasks[UID];
    }



    /// <summary>
    /// Compute the SHA256-Hash for a given file
    /// </summary>
    /// <param name="filePath"></param>
    /// <returns></returns>
    public static string ComputeSHA256Hash(string filePath)
    {
        using SHA256 _SHA256 = SHA256.Create();
        using FileStream fileStream = File.OpenRead(filePath);
        return BitConverter.ToString(_SHA256.ComputeHash(fileStream)).Replace("-", "").ToLowerInvariant();
    }



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
    public static string GetShortHumanReadable(this int seconds, TimeFormat timeFormat = TimeFormat.DAYS) =>
        TimeSpan.FromSeconds(seconds).GetShortTimeFormat(timeFormat);



    /// <summary>
    /// <inheritdoc cref="UniversalExtensions.GetShortHumanReadable(int, TimeFormat)"/>
    /// </summary>
    /// <param name="seconds"></param>
    /// <param name="timeFormat"></param>
    /// <returns></returns>
    public static string GetShortHumanReadable(this long seconds, TimeFormat timeFormat = TimeFormat.DAYS) =>
        TimeSpan.FromSeconds(seconds).GetShortTimeFormat(timeFormat);



    /// <summary>
    /// Get a human readable string for the given amount of seconds
    /// </summary>
    /// <param name="seconds"></param>
    /// <param name="timeFormat"></param>
    /// <returns></returns>
    public static string GetHumanReadable(this int seconds, TimeFormat timeFormat = TimeFormat.DAYS) =>
        TimeSpan.FromSeconds(seconds).GetTimeFormat(timeFormat);



    /// <summary>
    /// <inheritdoc cref="UniversalExtensions.GetHumanReadable(int, TimeFormat)"/>
    /// </summary>
    /// <param name="seconds"></param>
    /// <param name="timeFormat"></param>
    /// <returns></returns>
    public static string GetHumanReadable(this long seconds, TimeFormat timeFormat = TimeFormat.DAYS) =>
        TimeSpan.FromSeconds(seconds).GetTimeFormat(timeFormat);



    /// <summary>
    /// Check if a string contains only digits
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static bool IsDigitsOnly(this string str)
    {
        foreach (char c in str)
        {
            if (c is < '0' or > '9')
                return false;
        }

        return true;
    }



    /// <summary>
    /// Get all digits from a string
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static string GetAllDigits(this string str) =>
        new(str.Where(Char.IsDigit).ToArray());
}

public static class StringExt
{
    /// <summary>
    /// Shorten a string to the given length
    /// </summary>
    /// <param name="value"></param>
    /// <param name="maxLength"></param>
    /// <returns></returns>
    public static string Truncate(this string value, int maxLength)
    {
        if (string.IsNullOrEmpty(value)) return value;
        return value.Length <= maxLength ? value : value[ ..maxLength ];
    }



    /// <summary>
    /// Shorten a string to the given length and add ".." at the end
    /// </summary>
    /// <param name="value"></param>
    /// <param name="maxLength"></param>
    /// <returns></returns>
    public static string TruncateWithIndication(this string value, int maxLength)
    {
        if (string.IsNullOrEmpty(value)) 
            return value;

        return value.Length <= maxLength ? value : $"{value[ ..maxLength ]}..";
    }



    /// <summary>
    /// Remove unsupported characters from string to generate a valid filename
    /// </summary>
    /// <param name="name">The string with potentionally unwanted characters</param>
    /// <param name="replace_char">The character the unwanted characters get replaced with (default: <code>_</code>)</param>
    /// <returns>A valid filename</returns>
    public static string MakeValidFileName(this string name, char replace_char = '_')
    {
        string invalidChars = System.Text.RegularExpressions.Regex.Escape(new string(System.IO.Path.GetInvalidFileNameChars()));
        string invalidRegStr = string.Format(@"([{0}]*\.+$)|([{0}]+)", invalidChars);

        return System.Text.RegularExpressions.Regex.Replace(name, invalidRegStr, replace_char.ToString()).Replace('&', replace_char);
    }



    /// <summary>
    /// Compute the SHA256-Hash for the given string
    /// </summary>
    /// <param name="filePath"></param>
    /// <returns></returns>
    public static string ComputeSHA256Hash(string str)
    {
        using SHA256 _SHA256 = SHA256.Create();
        return BitConverter.ToString(_SHA256.ComputeHash(Encoding.ASCII.GetBytes(str))).Replace("-", "").ToLowerInvariant();
    }
}