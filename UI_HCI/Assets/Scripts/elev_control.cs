using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class elev_control : MonoBehaviour {
    public bool up;
    public bool on;
    public bool running;
    //public GameObject sw;
    public float height;
    private int counter;
    private Vector3 bottom;
    private Vector3 top;
    private Vector3 current;
    public GameObject wall_l;
    public GameObject wall_r;
    //private Color color_wall;
    //private int alpha;
	// Use this for initialization
	void Start () {
        this.up = false;
        this.on = false;
        top = new Vector3(transform .position [0], transform .position [1]+height, transform.position [2]);
        bottom = this.transform .position ;
        this.transform.position = bottom;
        current = bottom ;
        wall_l = GameObject.Find(this.gameObject.name+"/wall_left");
        wall_r = GameObject.Find(this.gameObject.name+"/wall_right");
        //wall_l.GetComponent<BoxCollider>().enabled = false;
        //wall_r.GetComponent<BoxCollider>().enabled = false;
        wall_l.SetActive(false);
        wall_r.SetActive(false);
        running = false;
        //color_wall = new Color(131/255F,131/255F,225/255F,0);
        //wall_l.GetComponent<Renderer>().material.color = color_wall;
        //wall_r.GetComponent<Renderer>().material.color = color_wall;
        //alpha = 0;  
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        if (on && !up)
        {
            transform.position = Vector3.MoveTowards(transform.position, top , 5F * Time.deltaTime);
            wall_l.SetActive(true);
            wall_r.SetActive(true);
            running = true;
            //wall_l.GetComponent<BoxCollider>().enabled = true;
            //wall_r.GetComponent<BoxCollider>().enabled = true;
            //sw.GetComponent<Neoswitch_control>().enabled = false;
            //alpha = alpha + 16;

        }
        else if (!on && up)
        {
            transform.position = Vector3.MoveTowards(transform.position, bottom, 5F * Time.deltaTime);
            wall_l.SetActive(true);
            wall_r.SetActive(true);
            running = true;
            //wall_l.GetComponent<BoxCollider>().enabled = true;
            //wall_r.GetComponent<BoxCollider>().enabled = true;
            //sw.GetComponent<Neoswitch_control>().enabled = false;
            //alpha = alpha + 16;
        }
        Statejudge();
       // color_wall.a = alpha;
        //wall_l.GetComponent<Renderer>().material.color = color_wall;
        //wall_r.GetComponent<Renderer>().material.color = color_wall;

    }
    void Statejudge()
    {
        if(this.transform .position == top)
        {
            up = true;
            running = false;
            //wall_l.GetComponent<BoxCollider>().enabled = false;
            //wall_r.GetComponent<BoxCollider>().enabled = false;
            wall_l.SetActive(false);
            wall_r.SetActive(false);
            //sw.GetComponent<Neoswitch_control>().enabled = true;
        }
        else if(this.transform .position == bottom)
        {
            up = false;
            running = false;
            wall_l.SetActive(false);
            wall_r.SetActive(false);
            //wall_l.GetComponent<BoxCollider>().enabled = false;
            //wall_r.GetComponent<BoxCollider>().enabled = false;
            //sw.GetComponent<Neoswitch_control>().enabled = true;
        }
    }
    void Changestatus()
    {
        on = !on;
    }

}
