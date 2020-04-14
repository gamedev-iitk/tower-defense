using UnityEngine;

/// <summary>
/// Exposes basic UI functionality. All MenuUISystems should implement this
/// interface.
/// </summary>
public interface IUISystem {
  /// <summary>
  /// "Create" a new UI screen, i.e. repopulate the canvas layer with details
  /// from the given object.
  /// </summary>
  /// <param name="obj"><c>GameObject </c> used to populate the layer</param>
  /// <returns>Boolean. True if repopulated.</returns>
  bool Create(GameObject obj);

  /// <summary>
  /// Hides the canvas layer using its alpha value.
  /// </summary>
  void Hide();

  /// <summary>
  /// Hides and resets the focused item for the associated UI.
  /// </summary>
  void Destroy();
}