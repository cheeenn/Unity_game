using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class mouse : MonoBehaviour {
    //目标点坐标
    private Vector3 mTargetPos;
    // Use this for initialization
    void Start () {
    
    }
	
	// Update is called once per frame
	void Update () {
        // if (Input.GetMouseButton(0))
        //  {

        //      GetComponent<Renderer>().material.color = new Color32(166, 5, 5, 255);
        //click鼠标右键时
        if (Input.GetMouseButton(1))
        {

        }
        //松开鼠标右键时
        if (Input.GetMouseButtonUp(1))
            {
                //获取屏幕坐标
                Vector3 mScreenPos = Input.mousePosition;
                //定义射线
                Ray mRay = Camera.main.ScreenPointToRay(mScreenPos);
                RaycastHit mHit;
                //判断射线是否击中地面
                if (Physics.Raycast(mRay, out mHit))
                {
                    Debug.Log(mHit.point);
                    print(mHit.collider.gameObject);
                    print(mHit.point);
          
                    float r = Random.Range(0f, 1f);
                    float g = Random.Range(0f, 1f);
                    float b = Random.Range(0f, 1f);
                    Color color = new Color(r, g, b);

                GetComponent<Renderer>().material.color = color;
               // GetComponent<Renderer>().material.color = new Color32(166, 5, 5, 255);
                // if (mHit.point == mHit.collider.transform.position)
                //   {
                //       GetComponent<Renderer>().material.color = new Color32(166, 5, 5, 255);
                //    }
            }
            }
           
 
     //   }
    //
    }
  }
