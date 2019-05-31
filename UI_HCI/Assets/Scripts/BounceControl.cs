using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceControl : MonoBehaviour {
	public float BounceForce;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	}

	void OnCollisionEnter (Collision other){
		if (other.collider.tag == "Player") {
			Vector3 BounceJump = new Vector3 (0.0f,BounceForce,0.0f);
			other.rigidbody.AddForce (BounceJump);
		}
	}
}
