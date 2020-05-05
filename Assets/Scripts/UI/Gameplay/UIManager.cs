using System;
using UnityEngine;

/// <summary>
/// Manager class that holds references to all UI layers and handles the UI stack.
/// </summary>
public class UIManager : MonoBehaviour
{
    private UpgradeMenuUISystem upgradeMenuSystem;
    private TowerMenuUISystem towerMenuSystem;
    private PauseMenu pauseMenu;
    private IUISystem activeScreen;

    void Start()
    {
        // Initialize private fields
        towerMenuSystem = transform.Find("TowerMenuUI").GetComponent<TowerMenuUISystem>();
        upgradeMenuSystem = transform.Find("UpgradeUI").GetComponent<UpgradeMenuUISystem>();
        pauseMenu = transform.Find("PauseMenu").GetComponent<PauseMenu>();

        // Register events and callbacks
        EventRegistry.RegisterAction<GameObject, Type>("showMenu", ShowMenu);
        EventRegistry.RegisterAction("hideMenu", HideMenu);
        EventRegistry.RegisterAction("pause", Pause);
    }

    public void ShowMenu(GameObject tower, Type type)
    {
        if (type == typeof(UpgradeMenuUISystem))
        {
            upgradeMenuSystem.Show(tower);
            activeScreen = upgradeMenuSystem;
        }
        else if (type == typeof(TowerMenuUISystem))
        {
            towerMenuSystem.Show(tower);
            activeScreen = towerMenuSystem;
        }
    }

    public void HideMenu()
    {
        activeScreen.Hide();
    }

    public void Pause()
    {
        Time.timeScale = 0;
        HideMenu();

        pauseMenu.Show();
    }
}
