using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class mmenuui : MonoBehaviour
{
   
    public void PlayButton ()
    {
        Debug.Log("Intialising Game");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex +1);
        Time.timeScale = 1f;
    } 
    public void QuitButton()
    {
        Debug.Log("Quit");
        Application.Quit();

    } 
    public void ReturnTomain()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex -1 );
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
