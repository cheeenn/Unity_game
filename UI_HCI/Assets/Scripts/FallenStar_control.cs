using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallenStar_control : MonoBehaviour
{

    public GameObject StarHat;
    public GameObject Nim;

    // Use this for initialization
    void Start()
    {
        Nim = GameObject.Find("Nim");
        StarHat = GameObject.Find("StarHat");
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        //Debug.Log ("Hit Something");
        if (other.gameObject.CompareTag(Nim.tag))
        {
            Debug.Log("Eat One Fruit");
            StarHat.SendMessage("QuestCounterUpdate");
            Nim.SendMessage("HiddenUpdate");
            Destroy(this.gameObject);
        }
    }
}
