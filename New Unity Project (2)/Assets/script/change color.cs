using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class changecolor : MonoBehaviour {

	// Use this for initialization
	void Start () {

        for (int i = 0; i < 10; i += 2)
        {
            GameObject obj = GameObject.CreatePrimitive(PrimitiveType.Cube);
            obj.transform.position = new Vector3(i, 0, 0);
            obj.GetComponent<MeshRenderer>().material.color = Color.red;

        }

    }

    // Update is called once per frame
    void Update () {
		
	}
}
