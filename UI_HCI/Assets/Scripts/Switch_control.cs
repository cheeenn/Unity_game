﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch_control : MonoBehaviour {
    public bool on;
    public GameObject chr;
    private Color onc = Color.green;
    private Color offc = Color.red;
    public GameObject elev;
    public bool near;
    private float dist;
	// Use this for initialization
	void Start () {
        on = false;
        near = false;
        chr = GameObject.Find("Nim");
	}
	
	// Update is called once per frame
	void Update () {
        NearJudge();
        Interactive();
        Colorcontrol();
	}
    private void OnTriggerEnter(Collider other)
    {
        if ( other.gameObject.CompareTag("Star"))
        {
			Debug.Log ("Hit Switch");
            this.on = !this.on;
        }
    }
    void Colorcontrol()
    {
        this.gameObject.GetComponent<Renderer>().material.color = elev.GetComponent<elev_control>().running ? onc : offc;
    }
    void NearJudge()
    {
        dist = Vector3.Distance(this.gameObject.transform.position, chr.transform.position);
        if (dist <= 3)
        {
            near = true;
        }
        else
        {
            near = false;
        }
    }
    void Interactive()
    {
        if (this.near && Input.GetKeyDown(KeyCode.C) && !elev.GetComponent<elev_control>().running)
        {
            this.on = !this.on;
            elev.SendMessage("Changestatus");
        }
    }
}
