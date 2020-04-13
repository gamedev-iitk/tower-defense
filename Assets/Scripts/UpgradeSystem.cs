using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UpgradeSystem : MonoBehaviour
{
    private GameObject _focusedTower;
    private CanvasGroup _canvasGroup;


    // Use a dictionary instead. Custom serialization to get it in the Inspector.
    [SerializeField]
    private GameObject _goldPrefab;
    public Button goldButton;
    [SerializeField]
    private GameObject _greenPrefab;
    public Button greenButton;
    [SerializeField]
    private GameObject _redPrefab;
    public Button redButton;

    void Start() {
        _canvasGroup = GameObject.Find("Canvas").GetComponent<CanvasGroup>();
        _canvasGroup.alpha = 0;

        goldButton = GameObject.Find("GoldTower").GetComponent<Button>() as Button;
    }


    public void ShowUI(GameObject tower) {
        _focusedTower = tower;
        setButtonActivation(tower.GetComponent<UpgradeTree>());
        _canvasGroup.alpha = 1;
    }

    public void HideUI() {
        _focusedTower = null; // probably have a Tower.Empty() instead?
        _canvasGroup.alpha = 0;
    }

    public void OnClicked(string type) {
        switch (type)
        {  
            case "green":
                upgradeTower(_greenPrefab);
                break;
            case "red":
                upgradeTower(_redPrefab);
                break;
            case "gold":
                upgradeTower(_goldPrefab);
                break;
            default:
                Debug.Log("Failed to upgrade tower.");
                break;
        }
    }

    void setButtonActivation(UpgradeTree tree) {
        greenButton.SetEnabled(tree.green);
        redButton.SetEnabled(tree.red);
        goldButton.SetEnabled(tree.gold);
    }

    void upgradeTower(GameObject tower) {
        GameObject newTower = Instantiate(tower, _focusedTower.transform.position, _focusedTower.transform.rotation);
        Debug.Log(newTower);
        GameObject.Destroy(_focusedTower);
        _focusedTower = newTower;
    }
}
