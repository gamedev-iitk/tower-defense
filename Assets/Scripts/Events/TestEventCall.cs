using UnityEngine;

/// <summary>
/// A class to test the event system. Fire events here.
/// </summary>
public class TestEventCall : MonoBehaviour
{
    private TDEvent<string, int> a = new TDEvent<string, int>();
    void Start()
    {
        EventRegistry.RegisterEvent("test", a);
    }

    void Update()
    {
        a.Invoke("Hello", 1);
    }
}