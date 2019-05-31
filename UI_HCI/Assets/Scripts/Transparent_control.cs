using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transparent_control : MonoBehaviour {
    private bool near;
    public GameObject chr;
    public float dist;
    public Color init ;
    private Color tran ;
    // Use this for initialization
    void Start () {
        this.GetComponent<Renderer>().material.color = init;
        init = GetComponent<Renderer>().material.color;
        tran = new Color(init[0], init[1], init[2], 255 / 255F);
        chr = GameObject.Find("Nim");
    }
	
	// Update is called once per frame
	void Update () {
        float alp = (near ? dist / this.transform.lossyScale.x * 255 : 255) / 255F - 31 / 255F;
        tran.a = alp>0?alp:0;
        NearJudge();
        if (near) this.GetComponent<Renderer>().material.color = tran;
        else this.GetComponent<Renderer>().material.color = init;
       // print("alp= "+alp);
    }
    void NearJudge()
    {
        dist = Vector3.Distance(this.transform.position,chr.transform.position);
        if (Mathf.Abs(this.gameObject.transform.position.x - chr.transform.position.x) <= this.transform.lossyScale.x && Mathf.Abs(this.gameObject.transform.position.y - chr.transform.position.y) <= this.transform.lossyScale.y)
        {
            near = true;
        }
        else
        {
            near = false;
        }
    }
}
