using UnityEngine;
using UnityEngine.UI;


/// <summary>
/// Manager class for tower upgrades. Handles UI and tower object instantiation/deletion.
/// </summary>
public class UpgradeMenuUISystem : MonoBehaviour, IUISystem
{
    [SerializeField]
    private GameObject _goldPrefab;
    [SerializeField]
    private GameObject _greenPrefab;
    [SerializeField]
    private GameObject _redPrefab;

    private GameObject _focusedTower;
    private CanvasGroup _canvasGroup;


    void Start()
    {
        // Initialize private fields
        _canvasGroup = GameObject.Find("UpgradeUI").GetComponent<CanvasGroup>();

        // Hide canvas
        _canvasGroup.alpha = 0;
    }


    /// <summary>
    /// Shows the Upgrade UI for a given tower.
    /// <param name="tower">Selected tower.</param>
    /// </summary>
    public bool Create(GameObject tower)
    {
        _focusedTower = tower;
        SetButtonActivation(tower.GetComponent<UpgradeTree>());
        _canvasGroup.alpha = 1;
        return true;
    }


    /// <summary>
    /// Hide the Upgrade UI on screen.
    /// </summary>
    public void Hide()
    {
        _canvasGroup.alpha = 0;
    }

    public void Destroy()
    {
        Hide();
        _focusedTower = null; // probably have a Tower.Empty() instead?
    }


    /// <summary>
    /// Callback for the Upgrade UI.
    /// <param name="type">String indicating the type of tower upgrade requested.</param>
    /// </summary>
    public void OnClick(string type)
    {
        switch (type)
        {
            case "green":
                CreateNewTower(_greenPrefab);
                break;

            case "red":
                CreateNewTower(_redPrefab);
                break;

            case "gold":
                CreateNewTower(_goldPrefab);
                break;

            default:
                Debug.LogError("Failed to upgrade tower.");
                break;
        }
    }


    /// <summary>
    /// Sets the <c>Interactable </c> property on buttons for upgrades based on the provided <c>UpgradeTree</c>.
    /// <param name="tree"><c>UpgradeTree </c> object to set button activation.</param>
    /// </summary>
    void SetButtonActivation(UpgradeTree tree)
    {
        //
    }


    /// <summary>
    /// Creates a new tower where the currently focused tower is and then destroys the focused tower.
    /// <param name="newTowerPrefab">Prefab for the new tower to create.</param>
    /// </summary>
    void CreateNewTower(GameObject newTowerPrefab)
    {
        Vector3 focusedTowerPosition = _focusedTower.transform.position;
        Vector3 spawnPoint = new Vector3(focusedTowerPosition.x, 0, focusedTowerPosition.z);
        GameObject newTower = Instantiate(newTowerPrefab, spawnPoint, _focusedTower.transform.rotation);
        Destroy(_focusedTower);
        _focusedTower = newTower;
    }
}
