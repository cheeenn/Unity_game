using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class distance : MonoBehaviour {
    public GameObject cube1;
    public GameObject cube2;
    public float distance_ ;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        distance_ = Vector3.Distance(cube1.transform.position,cube2.transform.position);
	}
}
