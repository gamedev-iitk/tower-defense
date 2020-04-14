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

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Called Trigger Enter with " + other.ToString());
        if (!other.gameObject.CompareTag("Ground"))
        {
            _renderer.material.color = _red;
        }
    }

    void OnTriggerExit(Collider other)
    {
        Debug.Log("Called Trigger Exit with " + other.ToString());
        if (!other.gameObject.CompareTag("Ground"))
        {
            _renderer.material.color = _green;
        }
    }
}
