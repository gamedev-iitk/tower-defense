using System.Collections.Generic;
using System;

public class TDEventContainer<TKey>
{
    private readonly Dictionary<TKey, object> dict = new Dictionary<TKey, object>();

    public void Add<TValue>(TKey key, TValue value)
    {
        dict.Add(key, value);
    }

    public bool TryGetValue<TValue>(TKey key, out TValue value) where TValue : class
    {
        bool exists = dict.TryGetValue(key, out object val);
        value = val as TValue;
        return exists && (value != null);
    }

    public bool ContainsKey(TKey key)
    {
        return dict.ContainsKey(key);
    }

    [Obsolete("Use TryGetValue instead.")]
    public TValue GetValue<TValue>(TKey key) where TValue : class
    {
        return dict[key] as TValue;
    }

}