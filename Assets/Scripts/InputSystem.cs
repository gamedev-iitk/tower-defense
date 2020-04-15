using UnityEngine;


/// <summary>
/// Manager class for handling mouse input.
/// </summary>
public class InputSystem : MonoBehaviour
{
    // TODO: Have a single utils class for doing these raycasts
    public GameObject selector;

    private bool placer = false;
    private GameObject player;
    private GameObject selectorInstance;
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
            if (Physics.Raycast(mainCamera.ScreenPointToRay(Input.mousePosition), out RaycastHit hit))
            {
                GameObject hitObject = hit.transform.gameObject;
                if (hitObject.CompareTag("Tower"))
                {
                    // TODO: This should fire an even instead.
                    // TODO: This actually sends in the "cube" now and not the empty root. Maybe add the collider to the root? Or get the parent and then pass that here 
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
            switch (placer)
            {
                case true:
                    {
                        Destroy(selectorInstance);
                        placer = false;
                    }
                    break;

                case false:
                    {
                        Physics.Raycast(mainCamera.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, LayerMask.GetMask("Ground"));
                        selectorInstance = Instantiate(selector, hit.point, Quaternion.identity);
                        placer = true;
                    }
                    break;
            }
        }
    }
}
