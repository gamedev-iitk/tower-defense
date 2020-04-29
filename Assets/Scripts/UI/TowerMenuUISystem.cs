using UnityEngine;


/// <summary>
/// Brings up the tower menu when a tower is selected.
/// </summary>
public class TowerMenuUISystem : MonoBehaviour, IUISystem
{
    private CanvasGroup canvasGroup;
    private GameObject focusedTower;
    private UIManager uiManager;


    void Start()
    {
        // Initialize private fields
        canvasGroup = GetComponent<CanvasGroup>();
        uiManager = GameObject.Find("UIManager").GetComponent<UIManager>();

        // Hide the canvas group
        canvasGroup.alpha = 0;
    }

    public bool Create(GameObject tower)
    {
        if (Equals(focusedTower, tower))
        {
            return false;
        }
        else
        {
            focusedTower = tower;
            canvasGroup.alpha = 1;
            return true;
        }
    }

    public void Hide()
    {
        canvasGroup.alpha = 0;
    }

    public void Destroy()
    {
        Hide();
        focusedTower = null;
    }

    /// <summary>
    /// Callback for the upgrade button.
    /// </summary>
    public void OnUpgradeClick()
    {
        // TODO: this should fire an event instead. We should not have a reference to the UI manager here.
        Hide();

        // Move player to the tower.

        uiManager.ShowUpgradeMenu(focusedTower);
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
