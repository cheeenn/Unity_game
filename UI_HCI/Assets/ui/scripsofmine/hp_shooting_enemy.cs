using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class hp_shooting_enemy : MonoBehaviour {

    public Image enemy_hpbar;
    public EnemyAIShoot enermyai;
    public float fillamount;
    private Vector3 hpbar_position;


    //private Vector3 rotation;
    //public Transform target;
    //public Camera cam;


    // Use this for initialization
    void Start()
    {
        //enemy_hpbar.transform.position = enermyai.gameObject.transform.position;
        hpbar_position = new Vector3(enermyai.gameObject.transform.position.x, enermyai.gameObject.transform.position.y + 1.5f, 0);

        enemy_hpbar.transform.position = hpbar_position;

    }

    // Update is called once per frame
    void Update()
    {
        //Vector3 screenPos = cam.WorldToScreenPoint(target.position);
        //Debug.Log("target is " + screenPos.x + " pixels from the left");
        //enemy_hpbar.transform.position = enermyai.gameObject.transform.position;
        hpbar_position = new Vector3(enermyai.gameObject.transform.position.x, enermyai.gameObject.transform.position.y + 1.5f, 0);
        enemy_hpbar.transform.position = hpbar_position;
        setfillamount();
        handlehp();
        print("active?" + enermyai.gameObject.activeInHierarchy);
    }

    public void handlehp()
    {
        print("enermy hp" + enermyai.health);



        if (enermyai.health == 3)
        {
            fillamount = 1f;
        }
        if (enermyai.health == 2)
        {
            fillamount = 1f;
        }
        else if (enermyai.health == 1)
        {
            fillamount = 0.5f;
        }




        //fillamount = fillamount * (enermyai.health / 3f);
    }

    public void setfillamount()
    {
        enemy_hpbar.fillAmount = fillamount;
    }
}

