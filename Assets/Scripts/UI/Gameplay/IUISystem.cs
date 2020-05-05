using UnityEngine;

/// <summary>
/// Exposes basic UI functionality. All MenuUISystems should implement this interface.
/// </summary>
public interface IUISystem
{
    /// <summary>
    /// Show the UI screen with values from the provided object.
    /// </summary>
    /// <param name="obj">GameObject used to populate the layer</param>
    void Show(GameObject obj);

    /// <summary>
    /// Hides the UI screen.
    /// </summary>
    void Hide();
}