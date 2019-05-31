using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class showname3_pickup : MonoBehaviour {
    bool mouseover = false;
    public Text names;

   // public GameObject player;
   
    private void Start()
    {
       // player.name = "player";
    }
    void OnMouseEnter()
    {
        mouseover = true;
        //names.text = player.name;
       
        names.text = "pickup";
        //If your mouse hovers over the GameObject with the script attached, output this message
        Debug.Log("Mouse is over GameObject.");
    }
    void OnMouseExit()
    {
        names.text = "";
    }

}
