using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicOperationControl : MonoBehaviour {

	private Object Instruction;
	private GameObject Nim;
	private bool IsDisplay;
	private bool near;

	// Use this for initialization
	void Start () {
		Instruction = Resources.Load ("Image/Instruction/BasicOperation");
		Nim = GameObject.Find ("Nim");
		IsDisplay = false;
		near = false;
	}
	
	// Update is called once per frame
	void OnGUI () {
		NearJudge ();
		if (near && (!IsDisplay)) {
			// Display the instruction
			Time.timeScale = 0;
			GUI.DrawTexture(new Rect(0, 0, Screen.width,Screen.height), (Texture)Instruction);
			if (Input.GetKeyDown (KeyCode.Return)) {
				Time.timeScale = 1;
				GUI.DrawTexture(new Rect(0, 0, 0,0), (Texture)Instruction);
				IsDisplay = true;
			}


		}
	}

	void NearJudge()
	{
		float dist = Vector3.Distance(this.gameObject.transform.position, Nim.transform.position);
		if (dist <= 4 && this.gameObject.transform.position[1] - Nim.transform.position[1] < 5)
		{
			near = true;
		}
		else
		{
			near = false;
		}
	}
}
