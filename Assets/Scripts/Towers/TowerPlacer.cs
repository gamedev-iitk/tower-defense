using UnityEngine;

public class TowerPlacer : MonoBehaviour
{
    private Renderer _renderer;
    private readonly Color _green = new Color(0, 1, 0, 0.3f);
    private readonly Color _red = new Color(1, 0, 0, 0.3f);

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
                Debug.Log("I hit the ground");

                // TODO: Interpolate values instead of directly setting them for a smoother experience
                float oldY = transform.position.y;
                transform.position = new Vector3(hit.point.x, oldY, hit.point.z);
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Ground"))
        {
            _renderer.material.color = _red;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (!other.gameObject.CompareTag("Ground"))
        {
            _renderer.material.color = _green;
        }
    }
}
