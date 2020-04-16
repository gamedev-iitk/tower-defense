using UnityEngine;

public class TestCallback : MonoBehaviour
{
    void Start()
    {
        EventRegistry.RegisterAction("test", Callback);
    }

    void Callback()
    {
        Debug.Log("print");
    }
}