using UnityEngine.Events;
using System.Collections.Generic;

/// <summary>
/// A class to orchestrate all events
/// </summary>
public static class EventRegistry
{
    private readonly static Dictionary<string, UnityEvent> registry = new Dictionary<string, UnityEvent>();

    /// <summary>
    /// Register a UnityEvent with a name.
    /// </summary>
    /// <param name="name">String to store as key for this event</param>
    /// <param name="unityEvent">(Optional) Event to be stored. A new one is created if not supplied</param>
    /// <returns>True if the event was successfully registered</returns>
    public static bool RegisterEvent(string name, UnityEvent unityEvent = default)
    {
        if (!registry.ContainsKey(name)) {
            if (unityEvent == default)
            {
                unityEvent = new UnityEvent();
            }
            registry.Add(name, unityEvent);
            return true;
        }
        else
        {
            return false;
        }
    }

    /// <summary>
    /// Register a callback delegate for an event.
    /// </summary>
    /// <param name="name">Identifier for the event to register this callback with</param>
    /// <param name="callback">Callback to register</param>
    /// <returns>True if the callback was successfully registered</returns>
    public static bool RegisterAction(string name, UnityAction callback)
    {
        if (registry.ContainsKey(name)) {
            registry.TryGetValue(name, out UnityEvent ev);
            ev?.AddListener(callback);
            return true;
        }
        else
        {
            return false;
        }
    }

    /// <summary>
    /// Invokes the event with the given name.
    /// </summary>
    /// <param name="name">Name of the event to be invoked</param>
    /// <returns>True if the event was successfully invoked</returns>
    public static bool Invoke(string name)
    {
        if (registry.ContainsKey(name)) {
            registry.TryGetValue(name, out UnityEvent ev);
            ev?.Invoke();
            return true;
        }
        else
        {
            return false;
        }
    }
}
