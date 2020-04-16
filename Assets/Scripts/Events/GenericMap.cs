using System.Collections.Generic;
using System;


/// <summary>
/// A wrapper around dictionary to support generic values.
/// </summary>
/// <typeparam name="TKey">Type parameter for the keys in the map</typeparam>
public class GenericMap<TKey>
{
    private readonly Dictionary<TKey, object> dict = new Dictionary<TKey, object>();

    /// <summary>
    /// Add a key-value pair to the map
    /// </summary>
    /// <param name="key">Key for this entry</param>
    /// <param name="value">Value for this entry</param>
    /// <typeparam name="TValue">Type parameter for value</typeparam>
    public void Add<TValue>(TKey key, TValue value)
    {
        dict.Add(key, value);
    }

    /// <summary>
    /// Try to find the value corresponding to the given key.
    /// </summary>
    /// <typeparam name="TValue">Type parameter for value. Fetched value will be cast to this type.</typeparam>
    /// <returns>True if a valid value was found</returns>
    public bool TryGetValue<TValue>(TKey key, out TValue value) where TValue : class
    {
        bool exists = dict.TryGetValue(key, out object val);
        value = val as TValue;
        return exists && (value != null);
    }

    /// <summary>
    /// Check if the map contains a value for the given key.
    /// </summary>
    /// <param name="key">Key to be queried</param>
    /// <returns>True if the key exists</returns>
    public bool ContainsKey(TKey key)
    {
        return dict.ContainsKey(key);
    }

    /// <summary>
    /// An unsafe way to get the value.false Might return null.
    /// </summary>
    /// <typeparam name="TValue">Type parameter for the value.</typeparam>
    [Obsolete("Use TryGetValue instead.")]
    public TValue GetValue<TValue>(TKey key) where TValue : class
    {
        return dict[key] as TValue;
    }

}