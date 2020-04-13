using UnityEngine;


/// <summary>
/// Brings up the tower menu when a tower is selected.
/// </summary>
public class TowerMenuUISystem : MonoBehaviour, IUISystem
{
    private CanvasGroup _canvasGroup;
    private GameObject _focusedTower;
    private UIManager _uiManager;
    private AIController _player;

    private bool _wait = false;


    void Start()
    {
        // Initialize private fields
        _canvasGroup = GetComponent<CanvasGroup>();

        _player = GameObject.Find("Player").GetComponent<AIController>();
        _uiManager = GameObject.Find("UIManager").GetComponent<UIManager>();

        // Hide the canvas group
        _canvasGroup.alpha = 0;
    }

    void Update()
    {
        if (_wait && _player.IsStopped())
        {
            ShowUpgradeUI();
            _wait = false;
        }
    }

    public bool Create(GameObject tower)
    {
        if (GameObject.Equals(_focusedTower, tower))
        {
            return false;
        }
        else
        {
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

    /// <summary>
    /// Callback for the upgrade button.
    /// </summary>
    public void OnClick()
    {
        // TODO: this should fire an event instead. We should not have a reference to the UI manager here.
        Hide();
        // TODO: Move player to the tower
        _player.MoveTo(_focusedTower.transform.position);
        _wait = true;
    }

    private void ShowUpgradeUI()
    {
        Debug.Log("Calling the upgrade menu");
        _uiManager.ShowUpgradeMenu(_focusedTower);
    }
}
