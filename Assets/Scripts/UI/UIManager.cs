using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Manager class that holds references to all UI layers and handles the UI stack.
/// </summary>
public class UIManager : MonoBehaviour
{
    private UpgradeMenuUISystem _upgradeMenuSystem;
    private TowerMenuUISystem _towerMenuSystem;
    
    private Stack<IUISystem> _screenStack;
    

    void Start()
    {
        // Initialize private fields
        _towerMenuSystem = transform.Find("TowerMenuUI").GetComponent<TowerMenuUISystem>();
        _upgradeMenuSystem = transform.Find("UpgradeUI").GetComponent<UpgradeMenuUISystem>();
        _screenStack = new Stack<IUISystem>();
    }

    public void ShowTowerMenu(GameObject tower)
    {
        // TODO: This should receive an event instead.
        if (_towerMenuSystem.Create(tower))
        {
            _screenStack.Push(_towerMenuSystem);
        }
    }

    public void ShowUpgradeMenu(GameObject tower)
    {
        // TODO: This should receive an event instead.
        if (_upgradeMenuSystem.Create(tower))
        {
            _screenStack.Push(_upgradeMenuSystem);
        }
    }    

    /// <summary>
    /// Hide all UI layers in the stack.
    /// </summary>
    public void HideAll()
    {
        // TODO: This should receive an event instead.
        foreach (IUISystem ui in _screenStack)
        {
            ui.Destroy();
        }

        _screenStack.Clear();
    }
}
