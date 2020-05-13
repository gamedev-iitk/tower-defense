using UnityEngine;


/// <summary>
/// Implements the tower placement location selector.
/// </summary>
public class TowerPlacer : MonoBehaviour
{
    /// <summary>
    /// A reference to the tower prefab that is to be placed.
    /// </summary>
    public GameObject TowerObject;

    private bool blocked = false;
    private readonly Color green = new Color(0, 1, 0, 0.3f);
    private readonly Color red = new Color(1, 0, 0, 0.3f);
    private new Renderer renderer;
    private int count;
    private bool toDestroy;

    /// <summary>
    /// Set the tower prefab to be placed.
    /// </summary>
    /// <param name="reference">The tower object to create.</param>
    /// <param name="isMove">Whether this placer is for moving a tower or creating a new one.</param>
    public void SetTower(GameObject reference, bool isMove)
    {
        TowerObject = reference;
        toDestroy = isMove;
    }

    /// <summary>
    /// Instantiate the tower.
    /// </summary>
    /// <returns>True if successfully instantiated the tower.</returns>
    public bool PlaceTower()
    {
        if (!blocked)
        {
            Instantiate(TowerObject, transform.position, transform.rotation);
            if (toDestroy)
            {
                Destroy(TowerObject);
            }
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
