using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

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

    private List<GameObject> upgradeButtons = new List<GameObject>();
    private GameObject focusedTower;
    private CanvasGroup canvasGroup;
    private TDEvent<ETowerType> showUpgradeDialog;

    void Start()
    {
        // Initialize private fields
        canvasGroup = GameObject.Find("UpgradeUI").GetComponent<CanvasGroup>();

        GameObject image = GameObject.Find("UpgradeUI/Image");
        for (int i = 1; i < image.transform.childCount; i++)
        {
            upgradeButtons.Add(image.transform.GetChild(i).gameObject);
        }

        // Hide canvas
        canvasGroup.alpha = 0;

        // Register events and callbacks
        showUpgradeDialog = EventRegistry.GetEvent<ETowerType>("showUpgradeDialog");
        EventRegistry.RegisterAction<ETowerType>("createTower", CreateTower);
        EventRegistry.RegisterAction("cancelTowerCreation", CancelTowerCreation);
    }

    // Interface overrides

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
        Hide();
        ETowerType requestedType = ETowerTypeUtils.GetTowerType(type);
        showUpgradeDialog.Invoke(requestedType);
    }

    /// <summary>
    /// Callback for the "createTower" event. Called when the upgrade is confirmed through the dialog box.
    /// </summary>
    /// <param name="type">Type of the tower to create</param>
    public void CreateTower(ETowerType type)
    {
        Debug.Log("Upgrade confirmed for tower type: " + type.GetString());
        GameObject prefab;

        // TODO: Can Unity use simplified "switch expressions" instead of this bulky thing?
        switch (type)
        {
            case ETowerType.Red:
                prefab = redPrefab;
                break;
            case ETowerType.Gold:
                prefab = goldPrefab;
                break;
            case ETowerType.Green:
                prefab = greenPrefab;
                break;
            default:
                prefab = null;
                break;
        }

        InstantiateTower(prefab);
    }

    /// <summary>
    /// Callback for "cancelTowerCreation" event. Called when upgrade confirmation dialog is closed.
    /// </summary>
    public void CancelTowerCreation()
    {
        Debug.Log("Cancelled tower creation.");
    }

    /// <summary>
    /// Sets the <c>Interactable </c> property on buttons for upgrades based on the provided <c>UpgradeTree</c>.
    /// <param name="tree"><c>UpgradeTree </c> object to set button activation.</param>
    /// </summary>
    void SetButtonActivation(UpgradeTree tree)
    {
        foreach (GameObject button in upgradeButtons)
        {
            tree.valuePairs.TryGetValue(button.GetComponent<TowerType>().Type, out bool isActive);
            button.GetComponent<Button>().interactable = isActive;
        }
    }

    /// <summary>
    /// Creates a new tower where the currently focused tower is and then destroys the focused tower.
    /// <param name="newTowerPrefab">Prefab for the new tower to create.</param>
    /// </summary>
    void InstantiateTower(GameObject newTowerPrefab)
    {
        Vector3 focusedTowerPosition = focusedTower.transform.position;
        Vector3 spawnPoint = new Vector3(focusedTowerPosition.x, 0, focusedTowerPosition.z);
        GameObject newTower = Instantiate(newTowerPrefab, spawnPoint, focusedTower.transform.rotation);

        Destroy(focusedTower);
        focusedTower = newTower;

        SetButtonActivation(focusedTower.GetComponent<UpgradeTree>());
    }
}
