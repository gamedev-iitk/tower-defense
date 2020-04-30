using UnityEngine;

public class GameState : MonoBehaviour
{
    /// <summary>
    /// Amount of cash that players will start with.
    /// </summary>
    public int StartingCash = 2000;

    private int currentCash;

    void Start()
    {
        currentCash = StartingCash;
    }
}