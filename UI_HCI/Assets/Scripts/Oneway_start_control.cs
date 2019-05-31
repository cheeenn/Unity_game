using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oneway_start_control : MonoBehaviour {
    public GameObject end;
	// Use this for initialization
	void Start () {
        end.GetComponent<Transparent_control>().enabled = false;
        end.GetComponent<Renderer>().material.color = this.GetComponent<Renderer>().material.color;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            end.GetComponent<Collider>().enabled = false;
            end.GetComponent<Transparent_control>().enabled = true;

        }
    }
}
