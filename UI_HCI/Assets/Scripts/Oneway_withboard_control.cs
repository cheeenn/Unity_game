using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oneway_withboard_control : MonoBehaviour
{
    public GameObject end;
    public GameObject board;
    // Use this for initialization
    void Start()
    {
        end.GetComponent<Transparent_control>().enabled = false;
        end.GetComponent<Renderer>().material.color = this.GetComponent<Renderer>().material.color;
        board.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            end.GetComponent<Collider>().enabled = false;
            end.GetComponent<Transparent_control>().enabled = true;
            board.SetActive(true);
        }
    }
}
