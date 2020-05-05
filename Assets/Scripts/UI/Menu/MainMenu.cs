using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private DialogSystem dialog;

    // TODO: Use async operations and a loading screen to change scenes

    void Start()
    {
        dialog = transform.Find("Dialog").GetComponent<DialogSystem>();
    }

    public void OnPlay()
    {
        Debug.Log("Pressed Play. Loading MainScene.");
        SceneManager.LoadScene("MainScene");
        Time.timeScale = 1f;

        SceneManager.UnloadSceneAsync("MainMenu");
    }

    public void OnHelp()
    {
        // Show help
    }

    public void OnSettings()
    {
        // Show Settings
    }

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
