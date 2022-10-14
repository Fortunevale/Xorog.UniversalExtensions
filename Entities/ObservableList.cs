using System.Collections;

namespace Xorog.UniversalExtensions.Entities;

public class ObservableList<T> : IList<T>
{
    private List<T> _items = new();

    public event EventHandler<ObservableListUpdate<T>> ItemsChanged = delegate { };

    public T this[int index] { get => _items[index]; set { _items[index] = value; } }

    public int Count => _items.Count;

    public bool IsReadOnly => false;

    public void Add(T item)
    {
        _items.Add(item);

        _ = Task.Run(() =>
        {
            this.ItemsChanged.Invoke(null, new ObservableListUpdate<T>
            {
                List = this.ToList(),
                NewItems = new List<T>() { item },
                RemovedItems = null
            });
        });
    }

    public void Insert(int index, T item)
    {
        _items.Insert(index, item);

        _ = Task.Run(() =>
        {
            this.ItemsChanged?.Invoke(null, new ObservableListUpdate<T>
            {
                List = this.ToList(),
                NewItems = new List<T>() { item },
                RemovedItems = null
            });
        });
    }

    public void Clear()
    {
        var oldList = _items.ToList();
        _items.Clear();

        _ = Task.Run(() =>
        {
            this.ItemsChanged?.Invoke(null, new ObservableListUpdate<T>
            {
                List = this.ToList(),
                NewItems = null,
                RemovedItems = oldList
            });
        });
    }

    public void RemoveAt(int index)
    {
        ObservableListUpdate<T> e = new ObservableListUpdate<T>
        {
            List = this.ToList(),
            NewItems = null,
            RemovedItems = new List<T>() { _items.ElementAt(index) }
        };

        _items.RemoveAt(index);

        _ = Task.Run(() =>
        {
            this.ItemsChanged?.Invoke(null, e);
        });
    }

    public bool Remove(T item)
    {
        var v = _items.Remove(item);
        
        _ = Task.Run(() =>
        {
            this.ItemsChanged?.Invoke(null, new ObservableListUpdate<T>
            {
                List = this.ToList(),
                NewItems = null,
                RemovedItems = new List<T>() { item }
            });
        });

        return v;
    }

    public bool Contains(T item)
        => _items.Contains(item);

    public void CopyTo(T[] array, int arrayIndex)
        => _items.CopyTo(array, arrayIndex);

    public IEnumerator<T> GetEnumerator()
        => _items.GetEnumerator();

    public int IndexOf(T item)
        => _items.IndexOf(item);

    IEnumerator IEnumerable.GetEnumerator() 
        => _items.GetEnumerator();
}
