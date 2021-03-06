using UnityEngine;

/// <summary>
/// Enum for tower types. This is important to refer to towers in a safe way.
/// </summary>
public enum ETowerType : ushort
{
    Base,
    Gold,
    Red,
    Green,
    None
}

/// <summary>
/// Utils and extensions for the ETowerType enum.
/// </summary>
static class ETowerTypeUtils
{
    /// <summary>
    /// Returns the string representation for the given tower type enum.
    /// </summary>
    /// <param name="type">Enum to get the string for</param>
    /// <returns>String for that type.</returns>
    public static string GetString(this ETowerType type)
    {
        switch (type)
        {
            case ETowerType.Base:
                return "base";
            case ETowerType.Gold:
                return "gold";
            case ETowerType.Green:
                return "green";
            case ETowerType.Red:
                return "red";
            default:
                {
                    Debug.LogError("Invalid tower type: " + type);
                    return null;
                }
        }
    }

    /// <summary>
    /// Returns an enum corresponding to the given string.
    /// </summary>
    /// <param name="type">Type of the tower as string.</param>
    /// <returns>Enum for that type.</returns>
    public static ETowerType GetTowerType(string type)
    {
        type = type.ToLower();
        switch (type)
        {
            case "base":
                return ETowerType.Base;
            case "gold":
                return ETowerType.Gold;
            case "green":
                return ETowerType.Green;
            case "red":
                return ETowerType.Red;
            default:
                {
                    Debug.LogError("Invalid tower type string: " + type);
                    return ETowerType.None;
                }
        }
    }

    /// <summary>
    /// Get the cost of upgrade for the tower.
    /// </summary>
    /// <param name="type">The type of tower to upgrade to.</param>
    /// <returns>Integer cost for that tower.</returns>
    public static int GetUpgradeCost(this ETowerType type)
    {
        switch (type)
        {
            case ETowerType.Base:
                return 100;
            case ETowerType.Gold:
                return 200;
            case ETowerType.Green:
                return 200;
            case ETowerType.Red:
                return 300;
            default:
                {
                    Debug.LogError("Invalid tower type: " + type);
                    return 0;
                }
        }
    }

    /// <summary>
    /// Get the cost for moving the tower.
    /// </summary>
    /// <param name="type">The type of tower to move.</param>
    /// <returns>Integer cost for the movement of that tower.</returns>
    public static int GetMoveCost(this ETowerType type)
    {
        switch (type)
        {
            case ETowerType.Base:
                return 50;
            case ETowerType.Gold:
                return 75;
            case ETowerType.Green:
                return 75;
            case ETowerType.Red:
                return 100;
            default:
                {
                    Debug.LogError("Invalid tower type: " + type);
                    return 0;
                }
        }
    }
}
