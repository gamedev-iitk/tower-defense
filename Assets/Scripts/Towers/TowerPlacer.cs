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

    private GameObject newTower;

    private bool blocked = false;
    private readonly Color green = new Color(0, 1, 0, 0.3f);
    private readonly Color red = new Color(1, 0, 0, 0.3f);
    private new Renderer renderer;
    private int count;
    private bool toDestroy;

    private bool isActionComplete = false;

    private Vector3 placePoint;

    private TDEvent<ETowerType> showMoveDialog;

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
        placePoint = transform.position;
        if (!blocked)
        {
            if (toDestroy)
            {
                string type = ETowerTypeUtils.GetString(TowerObject.GetComponent<TowerType>().Type);
                ETowerType requestedType = ETowerTypeUtils.GetTowerType(type);
                showMoveDialog.Invoke(requestedType);
            }
            else
            {
                isActionComplete = true;
                Instantiate(TowerObject, placePoint, transform.rotation);
            }
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool CheckTransaction()
    {
        return isActionComplete;
    }

    public void MoveTransaction(bool confirmation)
    {
        if (confirmation)
        {
            Instantiate(TowerObject, placePoint, transform.rotation);
            Destroy(TowerObject);
        }
        isActionComplete = true;
    }

    void Awake()
    {
        renderer = GetComponent<Renderer>();
        showMoveDialog = EventRegistry.GetEvent<ETowerType>("showMoveDialog");
        EventRegistry.RegisterAction<bool>("moveTransaction", MoveTransaction);
        toDestroy = false;
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
