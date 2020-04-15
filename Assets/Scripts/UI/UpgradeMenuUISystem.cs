using UnityEngine;


/// <summary>
/// Manager class for tower upgrades. Handles UI and tower object instantiation/deletion.
/// </summary>
public class UpgradeMenuUISystem : MonoBehaviour, IUISystem
{
    // TODO: Fetch these prefabs in code
    [SerializeField]
    private GameObject goldPrefab;
    [SerializeField]
    private GameObject greenPrefab;
    [SerializeField]
    private GameObject redPrefab;

    private GameObject focusedTower;
    private CanvasGroup canvasGroup;

    void Start()
    {
        // Initialize private fields
        canvasGroup = GameObject.Find("UpgradeUI").GetComponent<CanvasGroup>();

        // Hide canvas
        canvasGroup.alpha = 0;
    }

    public bool Create(GameObject tower)
    {
        focusedTower = tower;
        SetButtonActivation(tower.GetComponent<UpgradeTree>());
        canvasGroup.alpha = 1;
        return true;
    }

    public void Hide()
    {
        canvasGroup.alpha = 0;
    }

    public void Destroy()
    {
        Hide();
        focusedTower = null; // probably have a Tower.Empty() instead?
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
                CreateNewTower(greenPrefab);
                break;

            case "red":
                CreateNewTower(redPrefab);
                break;

            case "gold":
                CreateNewTower(goldPrefab);
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
        Vector3 focusedTowerPosition = focusedTower.transform.position;
        Vector3 spawnPoint = new Vector3(focusedTowerPosition.x, 0, focusedTowerPosition.z);
        GameObject newTower = Instantiate(newTowerPrefab, spawnPoint, focusedTower.transform.rotation);
        Destroy(focusedTower);
        focusedTower = newTower;
    }
}
