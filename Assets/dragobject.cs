using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dragobject : MonoBehaviour
{
    
    bool trigg = false;
    private float mZCoord;
    private Vector3 mOffset;
    Renderer rend;
    int colorPicker = 4;
    void Start()
    {   
        rend = GetComponent<Renderer>();


    }

    void OnMouseDown()
    {

        mZCoord = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
        mOffset =gameObject.transform.position  - GetMouseWorldPos();


    }

    private Vector3 GetMouseWorldPos()
    {
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = mZCoord;
        return Camera.main.ScreenToWorldPoint(mousePoint);
    }
    void OnMouseDrag()
    { Vector3 coord = GetMouseWorldPos();
        Vector3 coord1 = new Vector3(coord.x, 5.28f, coord.z) + new Vector3(mOffset.x, 2f, mOffset.z);
        transform.position = coord1;
        
        if (trigg) { colorPicker = 1; }
        else colorPicker = 0;
        

    }
    void OnTriggerEnter(Collider other)
    {

        trigg = true;
            colorPicker = 1;
        


    }
     void OnMouseUp()
     {


         colorPicker = 3;
     }
     void OnTriggerExit(Collider other)
     {
        trigg = false;
           colorPicker = 0;

     }  

         void Update()
         {
       
         switch (colorPicker)
         {
             case 0: rend.material.color = Color.green; break;
             case 1: rend.material.color = Color.red; break;

         default: rend.material.color = Color.white; break;
     }

 }
}
