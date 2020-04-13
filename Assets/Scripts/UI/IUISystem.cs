using UnityEngine;

public interface IUISystem
{
    bool Create(GameObject obj);
    void Hide();
    void Destroy();
}