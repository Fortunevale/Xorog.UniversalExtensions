namespace Xorog.UniversalExtensions.Entities;

public class ScheduledTask
{
    internal ScheduledTask()
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
        ScheduledTaskExtensions.DeleteScheduledTask(Uid);
}
