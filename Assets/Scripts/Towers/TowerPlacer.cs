using UnityEngine;


/// <summary>
/// Implements the tower placement location selector
/// </summary>
public class TowerPlacer : MonoBehaviour
{
    /// <summary>
    /// A reference to the tower prefab that is to be placed
    /// </summary>
    public GameObject TowerObject;

    private bool blocked = false;
    private readonly Color green = new Color(0, 1, 0, 0.3f);
    private readonly Color red = new Color(1, 0, 0, 0.3f);
    private new Renderer renderer;
    private int count;

    public void SetTower(GameObject reference)
    {
        TowerObject = reference;
    }

    public bool PlaceTower()
    {
        if (!blocked)
        {
            Instantiate(TowerObject, transform.position, transform.rotation);

            // TODO: Find a safer way to do this
            Destroy(TowerObject);
            return true;
        }
        else
        {
            return false;
        }
    }

    void Start()
    {
        renderer = GetComponent<Renderer>();
    }

    void LateUpdate()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, LayerMask.GetMask("Ground")))
        {
            // TODO: Interpolate values instead of directly setting them for a smoother experience
            float oldY = transform.position.y;
            transform.position = new Vector3(hit.point.x, oldY, hit.point.z);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        count++;
        renderer.material.SetColor("_BaseColor", red);
        blocked = true;
    }

    void OnTriggerExit(Collider other)
    {
        count--;
        if (count == 0)
        {
            renderer.material.SetColor("_BaseColor", green);
            blocked = false;
        }
    }
}
