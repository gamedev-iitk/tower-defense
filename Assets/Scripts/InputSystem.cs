using UnityEngine;
using UnityEngine.Events;


/// <summary>
/// Manager class for handling mouse input.
/// </summary>
public class InputSystem : MonoBehaviour
{
    // TODO: Have a single utils class for doing these raycasts
    public GameObject BaseTower;

    private GameObject player;
    private Camera mainCamera;
    private UIManager uiManager;

    void Start()
    {
        // Initialize private fields
        mainCamera = Camera.main;
        player = GameObject.Find("Player");
        uiManager = GameObject.Find("UIManager").GetComponent<UIManager>();

        // Show mouse in game
        Cursor.visible = true;
    }

    void Update()
    {
        // Check left mouse click or "select"
        if (Input.GetButtonDown("Fire1"))
        {
            if (Physics.Raycast(mainCamera.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, LayerMask.GetMask("Default")))
            {
                GameObject hitObject = hit.transform.parent.gameObject;
                if (hitObject.CompareTag("Tower"))
                {
                    // TODO: This should fire an even instead.
                    uiManager.ShowTowerMenu(hitObject);
                }
            }
        }

        // Check right mouse click or "walk"
        if (Input.GetButtonDown("Fire2"))
        {
            if (Physics.Raycast(mainCamera.ScreenPointToRay(Input.mousePosition), out RaycastHit hit))
            {
                player.GetComponent<AIController>().MoveTo(hit.point);
            }
        }

        // Check escape button or "cancel"
        if (Input.GetButtonDown("Cancel"))
        {
            // TODO: This should fire an event instead.
            uiManager.HideAll();
        }

        // Check the X button or "place", toggle the selector
        if (Input.GetKeyDown(KeyCode.X))
        {
            Physics.Raycast(mainCamera.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, LayerMask.GetMask("Ground"));
            EventRegistry.Invoke("togglePlacer", BaseTower, hit.point);
        }
    }
}
