using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dragmesh : MonoBehaviour
{

    bool trigg = false;
    private float mZCoord;
    private Vector3 mOffset;
    public GameObject plane;
    Renderer rend;
    int colorPicker = 4;
    void Start()
    {
        rend = GetComponent<Renderer>();

    }

    void OnMouseDown()
    {

        mZCoord = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
        mOffset = gameObject.transform.position - GetMouseWorldPos();


    }

    private Vector3 GetMouseWorldPos()
    {
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = mZCoord;
        return Camera.main.ScreenToWorldPoint(mousePoint);
    }
    void OnMouseDrag()
    {
        Vector3 coord = GetMouseWorldPos();
        Vector3 coord1 = new Vector3(coord.x, 5.28f, coord.z) + new Vector3(mOffset.x, 2f, mOffset.z);
        Vector3 coord2 = new Vector3(coord.x, 0.01f, coord.z) + new Vector3(mOffset.x, 0, mOffset.z);
        transform.position = coord1;
        plane.transform.position = coord2;

        if (trigg) {
            colorPicker = 1;
        }
        else colorPicker = 0;


    }


}
