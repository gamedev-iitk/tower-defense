using UnityEngine;

/// <summary>
/// A class to test the event system. Fire events here.
/// </summary>
public class TestEventCall : MonoBehaviour
{
    private TDEvent<string, int> a;
    void Start()
    {
        a = EventRegistry.GetEvent<string, int>("test");
    }

    void Update()
    {
        a.Invoke("Hello", 1);
    }
}