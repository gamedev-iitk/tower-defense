using System;
using UnityEngine;

/// <summary>
/// Brings up the tower menu when a tower is selected.
/// </summary>
public class TowerMenuUISystem : MonoBehaviour, IUISystem
{
    private GameObject focusedTower;

    void Start()
    {
        Hide();
    }

    public void Show(GameObject tower)
    {
        focusedTower = tower;
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    /// <summary>
    /// Callback for the upgrade button.
    /// </summary>
    public void OnUpgradeClick()
    {
        Hide();

        // Move player to the tower.

        EventRegistry.Invoke("showMenu", focusedTower, typeof(UpgradeMenuUISystem));
    }

    /// <summary>
    /// Callback for the Move button
    /// </summary>
    public void OnMoveClick()
    {
        Hide();
        Vector3 spawnLocation = focusedTower.transform.position;
        spawnLocation.y = 0;
        EventRegistry.Invoke("togglePlacer", focusedTower, spawnLocation);
    }
}
