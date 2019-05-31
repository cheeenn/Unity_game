using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotator : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        //如果数字的话，转速会非常快
        //所以使用Time.deltaTime
        transform.Rotate(new Vector3(15, 30, 45)* Time.deltaTime);
	}
}
