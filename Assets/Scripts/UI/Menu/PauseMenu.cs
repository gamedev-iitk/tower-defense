using UnityEngine;
using UnityEngine.SceneManagement;

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

    public void Show(GameObject obj)
    {
        Show();
    }

    public void Show()
    {
        Time.timeScale = 0;
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void OnResume()
    {
        Time.timeScale = 1;
        Hide();
    }

    public void OnSettings()
    {
        mainPanel.SetActive(false);
        settingsPanel.SetActive(true);
    }

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


    // TODO: Split settings into a different script, maybe have the implementation in a settings object that will save all settings?
    public void OnSettingsAccept()
    {
        settingsPanel.SetActive(false);
        mainPanel.SetActive(true);
    }

    public void OnBrightnessSlider()
    {
        //
    }


    private void ReturnToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

}
