
/// <summary>
/// Data class for game state variables.
/// </summary>
public static class GameState
{
    /// <summary>
    /// Current cash that the player has.
    /// </summary>
    /// <value>An integer value, starts at 2000.</value>
    public static int CurrentCash { get; set; } = 2000;

    /// <summary>
    /// Wave number that the player is at.
    /// </summary>
    /// <value>An integer value, starts at 0.</value>
    public static int WaveNumber { get; set; } = 0;
}