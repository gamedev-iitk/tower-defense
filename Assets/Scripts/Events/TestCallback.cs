using UnityEngine;

/// <summary>
/// A class to test the event system.false Place callbacks here.
/// </summary>
public class TestCallback : MonoBehaviour
{
    void Start()
    {
        EventRegistry.RegisterAction<string, int>("test", Callback);
    }

    void Callback(string a, int b)
    {
        Debug.Log("Printed the generic version");
    }
}