using UnityEngine;

public abstract class AbstractBattle : MonoBehaviour
{
    public float Attack = 10;
    public float Defense = 3;
    public float Range = 7;
    public float FireRate = 1;

    void Start() { SetRange(); }

    abstract protected void SetRange();
    abstract public void OnDetect(GameObject enemy);
    abstract public void OnLose();
}