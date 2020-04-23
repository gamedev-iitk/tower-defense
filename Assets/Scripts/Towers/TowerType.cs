using UnityEngine;

/// <summary>
/// A component that can be attached to buttons to check what tower they are associated with in a safe way with an enum
/// </summary>
public class TowerType : MonoBehaviour
{
    public ETowerType Type;
}

public enum ETowerType : ushort
{
    Base,
    Gold,
    Red,
    Green
}
