using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    public void SetHeading(string textHeading)
    {
        stateHeading.text = textHeading;
    }

    public void SetTimer(string textTimer)
    {
        timerText.text = textTimer;
    }

}
