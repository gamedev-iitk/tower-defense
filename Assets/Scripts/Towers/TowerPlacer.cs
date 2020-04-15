using UnityEngine;


/// <summary>
/// Implements the tower placement location selector
/// </summary>
public class TowerPlacer : MonoBehaviour
{
    /// <summary>
    /// A reference to the base tower prefab that is to be placed
    /// </summary>
    public GameObject BaseTower;

    private bool blocked = false;
    private readonly Color green = new Color(0, 1, 0, 0.3f);
    private readonly Color red = new Color(1, 0, 0, 0.3f);
    private new Renderer renderer;

    void Start()
    {
        renderer = GetComponent<Renderer>();
    }

    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, LayerMask.GetMask("Ground")))
        {
            // TODO: Interpolate values instead of directly setting them for a smoother experience
            float oldY = transform.position.y;
            transform.position = new Vector3(hit.point.x, oldY, hit.point.z);
        }

        // TODO: This should be done by receiving an event fired from the InputSystem
        if (Input.GetButtonDown("Fire1") && !blocked)
        {
            Instantiate(BaseTower, transform.position, transform.rotation);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        renderer.material.color = red;
        blocked = true;
    }

    void OnTriggerExit(Collider other)
    {
        renderer.material.color = green;
        blocked = false;
    }
}
