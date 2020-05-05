using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Handles Pause Menu behavior.
/// </summary>
public class PauseMenu : MonoBehaviour, IUISystem
{
    private GameObject settingsPanel;
    private GameObject mainPanel;
    private DialogSystem dialog;

    void Start()
    {
        mainPanel = transform.Find("MainPanel").gameObject;
        settingsPanel = transform.Find("SettingsPanel").gameObject;
        dialog = transform.Find("Dialog").GetComponent<DialogSystem>();
        
        gameObject.SetActive(false);
    }

    /// <summary>
    /// To implement the IUISystem.
    /// TODO: Change the interface to better handle void functions.
    /// </summary>
    public void Show(GameObject obj)
    {
        Show();
    }

    /// <summary>
    /// Pauses the game with timeScale and activates the PauseMenu object.
    /// </summary>
    public void Show()
    {
        Time.timeScale = 0;
        gameObject.SetActive(true);
    }

    /// <summary>
    /// Deactivates the PauseMenu object.
    /// </summary>
    public void Hide()
    {
        gameObject.SetActive(false);
    }

    /// <summary>
    /// Callback for the resume button. Unpause the game with timeScale and deactivate menu object.
    /// </summary>
    public void OnResume()
    {
        Time.timeScale = 1;
        Hide();
    }

    /// <summary>
    /// Callback for the settings button. Switch the active panel.
    /// </summary>
    public void OnSettings()
    {
        mainPanel.SetActive(false);
        settingsPanel.SetActive(true);
    }

    /// <summary>
    /// Callback for the quit button. Brings up the dialog for confirmation and loads the main scene if confirmed.
    /// </summary>
    public void OnQuit()
    {
        DialogConfig config = new DialogConfig
        {
            Message = "Are you sure you want to return to the Main Menu?",
            OK = new DialogButton(
                text: "Yes",
                onClick: ReturnToMainMenu
            ),
            Cancel = new DialogButton(
                text: "Cancel"
            )
        };

        dialog.Show(config);
    }


    /// <summary>
    /// Callback for the accept button in the settings panel. Switches active panels.
    /// TODO: Split settings into a different script, maybe have the implementation in a settings object that will save all settings?
    /// </summary>
    public void OnSettingsAccept()
    {
        settingsPanel.SetActive(false);
        mainPanel.SetActive(true);
    }

    /// <summary>
    /// Callback for the brightness slider.
    /// TODO: Implement settings
    /// </summary>
    public void OnBrightnessSlider()
    {
        //
    }

    /// <summary>
    /// Callback for the dialog box. Called when exit is confirmed.
    /// </summary>
    private void ReturnToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

}
