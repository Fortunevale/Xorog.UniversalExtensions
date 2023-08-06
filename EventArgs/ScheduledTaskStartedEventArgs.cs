using Xorog.UniversalExtensions.Entities;

namespace Xorog.UniversalExtensions.EventArgs;

public class ScheduledTaskStartedEventArgs : System.EventArgs
{
    internal ScheduledTaskStartedEventArgs(ScheduledTask details, Task task)
    {
        this.Details = details;
        this.Task = task;
    }

    /// <summary>
    /// The details of this scheduled task.
    /// </summary>
    public ScheduledTask Details { get; internal set; }

    /// <summary>
    /// The task that was executed.
    /// </summary>
    public Task Task { get; internal set; }
}
