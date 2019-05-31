using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MJInstructionControl : MonoBehaviour {

    private Object Instruction;

    private bool ShowInstruction;

    private bool HasShown;




	// Use this for initialization
	void Start () {
        Instruction = Resources.Load("Image/Instruction/MultiJumpGuide");
        ShowInstruction = false;
        HasShown = false;
    }
	
	// Update is called once per frame
	void OnGUI () {
		if(ShowInstruction && !HasShown)
        {
            Time.timeScale = 0;
            GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), (Texture)Instruction);
            if (Input.GetKeyDown(KeyCode.Return))
            {
                Time.timeScale = 1;
                GUI.DrawTexture(new Rect(0, 0, 0, 0), (Texture)Instruction);
                HasShown  = true;
            }
        }
	}

    void Display()
    {
        ShowInstruction = true;
    }
}
