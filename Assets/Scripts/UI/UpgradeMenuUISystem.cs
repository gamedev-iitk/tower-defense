using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;

using System.Threading;

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

    public Animator animator;
    private List<GameObject> upgradeButtons = new List<GameObject>();

    private GameObject focusedTower;
    public  CanvasGroup canvasGroup;
    GameObject towermenu;
    GameObject gameobject;
    public Text dialogueText;


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
        towermenu = GameObject.Find("TowerMenuUI");
        gameobject  = GameObject.Find("GameObject"); 
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
                if (gameobject.GetComponent<scoreup>().theScore >= 20)
                {
                    animator.SetBool("IsOpen", false);
                    gameobject.GetComponent<scoreup>().theScore -= 20;
                    
                    CreateNewTower(greenPrefab);
                  
                } 
                else
                {
                    dialogueText.text = "You Don't Have Enough Money";
                    
                }
               
                break;

            case "red":
                if (gameobject.GetComponent<scoreup>().theScore >= 20)
                {
                    animator.SetBool("IsOpen", false);
                    gameobject.GetComponent<scoreup>().theScore -= 20;
                    CreateNewTower(redPrefab);
                }
                else
                {
                    dialogueText.text = "You Don't Have Enough Money";

                }
                break;

            case "gold":
                if (gameobject.GetComponent<scoreup>().theScore >= 20)
                {
                    animator.SetBool("IsOpen", false);
                    gameobject.GetComponent<scoreup>().theScore -= 20;
                    CreateNewTower(goldPrefab);
                }
                else
                {
                    dialogueText.text = "You Don't Have Enough Money";

                }
                break;

            case "move":
                if (gameobject.GetComponent<scoreup>().theScore >= 10)
                {
                    animator.SetBool("IsOpen", false);
                    gameobject.GetComponent<scoreup>().theScore -= 10;
                    towermenu.GetComponent<TowerMenuUISystem>().OnMoveClick();
                }
                else
                {
                    dialogueText.text = "You Don't Have Enough Money";
                    canvasGroup.alpha = 0;
                    
                }
                break;

            default:
                Debug.LogError("Failed to upgrade tower.");
                break;
        }
        SetButtonActivation(focusedTower.GetComponent<UpgradeTree>());
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
    void CreateNewTower(GameObject newTowerPrefab)
    {
        Vector3 focusedTowerPosition = focusedTower.transform.position;
        Vector3 spawnPoint = new Vector3(focusedTowerPosition.x, 0, focusedTowerPosition.z);
        GameObject newTower = Instantiate(newTowerPrefab, spawnPoint, focusedTower.transform.rotation);
        Destroy(focusedTower);
        focusedTower = newTower;
    }
}
