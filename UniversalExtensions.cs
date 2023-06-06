﻿using System.Reflection;

namespace Xorog.UniversalExtensions;

public static class UniversalExtensions
{
    public static void LoadAllReferencedAssemblies(AppDomain domain)
    {
        _logger?.LogDebug("Loading all assemblies..");

        var assemblyCount = 0;
        foreach (Assembly assembly in domain.GetAssemblies())
        {
            LoadReferencedAssembly(assembly);
        }

        void LoadReferencedAssembly(Assembly assembly)
        {
            try
            {
                foreach (AssemblyName name in assembly.GetReferencedAssemblies())
                {
                    if (!AppDomain.CurrentDomain.GetAssemblies().Any(a => a.FullName == name.FullName))
                    {
                        assemblyCount++;
                        _logger?.LogDebug("Loading {Name}..", name.Name);
                        LoadReferencedAssembly(Assembly.Load(name));
                    }
                }
            }
            catch (Exception ex)
            {
                _logger?.LogError("Failed to load an assembly", ex);
            }
        }

        _logger?.LogInformation("Loaded {assemblyCount} assemblies.", assemblyCount);
    }

    /// <summary>
    /// Attaches a logger to UniversalExtensions. Used for Debug purposes.
    /// </summary>
    /// <param name="logger"></param>
    public static void AttachLogger(ILogger logger)
    {
        _logger = logger;
    }


    /// <summary>
    /// Extensions for string.IsNullOrWhiteSpace
    /// </summary>
    /// <param name="str"></param>
    /// <returns>Whether the string is null, empty or only contains whitespaces</returns>
    public static bool IsNullOrWhiteSpace(this string str) => string.IsNullOrWhiteSpace(str);



    /// <summary>
    /// Extensions for string.IsNullOrEmpty
    /// </summary>
    /// <param name="str"></param>
    /// <returns>Whether the string is null or empty</returns>
    public static bool IsNullOrEmpty(this string str) => string.IsNullOrEmpty(str);



    /// <summary>
    /// Select a random item from a list
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="obj"></param>
    /// <returns>The randomly selected item</returns>
    /// <exception cref="ArgumentNullException">The list is null</exception>
    /// <exception cref="ArgumentException">The list is empty</exception>
    public static T SelectRandom<T>(this IEnumerable<T> obj)
    {
        if (obj == null)
        {
            throw new ArgumentNullException();
        }

        if (!obj.Any())
        {
            throw new ArgumentException("The sequence is empty.");
        }


        int rng = new Random().Next(0, obj.Count());
        return obj.ElementAt(rng) ?? throw new ArgumentNullException();
    }



    /// <summary>
    /// Check whether a list contains elements and is not null
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="obj"></param>
    /// <returns>Whether the list contains elements and is not null</returns>
    public static bool IsNotNullAndNotEmpty<T>(this IEnumerable<T> obj) => obj is not null && obj.Any();



