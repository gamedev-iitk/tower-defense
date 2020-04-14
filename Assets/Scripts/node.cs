using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class node : MonoBehaviour {
  public Color hovercolor;

  private Renderer rend;
  private Color startcolor;
  private Collider collider;
  private int key = 1;
  // Start is called before the first frame update
  void Start() {
    rend = GetComponent<Renderer>();
    startcolor = rend.material.color;
    collider = GetComponent<Collider>();
  }

  void OnTriggerEnter(Collider other) { key = 0; }

  void OnMouseEnter() {
    if (key == 1) {
      rend.material.color = hovercolor;
    }

    else {
      rend.material.color = Color.red;
    }
  }

  void OnMouseExit() { rend.material.color = startcolor; }
}
