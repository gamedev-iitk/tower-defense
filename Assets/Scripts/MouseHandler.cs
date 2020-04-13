using UnityEngine;


/// <summary>
/// Manager class for handling mouse input.
/// </summary>
public class MouseHandler : MonoBehaviour
{    
    private Camera _mainCamera;
    private UpgradeSystem _upgradeSystem;


    void Start()
    {
        _mainCamera = Camera.main;
        _upgradeSystem = GameObject.Find("TowerUpgradeSystem").GetComponent<UpgradeSystem>();
        Cursor.visible = true;
    }


    void Update()
    {
        if (Input.GetMouseButtonDown(0)) {
            RaycastHit hit;
            if (Physics.Raycast(_mainCamera.ScreenPointToRay(Input.mousePosition), out hit)) {
                GameObject hitObject = hit.transform.gameObject;
                if (hitObject.CompareTag("Tower")) {
                    _upgradeSystem.ShowUI(hitObject);
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape)) {
            _upgradeSystem.HideUI();
        }
    }
}
