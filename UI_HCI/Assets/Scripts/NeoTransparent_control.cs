using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeoTransparent_control : MonoBehaviour
{
    private bool near;
    public GameObject chr;
    public float dist;
    public Color init;
    private Color tran;
    // Use this for initialization
    void Start()
    {
        this.GetComponent<Renderer>().material.color = init;
        init = GetComponent<Renderer>().material.color;
        tran = new Color(init[0], init[1], init[2], 255 / 255F);
        chr = GameObject.Find("Nim");
    }

    // Update is called once per frame
    void Update()
    {
        float alp = (near ? dist / 6F * 255 : 255) / 255F - 191 / 255F;
        tran.a = alp > 0 ? alp : 0;
        NearJudge();
        if (near) this.GetComponent<Renderer>().material.color = tran;
        else this.GetComponent<Renderer>().material.color = init;
        // print("alp= "+alp);
    }
    void NearJudge()
    {
        dist = Vector3.Distance(this.gameObject.transform.position, chr.transform.position);
        if (dist <= 6 && this.gameObject.transform.position[1] - chr.transform.position[1] < 5)
        {
            near = true;
        }
        else
        {
            near = false;
        }
    }
}
