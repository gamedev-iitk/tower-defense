using UnityEngine;
using UnityEngine.Events;

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