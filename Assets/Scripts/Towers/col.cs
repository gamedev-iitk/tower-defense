using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class col : MonoBehaviour
{
    Renderer rend;
    int colorPicker = 4;
    bool trigg = false;
    void Start()
    {
        rend = GetComponent<Renderer>();

    }
    // Start is called before the first frame update
    void OnTriggerEnter(Collider other)
    {

        trigg = true;
        colorPicker = 1;



    }

    void OnTriggerExit(Collider other)
    {
        trigg = false;
        colorPicker = 0;

    }

    void Update()
    {
       // rend.material.color.a = 0.3f;

        switch (colorPicker)
        {
            case 0: rend.material.color = Color.green; break;
            case 1: rend.material.color = Color.red; break;

            default: rend.material.color = Color.white; break;
        }

    }
}
