using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class mMenuui : MonoBehaviour
{
   
    public void PlayButton ()
    {
        Debug.Log("Intialising Game");
        SceneManager.LoadScene("MainScene");
        Time.timeScale = 1f;
    } 
    public void QuitButton()
    {
        Debug.Log("Quit");
        Application.Quit();

    } 
    public void ReturnTomain()
    {
        SceneManager.LoadScene("menu");
    } 
     
    public void pauseButton()
    {
        Time.timeScale = 0f;
    } 
    public void continueButton()
    {
        Time.timeScale = 1f;
    } 
  
}
