using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private UpgradeMenuUISystem _upgradeMenuSystem;
    private TowerMenuUISystem _towerMenuSystem;
    
    private Stack<IUISystem> _screenStack;
    

    void Start()
    {
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

        Debug.Log(_screenStack);
    }

    public void ShowUpgradeMenu(GameObject tower)
    {
        // TODO: This should receive an event instead.
        if (_upgradeMenuSystem.Create(tower))
        {
            _screenStack.Push(_upgradeMenuSystem);
        }
    }    

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
