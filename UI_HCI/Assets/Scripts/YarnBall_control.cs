using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YarnBall_control : MonoBehaviour
{

    public GameObject DandyLion;
    public GameObject Nim;

    // Use this for initialization
    void Start()
    {
        Nim = GameObject.Find("Nim");
        DandyLion = GameObject.Find("DandyLion");
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        //Debug.Log ("Hit Something");
        if (other.gameObject==Nim)
        {
            Debug.Log("yarn Found!");
            DandyLion.SendMessage("QuestCounterUpdate");
            Destroy(this.gameObject);
        }
    }
}
