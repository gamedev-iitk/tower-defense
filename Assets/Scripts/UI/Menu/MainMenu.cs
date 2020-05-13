using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Handles Main Menu behavior
/// </summary>
public class MainMenu : MonoBehaviour
{
    private DialogSystem dialog;

    void Start()
    {
        dialog = transform.Find("Dialog").GetComponent<DialogSystem>();
    }

    /// <summary>
    /// Callback for the play button. Loads the next scene.
    /// TODO: Use async operations and a loading screen to change scenes
    /// </summary>
    public void OnPlay()
    {
        SceneManager.LoadScene("LoadingScene");
        Time.timeScale = 1f;

        SceneManager.UnloadSceneAsync("MainMenu");
    }

    /// <summary>
    /// Callback for the help button. Activates the help panel.
    /// </summary>
    public void OnHelp()
    {
        // Show help
    }

    /// <summary>
    /// Callback for the settings screen. Activates the settings panel.
    /// </summary>
    public void OnSettings()
    {
        // Show Settings
    }

    /// <summary>
    /// Callback for the quit button. Brings up the dialog to ask for confirmation.
    /// </summary>
    public void OnQuit()
    {
        DialogConfig config = new DialogConfig
        {
            Message = "Are you sure you want to quit?",
            OK = new DialogButton(
                text: "Yes",
                onClick: Application.Quit
            ),
            Cancel = new DialogButton(
                text: "Cancel"
            )
        };

        dialog.Show(config);
    }
}
