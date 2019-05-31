using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tranparent_test : MonoBehaviour {
    public Color init;
    public Color tran;
	// Use this for initialization
	void Start () {
        tran = new Color(init[0], init[1], init[2], 0 / 255F);
        this.GetComponent<Renderer>().material.color = init;
    }
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.V))
        {
            this.GetComponent<Renderer>().material.color = tran;
            Debug.Log("changed");
        }
    }
}
