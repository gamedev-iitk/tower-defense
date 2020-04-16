using UnityEngine;

public class TestEventCall : MonoBehaviour
{
    public TDEvent<string, int> a = new TDEvent<string, int>();
    void Start()
    {
        EventRegistry.RegisterEvent("test", a);
    }

    void Update()
    {
        a.Invoke("Hello", 1);
    }
}