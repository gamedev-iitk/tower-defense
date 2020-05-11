using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingScene : MonoBehaviour
{
    [SerializeField]
    private Image _progressbar;
    void Start()
    {
        StartCoroutine(LoadAsyncOperation());
    }

    IEnumerator LoadAsyncOperation()
    {
        AsyncOperation gameLevel = SceneManager.LoadSceneAsync("MainMenu");
        ///<summary>
        ///Checks progress of loading up of the fame level
        /// </summary>

        while (gameLevel.progress < 1)
        {
            _progressbar.fillAmount = gameLevel.progress;
            yield return new WaitForEndOfFrame();
        }
    }
}
