using System.Collections;

namespace Xorog.UniversalExtensions.Entities;

public class ObservableList<T> : IList<T>
{
    private List<T> _items = new();

    public event EventHandler<ObservableListUpdate<T>>? ItemsChanged;

    public T this[int index] { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

    public int Count => _items.Count;

    public bool IsReadOnly => false;

    public void Add(T item)
    {
        _ = Task.Run(() =>
        {
            this.ItemsChanged?.Invoke(null, new ObservableListUpdate<T>
            {
               List = this,
               NewItems = new List<T>() { item },
               RemovedItems = null
            });
        });

        _items.Add(item);
    }

    public void Insert(int index, T item)
    {
        _ = Task.Run(() =>
        {
            this.ItemsChanged?.Invoke(null, new ObservableListUpdate<T>
            {
                List = this,
                NewItems = new List<T>() { item },
                RemovedItems = null
            });
        });

        _items.Insert(index, item);
    }

    public void Clear()
    {
        _ = Task.Run(() =>
        {
            this.ItemsChanged?.Invoke(null, new ObservableListUpdate<T>
            {
                List = this,
                NewItems = null,
                RemovedItems = _items.ToList()
            });
        });

        _items.Clear();
    }

    public void RemoveAt(int index)
    {
        _ = Task.Run(() =>
        {
            this.ItemsChanged?.Invoke(null, new ObservableListUpdate<T>
            {
                List = this,
                NewItems = null,
                RemovedItems = new List<T>() { _items.ElementAt(index) }
            });
        });

        _items.RemoveAt(index);
    }

    public bool Remove(T item)
    {
        _ = Task.Run(() =>
        {
            this.ItemsChanged?.Invoke(null, new ObservableListUpdate<T>
            {
                List = this,
                NewItems = null,
                RemovedItems = new List<T>() { item }
            });
        });

        return _items.Remove(item);
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
