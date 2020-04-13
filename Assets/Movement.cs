using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Movement : MonoBehaviour
{
    public Camera cam;
    public NavMeshAgent agent;


    void Update()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            print("hi");
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if(Physics.Raycast(ray, out hit))
            {
                agent.SetDestination(hit.point);
                print("hello");
            }
        }
    }
}
