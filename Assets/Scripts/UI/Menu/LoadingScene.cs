using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// Handles loading scene behavior.
/// </summary>
public class LoadingScene : MonoBehaviour
{
    private Image progressBar;

    void Start()
    {
        progressBar = GameObject.Find("BarFill").GetComponent<Image>();
        StartCoroutine(LoadAsyncOperation());
    }

    ///<summary>
    /// Load the MainScene and update progress bar.
    /// </summary>
    IEnumerator LoadAsyncOperation()
    {
        AsyncOperation gameLevel = SceneManager.LoadSceneAsync("MainScene");
        while (gameLevel.progress < 1)
        {
            progressBar.fillAmount = gameLevel.progress;
            yield return new WaitForEndOfFrame();
        }
    }
}
