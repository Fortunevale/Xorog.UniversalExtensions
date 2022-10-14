namespace Xorog.UniversalExtensions.Entities;

public class ObservableListUpdate<T>
{
    public List<T>? List { get; internal set; }

    public IReadOnlyList<T>? NewItems { get; internal set; }
    public IReadOnlyList<T>? RemovedItems { get; internal set; }
}
