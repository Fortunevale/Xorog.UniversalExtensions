﻿namespace Xorog.UniversalExtensions;
public static class ScheduledTaskExtensions
{
    /// <summary>
    /// Create a scheduled task
    /// </summary>
    /// <param name="task">The task to run</param>
    /// <param name="runTime">The time to run the task</param>
    /// <param name="customData">Any custom data you wish to provide.</param>
    /// <returns>An unique identifier of the task</returns>

    public static string CreateScheduledTask(this Task task, DateTime runTime, object? customData = null)
    {
        if (task.Status != TaskStatus.Created)
            throw new InvalidOperationException("The task is already being executed or has been scheduled for execution.")
                .AttachData("Task", task)
                .AttachData("RunTime", runTime)
                .AttachData("CustomData", customData);

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
}
