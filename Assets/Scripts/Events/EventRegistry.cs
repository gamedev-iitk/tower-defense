using UnityEngine.Events;
using UnityEngine;


/// <summary>
/// A class to orchestrate all events
/// </summary>
public static class EventRegistry
{
    private readonly static GenericMap<string> container = new GenericMap<string>();

    /// <summary>
    /// Gets you the event associated with the key. Creates one if no event found.
    /// </summary>
    /// <param name="name">Key for event lookup</param>
    /// <returns>TDEvent corresponding to the string</returns>
    public static TDEvent GetEvent(string name)
    {
        TDEvent ev;
        if (container.ContainsKey(name))
        {
            container.TryGetValue(name, out ev);
        }
        else
        {
            Debug.LogWarning("No event found. Creating a new event.");
            ev = new TDEvent();
            container.Add(name, ev);
        }
        return ev;
    }

    public static TDEvent<TParam> GetEvent<TParam>(string name)
    {
        TDEvent<TParam> ev;
        if (container.ContainsKey(name))
        {
            container.TryGetValue(name, out ev);
        }
        else
        {
            Debug.LogWarning("No event found. Creating a new event.");
            ev = new TDEvent<TParam>();
            container.Add(name, ev);
        }
        return ev;
    }

    public static TDEvent<TParam1, TParam2> GetEvent<TParam1, TParam2>(string name)
    {
        TDEvent<TParam1, TParam2> ev;
        if (container.ContainsKey(name))
        {
            container.TryGetValue(name, out ev);
        }
        else
        {
            ev = new TDEvent<TParam1, TParam2>();
            container.Add(name, ev);
        }
        return ev;
    }

    /// <summary>
    /// Registers a zero argument callback for an event. Creates the event if it doesn't exist.
    /// </summary>
    /// <param name="name">Name of the event</param>
    /// <param name="callback">Callback to register</param>
    /// <returns>True if successfully registered</returns>
    public static bool RegisterAction(string name, UnityAction callback)
    {
        TDEvent ev = GetEvent(name);
        if (ev == null)
        {
            Debug.LogError("Failed to register callback. Type mismatch.");
            return false;
        }
        else
        {
            ev?.AddListener(callback);
            return true;
        }
    }

    public static bool RegisterAction<TParam>(string name, UnityAction<TParam> callback)
    {
        TDEvent<TParam> ev = GetEvent<TParam>(name);
        if (ev == null)
        {
            Debug.LogError("Failed to register callback. Type mismatch.");
            return false;
        }
        else
        {
            ev?.AddListener(callback);
            return true;
        }
    }

    public static bool RegisterAction<TParam1, TParam2>(string name, UnityAction<TParam1, TParam2> callback)
    {
        TDEvent<TParam1, TParam2> ev = GetEvent<TParam1, TParam2>(name);
        if (ev == null)
        {
            Debug.LogError("Failed to register callback. Type mismatch.");
            return false;
        }
        else
        {
            ev?.AddListener(callback);
            return true;
        }
    }

    public static void Invoke<TParam1, TParam2>(string name, TParam1 param1, TParam2 param2)
    {
        container.TryGetValue(name, out TDEvent<TParam1, TParam2> ev);
        if (ev == null)
        {
            Debug.LogError("No callback registered for the event " + name + " that supports the supplied parameters. Cannot invoke.");
            return;
        }
        else
        {
            ev.Invoke(param1, param2);
        }
    }
}
