using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowController : MonoBehaviour {

    GameObject player;
    public float angle;
    public Vector3 movementDirection;
    // Use this for initialization
    void Start()
    {
        player = GameObject.Find("Nim");
        GameObject.Find("Arrow").GetComponent<Renderer>().enabled = false;

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = player.transform.position + new Vector3(0, 0, .1f);
        movementDirection = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
        if (movementDirection.magnitude > 0)
        {
            angle = Vector3.Angle(Vector3.up, movementDirection);
            transform.rotation = Quaternion.Euler(0, 0, -1 * Mathf.Sign(Input.GetAxis("Horizontal")) * angle);
        }

    }
}
