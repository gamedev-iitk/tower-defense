using UnityEngine;


/// <summary>
/// Manager class for tower upgrades. Handles UI and tower object instantiation/deletion.
/// </summary>
public class UpgradeSystem : MonoBehaviour
{
    [SerializeField]
    private GameObject _goldPrefab;
    [SerializeField]
    private GameObject _greenPrefab;
    [SerializeField]
    private GameObject _redPrefab;

    private GameObject _focusedTower;
    private CanvasGroup _canvasGroup;


    void Start() {
        _canvasGroup = GameObject.Find("Canvas").GetComponent<CanvasGroup>();
        _canvasGroup.alpha = 0;
    }


    /// <summary>
    /// Shows the Upgrade UI for a given tower.
    /// <param name="tower">Selected tower.</param>
    /// </summary>
    public void ShowUI(GameObject tower) {
        _focusedTower = tower;
        setButtonActivation(tower.GetComponent<UpgradeTree>());
        _canvasGroup.alpha = 1;
    }


    /// <summary>
    /// Hide the Upgrade UI on screen.
    /// </summary>
    public void HideUI() {
        _focusedTower = null; // probably have a Tower.Empty() instead?
        _canvasGroup.alpha = 0;
    }


    /// <summary>
    /// Callback for the Upgrade UI.
    /// <param name="type">String indicating the type of tower upgrade requested.</param>
    /// </summary>
    public void OnClicked(string type) {
        switch (type)
        {  
            case "green":
                createNewTower(_greenPrefab);
                break;
            case "red":
                createNewTower(_redPrefab);
                break;
            case "gold":
                createNewTower(_goldPrefab);
                break;
            default:
                Debug.Log("Failed to upgrade tower.");
                break;
        }
    }


    /// <summary>
    /// Sets the <c>Interactable </c> property on buttons for upgrades based on the provided <c>UpgradeTree</c>.
    /// <param name="tree"><c>UpgradeTree </c> object to set button activation.</param>
    /// </summary>
    void setButtonActivation(UpgradeTree tree) {
        //
    }


    /// <summary>
    /// Creates a new tower where the currently focused tower is and then destroys the focused tower.
    /// <param name="newTowerPrefab">Prefab for the new tower to create.</param>
    /// </summary>
    void createNewTower(GameObject newTowerPrefab) {
        GameObject newTower = Instantiate(newTowerPrefab, _focusedTower.transform.position, _focusedTower.transform.rotation);
        Debug.Log(newTower);
        GameObject.Destroy(_focusedTower);
        _focusedTower = newTower;
    }
}
