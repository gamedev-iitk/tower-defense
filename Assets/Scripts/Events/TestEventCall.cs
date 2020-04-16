using UnityEngine;
using UnityEngine.Events;

public class TestEventCall : MonoBehaviour
{
    private float timer = 0;
    public UnityEvent a;
    void Start()
    {
        EventRegistry.RegisterEvent("test", a);
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer > 3)
        {
            EventRegistry.Invoke("test");
        }
    }

    public void Hello()
    {
        Debug.LogWarning("Hello");
    }
}