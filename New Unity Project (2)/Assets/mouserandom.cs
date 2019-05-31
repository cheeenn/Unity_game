using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mouserandom : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonUp(0))
        {

            GetComponent<Renderer>().material.color = new Color32(166, 5, 5, 255);
        }
    }
}
