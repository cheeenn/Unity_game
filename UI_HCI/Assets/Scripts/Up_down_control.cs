using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Up_down_control: MonoBehaviour
{
    public bool up;
    public bool on;
    //public GameObject sw;
    public float height;
    private int counter;
    private Vector3 bottom;
    private Vector3 top;
    private Vector3 current;
    //private Color color_wall;
    //private int alpha;
    // Use this for initialization
    void Start()
    {
        this.up = false;
        this.on = true;
        top = new Vector3(transform.position[0], transform.position[1] + height, transform.position[2]);
        bottom = this.transform.position;
        this.transform.position = bottom;
        current = bottom;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (on && !up)
        {
            transform.position = Vector3.MoveTowards(transform.position, top, 5F * Time.deltaTime);

        }
        else if (!on && up)
        {
            transform.position = Vector3.MoveTowards(transform.position, bottom, 5F * Time.deltaTime);
        }
        Statejudge();

    }
    void Statejudge()
    {
        if (this.transform.position == top)
        {
            up = true;
            Changestatus();
            //sw.GetComponent<Neoswitch_control>().enabled = true;
        }
        else if (this.transform.position == bottom)
        {
            up = false;
            Changestatus();
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
