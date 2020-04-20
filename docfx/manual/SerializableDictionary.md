# Usage Guide for the Serializable Dictionary

We've used azixMcAze's implementation: https://github.com/azixMcAze/Unity-SerializableDictionary

**Instructions**
1. Subclass `SerializableDictionary`. Generic classes won't be serialized.
```cs
[System.Serializable]
public class StringStringDictionary : SerializableDictionary<string, string>  { }
```
1. Subclass `SerializableDictionaryPropertyDrawer` in the Editor folder. Add the `CustomPropertyDrawer` attribute.
```cs
[UnityEditor.CustomPropertyDrawer(typeof(StringStringDictionary))]
public class StringStringPropertyDrawer : SerializableDictionaryPropertyDrawer { }
```