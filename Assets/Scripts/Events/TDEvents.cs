using UnityEngine.Events;

/// <summary>
/// An event that takes callbacks with no parameters
/// </summary>
public class TDEvent : UnityEvent { }

/// <summary>
/// An event that takes callbacks with one parameter
/// </summary>
/// <typeparam name="TParam">Type of the parameter</typeparam>
public class TDEvent<TParam> : UnityEvent<TParam> { }

/// <summary>
/// An event that takes callbacks with two parameters
/// </summary>
/// <typeparam name="TParam1">Type of the first parameter</typeparam>
/// <typeparam name="TParam2">Type of the second parameter</typeparam>
public class TDEvent<TParam1, TParam2> : UnityEvent<TParam1, TParam2> { }

/// <summary>
/// An event that takes callbacks with two parameters
/// </summary>
/// <typeparam name="TParam1">Type of the first parameter</typeparam>
/// <typeparam name="TParam2">Type of the second parameter</typeparam>
/// <typeparam name="TParam3">Type of the third parameter</typeparam>
public class TDEvent<TParam1, TParam2, TParam3> : UnityEvent<TParam1, TParam2, TParam3> { }