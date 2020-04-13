using UnityEngine;


/// <summary>
/// Brings up the tower menu on click
/// </summary>
public class TowerMenuUISystem : MonoBehaviour, IUISystem
{
    private CanvasGroup _canvasGroup;
    private GameObject _focusedTower;

    private UIManager _uiManager;

    void Start()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
        _canvasGroup.alpha = 0;

        _uiManager = GameObject.Find("UIManager").GetComponent<UIManager>();
    }

    public bool Create(GameObject tower)
    {
        if (GameObject.Equals(_focusedTower, tower))
        {
            return false;
        }
        else
        {
            Debug.Log("This is actually a new menu.");
            _focusedTower = tower;
            _canvasGroup.alpha = 1;
            return true;
        }
    }

    public void Hide()
    {
        _canvasGroup.alpha = 0;
    }

    public void Destroy()
    {
        Hide();
        _focusedTower = null;
    }

    public void OnClick()
    {
        // TODO: this should fire an event instead. We should not have a reference to the UI manager here.
        Hide();

        // Move player to the tower.

        _uiManager.ShowUpgradeMenu(_focusedTower);
    }
}
