using UnityEngine;
using UnityEngine.UI;


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
    // goldb... are the button prefabs
    [SerializeField]
    private GameObject goldb;

    [SerializeField]
    private GameObject redb;

    [SerializeField]
    private GameObject greenb;

    private GameObject focusedTower;
    private CanvasGroup canvasGroup;
    // branch handles the value of bools while upgrading towers
    private UpgradeTree branch;
    // goldButton... are the button components of goldb...
    private Button redButton;

    private Button goldButton;

    private Button greenButton;

    void Start()
    {
        // Initialize private fields
        canvasGroup = GameObject.Find("UpgradeUI").GetComponent<CanvasGroup>();
        goldButton = goldb.GetComponent<Button>();
        redButton = redb.GetComponent<Button>();
        greenButton = greenb.GetComponent<Button>();
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
            {
                CreateNewTower(greenPrefab);
                // to make buttons interactable/non-interactable at the time of clicking itself
                goldButton.interactable = false;
                redButton.interactable = true;
                break;
            }
            case "red":
                CreateNewTower(redPrefab);
                greenButton.interactable = false;
                break;

            case "gold": 
            {
                CreateNewTower(goldPrefab);
                greenButton.interactable = false;
                break;
            }
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
        if(tree.green == true)
        {
            greenButton.interactable = true;
        }
        else
        {
             greenButton.interactable = false;
        }
        if(tree.gold == true)
        {
            goldButton.interactable = true;
        }
        else
        {
             goldButton.interactable = false;
        }
        if(tree.red == true)
        {
            redButton.interactable = true;
        }
        else
        {
             redButton.interactable = false;
        }    
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
        branch = newTowerPrefab.GetComponent<UpgradeTree>();
        if (newTowerPrefab == greenPrefab)
        {
            branch.green = false;
            branch.gold = false;
            branch.red = true;
        }
        if (newTowerPrefab == redPrefab)
        {
            branch.green = false;
            branch.gold = false;
            branch.red = false;
        }
        if (newTowerPrefab == goldPrefab)
        {
            branch.green = false;
            branch.gold = false;
            branch.red = false;
        }
        Destroy(focusedTower);
        focusedTower = newTower;
    }
}
