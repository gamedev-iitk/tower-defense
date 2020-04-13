using UnityEngine;


/// <summary>
/// Manager class for handling mouse input.
/// </summary>
public class InputSystem : MonoBehaviour
{
    private Camera _mainCamera;
    private UpgradeSystem _upgradeSystem;
    private GameObject _player;
    
    void Start()
    {
        // Initialize private fields
        _mainCamera = Camera.main;
        _player = GameObject.Find("Player");
        _upgradeSystem = GameObject.Find("TowerUpgradeSystem").GetComponent<UpgradeSystem>();

        // Show mouse in game
        Cursor.visible = true;
    }

    void Update()
    {
        // Check left mouse click or selections
        if (Input.GetButtonDown("Fire1")) 
        {
            RaycastHit hit;
            if (Physics.Raycast(_mainCamera.ScreenPointToRay(Input.mousePosition), out hit)) 
            {
                GameObject hitObject = hit.transform.gameObject;
                if (hitObject.CompareTag("Tower")) 
                {
                    _upgradeSystem.ShowUI(hitObject);
                }
            }
        }

        // Check right mouse click or movement
        if (Input.GetButtonDown("Fire2")) 
        {
            RaycastHit hit;
            if (Physics.Raycast(_mainCamera.ScreenPointToRay(Input.mousePosition), out hit)) 
            {
                _player.GetComponent<AIController>().MoveTo(hit.point);
            }
        }

        // Check escape button or cancel
        if (Input.GetButtonDown("Cancel")) 
        {
            _upgradeSystem.HideUI();
        }
    }
}
