using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class settingUI : MonoBehaviour

{
    string pname;
    public Light sunLight;
    GameObject sliderInitialValue;
    void start()
    {
        sunLight = GetComponent<Light>();
      

    }
   public void setBrightness(float sliderValue)
    {
        
        sunLight.intensity = sliderValue * 2;
    } 
    public void getName(string name)
    {
        pname = name;
    }
}
