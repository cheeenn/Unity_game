using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class betterjump : MonoBehaviour {
    public float fillmultiplier = 2.5f;
    public float lowjumpmultiplier = 2f;
    private Rigidbody rb;
    public KeyCode Key;
    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody>();
    }
	
	// Update is called once per frame
	void Update () {

        if (rb.velocity.y < 0)//代表y轴负数，下落
        {
            // - 1 是因为系统初始有1的自重
            print(Vector3.up);//(0,1,0)

            rb.velocity += Vector3.up * Physics.gravity.y * (fillmultiplier - 1) * Time.deltaTime;
            print("到了");
        }
        else if (rb.velocity.y > 0 && !Input.GetKeyDown(Key)) //如果跳起时，跳跃没有长按，则为小跳
        {
            rb.velocity += Vector3.up * Physics.gravity.y * (lowjumpmultiplier - 1) * Time.deltaTime;
        }
    }
}
