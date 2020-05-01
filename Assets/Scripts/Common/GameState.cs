using UnityEngine;

public class GameState : MonoBehaviour
{
    /// <summary>
    /// Amount of cash that players will start with.
    /// </summary>
    public int StartingCash = 2000;

    private int currentCash;

    public static int waveNumber=0;

    void Start()
    {
        currentCash = StartingCash;
    }
}