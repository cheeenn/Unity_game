using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class color : MonoBehaviour {

	// Use this for initialization
	void Start () {

     
    }
	
	// Update is called once per frame
	void Update () {
        GetComponent<Renderer>().material.color = new Color32(166, 5, 5, 255);
    }
}
