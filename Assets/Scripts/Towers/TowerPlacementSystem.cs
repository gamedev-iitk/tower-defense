using UnityEngine;

/// <summary>
/// System to place towers. Sets up the TowerPlacer.
/// </summary>
public class TowerPlacementSystem : MonoBehaviour
{
    /// <summary>
    /// Prefab to use as the TowerPlacer.
    /// </summary>
    public GameObject PlacerPrefab;

    private GameObject instance;
    private GameObject focus;
    private bool isActive = false;

    void Start()
    {
        EventRegistry.RegisterAction<GameObject, Vector3, bool>("togglePlacer", TogglePlacer);
    }

    void Update()
    {
        if (isActive && Input.GetButtonDown("Fire1"))
        {
            if (instance.GetComponent<TowerPlacer>().PlaceTower())
            {
                DestroyPlacer();
            }
        }
    }

    /// <summary>
    /// Callback for the <c>togglePlacer </c> event. Toggles the TowerPlacer.
    /// </summary>
    /// <param name="targetTower">The tower to create with this placer.</param>
    /// <param name="location">The position to create the placer at.</param>
    public void TogglePlacer(GameObject targetTower, Vector3 location, bool action)
    {
        if (isActive)
        {
            DestroyPlacer();
        }
        else
        {
            CreatePlacer(targetTower, location, action);
        }
    }

    private void CreatePlacer(GameObject targetTower, Vector3 location, bool action)
    {
        location.y = 0;  //so that indicator spawn on the ground
        instance = Instantiate(PlacerPrefab, location, Quaternion.identity);
        instance.GetComponent<TowerPlacer>().SetTower(targetTower, action);
        isActive = true;
    }

    private void DestroyPlacer()
    {
        Destroy(instance);
        isActive = false;
    }
}