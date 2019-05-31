using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeControlWithoutSwitch : MonoBehaviour {

	public GameObject Player;
	private bool up;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
	}

	void OnTriggerEnter (Collider other){
		if (other.transform.tag == "Player") {
			Player.SendMessage ("death");
		}
	}

	void Moveup()
	{
		for(int i = 0; i < 2000; i++)
		{
			this.gameObject.transform.position = this.gameObject.transform.position+new Vector3(0,0.001F,0);
		}
	}
	void Movedown()
	{
		for(int i = 0; i < 2000; i++)
		{
			this.gameObject.transform.position = this.gameObject.transform.position + new Vector3(0, -0.001F, 0);
		}
	}
}
