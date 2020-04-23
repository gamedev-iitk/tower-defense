using UnityEngine;

public class TowerPlacementSystem : MonoBehaviour
{
    public GameObject SelectorPrefab;
    private GameObject instance;
    private GameObject focus;
    private bool isActive = false;

    void Start()
    {
        EventRegistry.RegisterAction<GameObject, Vector3>("togglePlacer", TogglePlacer);
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

    public void TogglePlacer(GameObject targetTower, Vector3 location)
    {
        if (isActive)
        {
            DestroyPlacer();
        }
        else
        {
            CreatePlacer(targetTower, location);
        }
    }

    private void CreatePlacer(GameObject targetTower, Vector3 location)
    {
        location.y=0;  //so that indicator spawn on the ground
        instance = Instantiate(SelectorPrefab, location, Quaternion.identity);
        instance.GetComponent<TowerPlacer>().SetTower(targetTower);
        isActive = true;
    }

    private void DestroyPlacer()
    {
        Destroy(instance);
        isActive = false;
    }
}