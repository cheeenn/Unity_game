using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Neocier_control : MonoBehaviour {
    public GameObject kaiguan;
    private float y;
    public bool down;
	// Use this for initialization
	void Start () {
        y = transform.position[1];
        down = false;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        if(kaiguan.GetComponent<Neoswitch_control>().on != down){
            Move();
            Positionjudge();
        }
	}
    void Move()
    {
        if (!down)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position[0], transform.position[1]  - 2, transform.position[2]), 10 * Time.deltaTime);
            //gameObject.SetActive(false);
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position[0], transform.position[1] + 2, transform.position[2]), 10 * Time.deltaTime);
           // gameObject.SetActive(true);
        }
    }
    void Positionjudge()
    {
        if(transform.position[1] <= y - 2)
        {
            down = true;
        }
        else if(transform.position[1] >= y)
        {
            down = false;
        }
    }
}