    /// <summary>
    /// Get the current CPU Usage on all platforms
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
    public static async Task<string> UnshortenUrl(string url, bool UseHeadMethod = true)
    {
        _logger?.LogDebug("Unshortening Url '{Url}', using head method: {UseHeadMethod}", url, UseHeadMethod);

        HttpClient client = new(new HttpClientHandler()
        {
            AllowAutoRedirect = false,
            AutomaticDecompression = DecompressionMethods.GZip,

        });
        client.Timeout = TimeSpan.FromSeconds(60);
        client.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/98.0.4758.82 Safari/537.36");
        client.DefaultRequestHeaders.Add("upgrade-insecure-requests", "1");
        client.DefaultRequestHeaders.Add("accept-encoding", "gzip, deflate, br");
        client.DefaultRequestHeaders.Add("accept-language", "en-US,en;q=0.9");
        client.MaxResponseContentBufferSize = 4096;

        HttpRequestMessage requestMessage = new HttpRequestMessage((UseHeadMethod ? HttpMethod.Head : HttpMethod.Get), url);

        CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
        var request_task = client.SendAsync(requestMessage, cancellationTokenSource.Token);

        try
        {
            await request_task.WaitAsync(TimeSpan.FromSeconds(3));
        }
        catch (Exception)
        {
            if (UseHeadMethod)
                return await UnshortenUrl(url, false);

            throw;
        }

        if (!request_task.IsCompleted)
            cancellationTokenSource.Cancel();

        if (UseHeadMethod && request_task.IsFaulted && request_task.Exception.InnerException.GetType() == typeof(HttpRequestException))
        {
            _logger?.LogWarning("Unshortening Url '{Url}' failed, falling back to non-head method", url);
            return await UnshortenUrl(url, false);
        }

        var statuscode = request_task.Result.StatusCode;
        var header = request_task.Result.Headers;

        if (UseHeadMethod && statuscode is HttpStatusCode.NotFound or HttpStatusCode.InternalServerError)
        {
            _logger?.LogWarning("Unshortening Url '{Url}' failed, falling back to non-head method", url);
            return await UnshortenUrl(url, false);
        }

        if (statuscode is HttpStatusCode.Found
            or HttpStatusCode.Redirect
            or HttpStatusCode.SeeOther
            or HttpStatusCode.RedirectKeepVerb
            or HttpStatusCode.RedirectMethod
            or HttpStatusCode.PermanentRedirect
            or HttpStatusCode.TemporaryRedirect)
        {
            if (header is not null && header.Location is not null)
                return await UnshortenUrl(header.Location.AbsoluteUri);
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
    /// Runs a long non-blocking delay, a work-around for Task.Delay only supporting Int32
    /// </summary>
    /// <param name="delay">A timespan of how long the delay should last</param>
    /// <param name="token">A cancellation token source to cancel the action</param>
    /// <returns></returns>
    internal static async Task LongDelay(TimeSpan delay, CancellationTokenSource token)
    {
        var st = new Stopwatch();
        st.Start();
        while (true && !token.IsCancellationRequested)
        {
            var remaining = (delay - st.Elapsed).TotalMilliseconds;
            if (remaining <= 0)
                break;
            if (remaining > Int16.MaxValue)
                remaining = Int16.MaxValue;
            await Task.Delay(TimeSpan.FromMilliseconds(remaining), token.Token);
        }
    }



    /// <summary>
    /// Create a scheduled task
    /// </summary>
    /// <param name="task">The task to run</param>
    /// <param name="runTime">The time to run the task</param>
    /// <param name="customData">Any custom data you wish to provide.</param>
    /// <returns>An unique identifier of the task</returns>

    public static string CreateScheduledTask(this Task task, DateTime runTime, object? customData = null)
    {
        string UID = Guid.NewGuid().ToString();
        CancellationTokenSource CancellationToken = new CancellationTokenSource();

        if (Math.Ceiling(runTime.GetTimespanUntil().TotalMilliseconds) < 0)
            runTime = DateTime.UtcNow.AddSeconds(1);

        _ = LongDelay(runTime.GetTimespanUntil(), CancellationToken).ContinueWith(x =>
        {
            lock (RegisteredScheduledTasks)
            {
                RegisteredScheduledTasks.Remove(UID); 
            }

            _logger?.LogDebug("Running scheduled task with UID '{UID}'", UID, runTime.GetTimespanUntil().GetHumanReadable());

            if (x.IsCompletedSuccessfully)
                task.Start();
        });

        lock (RegisteredScheduledTasks)
        {
            _logger?.LogDebug("Creating scheduled task with UID '{UID}' running in {RunTime}", UID, runTime.GetTimespanUntil().GetHumanReadable());

            RegisteredScheduledTasks.Add(UID, new ScheduledTask
            {
                Uid = UID,
                RunTime = runTime,
                TokenSource = CancellationToken,
                CustomData = customData,
            });
        }
        return UID;
    }



    /// <summary>
    /// Deletes a scheduled task
    /// </summary>
    /// <param name="UID">The task's unique identifier</param>
    /// <exception cref="KeyNotFoundException">Throws if the task hasn't been found or if an internal error occurred</exception>
    public static void DeleteScheduledTask(string UID)
    {
        if (!RegisteredScheduledTasks.ContainsKey(UID))
            throw new KeyNotFoundException($"No scheduled task has been found with UID '{UID}'");

        if (RegisteredScheduledTasks[UID].TokenSource is null)
            throw new Exception($"Internal: There is no token source registered the specified task.");

        _logger?.LogDebug("Deleting scheduled task with UID '{UID}'", UID);

        lock (RegisteredScheduledTasks)
        {
            RegisteredScheduledTasks[UID].TokenSource?.Cancel();
            RegisteredScheduledTasks.Remove(UID); 
        }
        return;
    }



    /// <summary>
    /// Gets a list of all registered tasks
    /// </summary>
    /// <returns>A list of all registered tasks</returns>
    public static IReadOnlyList<ScheduledTask>? GetScheduledTasks() 
        => RegisteredScheduledTasks.Select(x => x.Value).ToList().AsReadOnly();

    /// <summary>
    /// Gets a specific task
    /// </summary>
    /// <param name="UID">The unique identifier of what task to get</param>
    /// <returns>The task</returns>
    /// <exception cref="Exception">Throws if the task has not been found</exception>
    public static ScheduledTask GetScheduledTask(string UID) 
        => RegisteredScheduledTasks[UID];



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



    /// <summary>
    /// Compute the SHA256-Hash for a given file
    /// </summary>
    /// <param name="filePath"></param>
    /// <returns></returns>
    public static string ComputeSHA256Hash(FileInfo filePath)
    {
        using SHA256 _SHA256 = SHA256.Create();
        using FileStream fileStream = filePath.OpenRead();
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

    public static string GetShortHumanReadable(this TimeSpan timespan, TimeFormat timeFormat = TimeFormat.DAYS) =>
        timespan.GetShortTimeFormat(timeFormat);



    /// <summary>
    /// Get a human readable string for the given amount of time.
    /// </summary>
    /// <param name="seconds"></param>
    /// <param name="timeFormat"></param>
    /// <returns></returns>
    public static string GetHumanReadable(this int seconds, TimeFormat timeFormat = TimeFormat.DAYS, HumanReadableTimeFormatConfig? config = null) =>
        TimeSpan.FromSeconds(seconds).GetTimeFormat(timeFormat,config);



    /// <inheritdoc cref="UniversalExtensions.GetHumanReadable(int, TimeFormat)"/>
    public static string GetHumanReadable(this long seconds, TimeFormat timeFormat = TimeFormat.DAYS, HumanReadableTimeFormatConfig? config = null) =>
        TimeSpan.FromSeconds(seconds).GetTimeFormat(timeFormat, config);

    /// <inheritdoc cref="UniversalExtensions.GetHumanReadable(int, TimeFormat)"/>
    public static string GetHumanReadable(this TimeSpan timeSpan, TimeFormat timeFormat = TimeFormat.DAYS, HumanReadableTimeFormatConfig? config = null) =>
        timeSpan.GetTimeFormat(timeFormat, config);



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



    /// <summary>
    /// Get country flag emoji based on Iso Country Code
    /// </summary>
    /// <param name="country"></param>
    /// <returns></returns>
    public static string IsoCountryCodeToFlagEmoji(this string country)
    {
        return string.Concat(country.ToUpper().Select(x => char.ConvertFromUtf32(x + 0x1F1A5)));
    }


    /// <summary>
    /// Get closest Color to given Color
    /// </summary>
    /// <param name="colorArray"></param>
    /// <param name="baseColor"></param>
    /// <returns></returns>
    public static Color GetClosestColor(List<Color> colorArray, Color baseColor)
    {
        var colors = colorArray.Select(x => new { Value = x, Diff = Internal.GetDiff(x, baseColor) }).ToList();
        var min = colors.Min(x => x.Diff);
        return colors.Find(x => x.Diff == min).Value;
    }


    /// <summary>
    /// Convert Hex to Color
    /// </summary>
    /// <returns>The converted color</returns>
    public static Color ToColor(this string str)
    {
        return ColorTranslator.FromHtml(str);
    }


    /// <summary>
    /// Convert RGB Value to Hex
    /// </summary>
    /// <param name="R">Red</param>
    /// <param name="G">Green</param>
    /// <param name="B">Blue</param>
    /// <returns>A string that represents the color in hex (e.g. 255, 0, 0 -> #FF0000)</returns>
    public static string ToHex(int R, int G, int B)
    {
        return "#" + R.ToString("X2") + G.ToString("X2") + B.ToString("X2");
    }



    /// <summary>
    /// Calculates the percentage of the given 2 values.
    /// </summary>
    /// <param name="current">The current value.</param>
    /// <param name="max">The maximum value.</param>
    /// <returns>The percentage.</returns>
    /// <exception cref="ArgumentException"></exception>
    public static int CalculatePercentage(double current, double max)
    {
        if (max == 0)
            throw new ArgumentException("Max cannot be zero.");

        double percentage = (current / max) * 100;
        return Convert.ToInt32(percentage);
    }



    /// <summary>
    /// Shorten a string to the given length
    /// </summary>
    /// <param name="value"></param>
    /// <param name="maxLength"></param>
    /// <returns></returns>
    public static string Truncate(this string value, int maxLength)
    {
        if (string.IsNullOrEmpty(value))
            return value;
        return value.Length <= maxLength ? value : value[..maxLength];
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

        return value.Length <= maxLength ? value : $"{value[..(maxLength - 2)]}..";
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
    /// Changes the first letter to upper.
    /// </summary>
    /// <param name="str">The string to modify.</param>
    /// <returns>The string with the first letter changed to upper.</returns>
    public static string FirstLetterToUpper(this string str)
    {
        return $"{str.First().ToString().ToUpper()}{str.Remove(0, 1)}";
    }
}