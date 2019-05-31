using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Neoswitch_control : MonoBehaviour {
	private float dist;
	public GameObject chr;
	private bool near;
    public bool on;// = true when switch is used
    public bool open;// = true when pointing to right
    public bool canpress;
    //public Transform right;
    //public Transform left;
    public Transform ax;
	private int angle_counter;
    //public Quaternion l = new Quaternion(0, 0, 0.3F, 1.0F);
    //public Quaternion r = new Quaternion(0, 0, -0.3F, 1.0F);
	// Use this for initialization


	void Start () {
        on = false;
        open = false;
        canpress = true;
       // Debug.Log("r:"+right.rotation + "l:" + left.rotation);
	}

	
	// Update is called once per frame
	void FixedUpdate () {
		NearJudge ();
		if (near) {
			if (Input.GetKeyDown (KeyCode.C)&&canpress) {
				on = !on;
                canpress = false;
			}
			if (on != open) {
				Rotate ();
			}
			if (angle_counter >= 12) {
				open = !open;
				angle_counter = 0;
                canpress = true;
			}
		}
	}
    void Rotate()
    {
        if (on)
        {
            //this.gameObject.transform.rotation = Quaternion.RotateTowards(this.transform.rotation, right.rotation, 360 * Time.deltaTime);
			this.gameObject.transform.RotateAround(ax.position,Vector3 .forward, (transform.rotation[2]+transform.rotation[3]>0?1:(-1))*-240 * Time.deltaTime);
			angle_counter++;
        }
        else
        {
            //this.gameObject.transform.rotation = Quaternion.RotateTowards(this.transform.rotation, left.rotation, 360 * Time.deltaTime);
			this.gameObject.transform.RotateAround(ax.position, Vector3.forward, (transform.rotation[2]+transform.rotation[3]>0?1:(-1))*240 * Time.deltaTime);
			angle_counter++;
        }
    }

	void NearJudge()
	{
		dist = Vector3.Distance(this.gameObject.transform.position, chr.transform.position);
		if (dist <= 4 && this.gameObject.transform.position[1] - chr.transform.position[1] < 5)
		{
			near = true;
		}
		else
		{
			near = false;
		}
	}

	void OnTriggerEnter(Collider other){
		//Debug.Log ("HitSomething");
		if (other.tag == "Star"||other.tag == "bullet") {
			Debug.Log ("Chage");
			on = !on;
		}
	}
}
