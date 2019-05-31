using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeControl : MonoBehaviour {

	public GameObject Player;
	public GameObject kaiguan;

	// Private Variables
	private bool up;
	public double UpLength;

	// Use this for initialization
	void Start () {
		UpLength = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if (kaiguan.GetComponent<Switch_control>().on && !up)
		{
			this.gameObject.transform.position = this.gameObject.transform.position+new Vector3(0,0.1F,0);
			UpLength = UpLength + 0.1f;
			if (UpLength >= 2f)
				up = true;
		}
		else if(!kaiguan.GetComponent<Switch_control>().on && up)
		{
			Movedown();
			up = false;
		}
	}

	void OnCollisionEnter (Collision other){
		if (other.collider.tag == "Player") {
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
