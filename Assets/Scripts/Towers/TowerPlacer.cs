using UnityEngine;

public class TowerPlacer : MonoBehaviour
{
    private Renderer _renderer;
    private readonly Color _green = new Color(0, 1, 0, 0.3f);
    private readonly Color _red = new Color(1, 0, 0, 0.3f);

    private bool blocked = false;

    public GameObject basetower;

    void Start()
    {
        _renderer = GetComponent<Renderer>();
    }

    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray, out RaycastHit hit))
        {
            if (hit.transform.gameObject.CompareTag("Ground"))
            {
                // TODO: Interpolate values instead of directly setting them for a smoother experience
                float oldY = transform.position.y;
                transform.position = new Vector3(hit.point.x, oldY, hit.point.z);
            }
        }

        if (Input.GetButtonDown("Fire1") && !blocked)
        {
            Instantiate(basetower, transform.position, transform.rotation);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Ground"))
        {
            _renderer.material.color = _red;
            blocked = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (!other.gameObject.CompareTag("Ground"))
        {
            _renderer.material.color = _green;
            blocked = false;
        }
    }
}
