using UnityEngine;

/// <summary>
/// Base class for combat related functionality. Enemies and towers should implement this class.
/// This class works closely with the Detection component.
/// </summary>
public abstract class AbstractBattle : MonoBehaviour
{
    /// <summary>
    /// Attack power of this unit. This is the amount that is subtracted from the enemy's health.
    /// </summary>
    public float Attack = 10;

    /// <summary>
    /// Defense stat for this unit. When attacked this unit will absorb this fraction of the applied damage.
    /// Must be between 0 and 1.
    /// </summary>
    public float Defense = 0.3f;

    /// <summary>
    /// Detection range for this unit. It will only attack when enemies are closer than this value.
    /// </summary>
    public float Range = 7;

    /// <summary>
    /// Rate of attack for this unit. Value must be in attacks per second.
    /// </summary>
    public float FireRate = 1;

    /// <summary>
    /// Called in the Start function for initialization.
    /// </summary>
    abstract protected void SetRange();

    /// <summary>
    /// Called by the Detection component to start attack processes.
    /// </summary>
    /// <param name="enemy">GameObject to target</param>
    abstract public void OnDetect(GameObject enemy);

    /// <summary>
    /// A "reset" for the attack processes used when the target is destroyed or lost.
    /// </summary>
    abstract public void OnLose();

    void Start() { SetRange(); }
}