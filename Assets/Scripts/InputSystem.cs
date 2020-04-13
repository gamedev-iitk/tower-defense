using UnityEngine;


/// <summary>
/// Manager class for handling mouse input.
/// </summary>
public class InputSystem : MonoBehaviour
{
    private Camera _mainCamera;
    private GameObject _player;
    private UIManager _UIManager;
    
    void Start()
    {
        // Initialize private fields
        _mainCamera = Camera.main;
        _player = GameObject.Find("Player");
        _UIManager = GameObject.Find("UIManager").GetComponent<UIManager>();

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
                    // TODO: This should fire an even instead.
                    _UIManager.ShowTowerMenu(hitObject);
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
            // TODO: This should fire an event instead.
            _UIManager.HideAll();
        }
    }
}
