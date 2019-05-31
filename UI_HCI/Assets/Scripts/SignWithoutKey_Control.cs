using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignWithoutKey_Control : MonoBehaviour {

	// pubilc variable
	public string SignImageName;


	// public variable
	//public GameObject SignImage;
	public GameObject chr;

	// private variable
	private  float dist;
	private bool near;
	public Object SignImage;
	private bool show;

	// Use this for initialization
	void Start () {
		near = false;
		show = false;

		SignImage = Resources.Load ("Image/Instruction/"+SignImageName);
		chr = GameObject.Find ("Nim");
	}

	// Update is called once per frame
	void OnGUI () {
		NearJudge();
		if (near)
			show = true;
		else {
			show = false;
		}

		if (show) {
			GUI.DrawTexture(new Rect(0, 0, Screen.width,Screen.height), (Texture)SignImage);
		}else {
			GUI.DrawTexture(new Rect(0, 0, 0, 0), (Texture)null);
		}

		Debug.Log (chr.name);
	}


	void NearJudge()
	{
		dist = Vector3.Distance(this.gameObject.transform.position, chr.transform.position);
		if (dist <= 1.5 && this.gameObject.transform.position[1] - chr.transform.position[1] < 3)
		{
			near = true;
		}
		else
		{
			near = false;
		}
	}
}
