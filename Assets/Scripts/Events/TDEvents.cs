using System;
using UnityEngine.Events;

[Serializable]
public class TDEvent : UnityEvent {}

[Serializable]
public class TDEvent<TParam> : UnityEvent<TParam> {}

[Serializable]
public class TDEvent<TParam1, TParam2> : UnityEvent<TParam1, TParam2> {}
