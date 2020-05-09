using System;
using UnityEngine;

/// <summary>
/// Manager class for handling mouse input.
/// </summary>
public class InputSystem : MonoBehaviour
{
    /// <summary>
    /// Prefab that the tower placer will place by default
    /// </summary>
    public GameObject BaseTower;

    private GameObject player;
    private Camera mainCamera;
    private TDEvent<GameObject, Type> showMenu;
    private TDEvent hideMenu;

    void Start()
    {
        // Initialize private fields
        mainCamera = Camera.main;
        player = GameObject.Find("Player");

        // Show mouse in game
        Cursor.visible = true;

        // Register events and callbacks
        showMenu = EventRegistry.GetEvent<GameObject, Type>("showMenu");
        hideMenu = EventRegistry.GetEvent("hideMenu");
    }

    void Update()
    {
        // Check left mouse click or "select"
        if (Input.GetButtonDown("Fire1"))
        {
            if (Physics.Raycast(mainCamera.ScreenPointToRay(Input.mousePosition), out RaycastHit hit))
            {
                GameObject hitObject = hit.transform.parent.gameObject;
                if (hitObject.CompareTag("Tower"))
                {
                    showMenu.Invoke(hitObject, typeof(TowerMenuUISystem));
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
            hideMenu.Invoke();
        }

        // TODO: Use input mappings here
        // Check the X button or "place", toggle the selector
        if (Input.GetKeyDown(KeyCode.X))
        {
            Physics.Raycast(mainCamera.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, LayerMask.GetMask("Ground"));
            EventRegistry.Invoke("togglePlacer", BaseTower, hit.point);
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            EventRegistry.Invoke("pause");
        }
    }
}
