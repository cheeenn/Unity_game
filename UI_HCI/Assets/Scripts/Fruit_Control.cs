using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruit_Control : MonoBehaviour {

	public GameObject Pancake;
	public GameObject Nim;

	// Use this for initialization
	void Start () {
		Nim = GameObject.Find ("Nim");
		Pancake = GameObject.Find("Pancake");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider other){
		//Debug.Log ("Hit Something");
		if (other.gameObject.CompareTag(Nim.tag)){
			Debug.Log ("Eat One Fruit");
			Pancake.SendMessage ("QuestCounterUpdate");
			Destroy (this.gameObject);
		}
	}
}
