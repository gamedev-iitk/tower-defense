#if UNITY_EDITOR
using UnityEditor;

[CustomPropertyDrawer(typeof(TowerTypeBoolDictionary))]
public class TowerTypeBoolDictionaryPropertyDrawer : SerializableDictionaryPropertyDrawer { }
#endif