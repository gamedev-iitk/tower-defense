using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Controls the wave timer on screen.
/// </summary>
public class WaveTimerUI : MonoBehaviour
{
    private GameObject gameManager;
    private Text stateHeading;
    private Text timerText;

    void Awake()
    {
        gameManager = GameObject.Find("GameManager");
        stateHeading = transform.Find("StateName").GetComponent<Text>();
        timerText = transform.Find("Timer").GetComponent<Text>();
    }

    /// <summary>
    /// Set the heading on the screen.
    /// </summary>
    /// <param name="textHeading">String to be set as heading.</param>
    public void SetHeading(string textHeading)
    {
        stateHeading.text = textHeading;
    }

    /// <summary>
    /// Set the text on the timer.
    /// </summary>
    /// <param name="textTimer">String to be set on the timer.</param>
    public void SetTimer(string textTimer)
    {
        timerText.text = textTimer;
    }

}
