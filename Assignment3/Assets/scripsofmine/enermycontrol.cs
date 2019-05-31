using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enermycontrol : MonoBehaviour {


    public GameObject enermy;
 
    private Rigidbody rb1;
    private Vector3 conti;
    public float speed1;
    private Vector3 start_pos;
    private Vector3 end_pos;
    private float distance =20f;
    //从开始位置到目标位置所需时间
    private float lerptime = 5;

    private float currentlerptime = 0;


    // Use this for initialization
    void Start () {
        start_pos = enermy.transform.position;
        end_pos = enermy.transform.position+Vector3.left*distance;
        rb1 = enermy.GetComponent<Rigidbody>();
    
       

    }
	
	// Update is called once per frame
	void Update () {

        currentlerptime += Time.deltaTime;
        if (currentlerptime >= lerptime)
        {
            currentlerptime = lerptime;
 
        }
        float perc = currentlerptime / lerptime;
        enermy.transform.position = Vector3.Lerp(start_pos,end_pos,perc);
        //print(enermy.transform.position);
        if (enermy.transform.position == end_pos)
        {
            conti = start_pos;
            start_pos = end_pos;
            end_pos = conti;
            currentlerptime = 0;
        }
        //rb1.velocity = transform.forward * speed1;
        // rb2.velocity = transform.forward * speed2;
    }
}
