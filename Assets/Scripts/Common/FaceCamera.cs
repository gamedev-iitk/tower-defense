using UnityEngine;

/// <summary>
/// Component to make the game object always face the camera. Used for UI elements.
/// </summary>
public class FaceCamera : MonoBehaviour
{
    void LateUpdate()
    {
        transform.LookAt(
            transform.position + Camera.main.transform.rotation * Vector3.forward,
            Camera.main.transform.rotation * Vector3.up
        );
    }
}
