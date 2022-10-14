using System.Collections;
using System.Diagnostics.CodeAnalysis;

namespace Xorog.UniversalExtensions.Entities;

public class ObservableDictionary<TKey, TValue> : IDictionary<TKey, TValue>
{
    private Dictionary<TKey, TValue> _items = new();

    public event EventHandler<ObservableListUpdate<KeyValuePair<TKey, TValue>>> ItemsChanged = delegate { };

    public TValue this[TKey key] { get => _items[key]; set => _items[key] = value; }

    public ICollection<TKey> Keys => _items.Keys;

    public ICollection<TValue> Values => _items.Values;

    public int Count => _items.Count;

    public bool IsReadOnly => false;

    public void Add(TKey key, TValue value)
    {
        _items.Add(key, value);

        _ = Task.Run(() =>
        {
            this.ItemsChanged.Invoke(null, new()
            {
                List = this.ToList(),
                NewItems = new List<KeyValuePair<TKey, TValue>>() { new(key, value) },
                RemovedItems = null
            });
        });
    }

    public void Add(KeyValuePair<TKey, TValue> item)
    {
        _items.Add(item.Key, item.Value);

        _ = Task.Run(() =>
        {
            this.ItemsChanged.Invoke(null, new()
            {
                List = this.ToList(),
                NewItems = new List<KeyValuePair<TKey, TValue>>() { new(item.Key, item.Value) },
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
            this.ItemsChanged.Invoke(null, new()
            {
                List = this.ToList(),
                NewItems = null,
                RemovedItems = oldList
            });
        });
    }

    public bool Remove(TKey key)
    {
        var old = _items[key];
        var v = _items.Remove(key);

        _ = Task.Run(() =>
        {
            this.ItemsChanged.Invoke(null, new()
            {
                List = this.ToList(),
                NewItems = null,
                RemovedItems = new List<KeyValuePair<TKey, TValue>>() { new(key, old) },
            });
        });

        return v;
    }

    public bool Remove(KeyValuePair<TKey, TValue> item)
    {
        var old = _items[item.Key];
        var v = _items.Remove(item.Key);

        _ = Task.Run(() =>
        {
            this.ItemsChanged.Invoke(null, new()
            {
                List = this.ToList(),
                NewItems = null,
                RemovedItems = new List<KeyValuePair<TKey, TValue>>() { new(item.Key, old) },
            });
        });
        return v;
    }

    public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
    {
        List<KeyValuePair<TKey, TValue>> newArrayBuild = new();

        for (var i = 0; i < array.Length; i++)
        {
            if (i == arrayIndex)
                newArrayBuild.AddRange(this._items);

            KeyValuePair<TKey, TValue> b = array[i];
            newArrayBuild.Add(b);
        }

        array = newArrayBuild.ToArray();
    }

    public bool Contains(KeyValuePair<TKey, TValue> item)
        => _items.Contains(item);

    public bool ContainsKey(TKey key)
        => _items.ContainsKey(key);

    public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        => _items.GetEnumerator();

    public bool TryGetValue(TKey key, [MaybeNullWhen(false)] out TValue value)
        => _items.TryGetValue(key, out value);

    IEnumerator IEnumerable.GetEnumerator()
        => _items.GetEnumerator();
}
