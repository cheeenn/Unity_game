using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class switchcar : MonoBehaviour {

    public Transform Player;

    public Camera FirstPersonCam;

    public Camera ThirdPersonCam;

    public KeyCode Key;

    void Start()
    {
        ThirdPersonCam.gameObject.SetActive(false);
        FirstPersonCam.gameObject.SetActive(true);

    }

    void Update()
    {
        float horizontalvalue = Input.GetAxis("Horizontal");
       // float verticalalvalue = Input.GetAxis("vertical");

        if (Input.GetKeyDown(Key))
        {
            FirstPersonCam.gameObject.SetActive(false);
            ThirdPersonCam.gameObject.SetActive(true);
        }

       // else 
      //  {
        //    FirstPersonCam.gameObject.SetActive(true);
        //    ThirdPersonCam.gameObject.SetActive(false);

       // }
        if (horizontalvalue !=0)
        {
            print("1");
            FirstPersonCam.gameObject.SetActive(true);
            ThirdPersonCam.gameObject.SetActive(false);
        }
    }
}

