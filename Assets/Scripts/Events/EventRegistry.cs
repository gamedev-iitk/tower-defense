using UnityEngine.Events;
using UnityEngine;


/// <summary>
/// A class to orchestrate all events
/// </summary>
public static class EventRegistry
{
    private readonly static GenericMap<string> container = new GenericMap<string>();

    #region events
    /// <summary>
    /// Register an event with no parameters.
    /// </summary>
    /// <param name="name"></param>
    /// <param name="unityEvent"></param>
    /// <returns></returns>
    public static bool RegisterEvent(string name, TDEvent unityEvent)
    {
        if (!container.ContainsKey(name))
        {
            container.Add(name, unityEvent);
            return true;
        }
        else
        {
            return false;
        }
    }

    /// <summary>
    /// Register an event with one parameter
    /// </summary>
    /// <param name="name"></param>
    /// <param name="unityEvent"></param>
    /// <typeparam name="TParam"></typeparam>
    /// <returns></returns>
    public static bool RegisterEvent<TParam>(string name, TDEvent<TParam> unityEvent)
    {
        if (!container.ContainsKey(name))
        {
            container.Add(name, unityEvent);
            return true;
        }
        else
        {
            return false;
        }
    }

    /// <summary>
    /// Register an event with two parameters
    /// </summary>
    /// <param name="name"></param>
    /// <param name="unityEvent"></param>
    /// <typeparam name="TParam1"></typeparam>
    /// <typeparam name="TParam2"></typeparam>
    /// <returns></returns>
    public static bool RegisterEvent<TParam1, TParam2>(string name, TDEvent<TParam1, TParam2> unityEvent)
    {
        if (!container.ContainsKey(name))
        {
            container.Add(name, unityEvent);
            return true;
        }
        else
        {
            return false;
        }
    }
    #endregion


    #region actions
    /// <summary>
    /// Register an action with no parameters
    /// </summary>
    /// <param name="name"></param>
    /// <param name="callback"></param>
    /// <returns></returns>
    public static bool RegisterAction(string name, UnityAction callback)
    {
        if (container.TryGetValue(name, out TDEvent value))
        {
            value?.AddListener(callback);
            return true;
        }
        else
        {
            Debug.LogError("Failed to register callback. Name name supplied does not have an associated event or type argument mismatch.");
            return false;
        }
    }

    /// <summary>
    /// Register an action with one parameter
    /// </summary>
    /// <param name="name"></param>
    /// <param name="callback"></param>
    /// <typeparam name="TParam"></typeparam>
    /// <returns></returns>
    public static bool RegisterAction<TParam>(string name, UnityAction<TParam> callback)
    {
        if (container.TryGetValue(name, out TDEvent<TParam> value))
        {
            value?.AddListener(callback);
            return true;
        }
        else
        {
            Debug.LogError("Failed to register callback. Name name supplied does not have an associated event or type argument mismatch.");
            return false;
        }
    }

    /// <summary>
    /// Register an action with two parameters
    /// </summary>
    /// <param name="name"></param>
    /// <param name="callback"></param>
    /// <typeparam name="TParam1"></typeparam>
    /// <typeparam name="TParam2"></typeparam>
    /// <returns></returns>
    public static bool RegisterAction<TParam1, TParam2>(string name, UnityAction<TParam1, TParam2> callback)
    {
        if (container.TryGetValue(name, out TDEvent<TParam1, TParam2> value))
        {
            value?.AddListener(callback);
            return true;
        }
        else
        {
            Debug.LogError("Failed to register callback. Name name supplied does not have an associated event or type argument mismatch.");
            return false;
        }
    }
    #endregion

}
