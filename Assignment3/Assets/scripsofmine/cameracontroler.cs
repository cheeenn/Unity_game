using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameracontroler : MonoBehaviour {
    public GameObject player;
    private Vector3 offset;
	// Use this for initialization
	void Start () {
        //记录初始化摄像机和物体之间的距离
        offset = transform.position - player.transform.position;
	}
    //The camera should follow the player from a reasonable side view.
    // Update is called once per frame
    void LateUpdate () {
        transform.position = offset + player.transform.position;
	}
}
