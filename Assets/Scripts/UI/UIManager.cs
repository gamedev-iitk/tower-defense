using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Manager class that holds references to all UI layers and handles the UI stack.
/// </summary>
public class UIManager : MonoBehaviour
{
    private Stack<IUISystem> screenStack;
    private UpgradeMenuUISystem upgradeMenuSystem;
    private TowerMenuUISystem towerMenuSystem;

    void Start()
    {
        // Initialize private fields
        towerMenuSystem = transform.Find("TowerMenuUI").GetComponent<TowerMenuUISystem>();
        upgradeMenuSystem = transform.Find("UpgradeUI").GetComponent<UpgradeMenuUISystem>();
        screenStack = new Stack<IUISystem>();
    }

    /// <summary>
    /// Creates the tower menu for a given tower
    /// </summary>
    /// <param name="tower"><c>GameObject </c>for the selected tower</param>
    public void ShowTowerMenu(GameObject tower)
    {
        // TODO: This should receive an event instead.
        if (towerMenuSystem.Create(tower))
        {
            screenStack.Push(towerMenuSystem);
        }
    }

    /// <summary>
    /// Creates the upgrade menu for a given tower
    /// </summary>
    /// <param name="tower"><c>GameObject </c>for the selected tower</param>
    public void ShowUpgradeMenu(GameObject tower)
    {
        // TODO: This should receive an event instead.
        if (upgradeMenuSystem.Create(tower))
        {
            screenStack.Push(upgradeMenuSystem);
        }
    }

    /// <summary>
    /// Hide all UI layers in the stack.
    /// </summary>
    public void HideAll()
    {
        // TODO: This should receive an event instead.
        foreach (IUISystem ui in screenStack)
        {
            ui.Destroy();
        }

        screenStack.Clear();
    }
}
