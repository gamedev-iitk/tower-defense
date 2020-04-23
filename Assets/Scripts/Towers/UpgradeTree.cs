using UnityEngine;
using System;


/// <summary>
/// This component stores information about possible upgrades in the upgrade tree.
/// </summary>
public class UpgradeTree : MonoBehaviour
{
    public TowerTypeBoolDictionary valuePairs;
}

[Serializable]
public class TowerTypeBoolDictionary : SerializableDictionary<ETowerType, bool> { }
