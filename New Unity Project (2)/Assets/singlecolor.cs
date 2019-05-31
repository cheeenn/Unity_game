using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class singlecolor : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

    

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
                Debug.Log(mHit.collider.name);
                print(mHit.collider.gameObject);

                print(mHit.collider.name);
                print(mHit.point);
                if (mHit.collider.gameObject.name == "Cube (11)")    //如果碰撞的点所在的物体的名字是“StartButton”(collider就是检测碰撞所需的碰撞器)
                {

                    print("游戏开始！");
                    //each touch will cause change of color
                    float r = Random.Range(0f, 1f);
                    float g = Random.Range(0f, 1f);
                    float b = Random.Range(0f, 1f);
                    Color color = new Color(r, g, b);
                    
                    GameObject gameObj = mHit.collider.gameObject;


                    //GetComponent<Renderer>().material.color = color;
                    gameObj.GetComponent<Renderer>().material.color = color;
                    // GetComponent<Renderer>().material.color = new Color32(166, 5, 5, 255);
                }
                if (mHit.collider.gameObject.name == gameObject.name)    //如果碰撞的点所在的物体的名字是“StartButton”(collider就是检测碰撞所需的碰撞器)
                {

                    print("nice choice！");
                    //each touch will cause change of color
                    float r = Random.Range(0f, 1f);
                    float g = Random.Range(0f, 1f);
                    float b = Random.Range(0f, 1f);
                    Color color = new Color(r, g, b);
                    
                    GameObject gameObj = mHit.collider.gameObject; //定义射线所到物体
                    //GetComponent<Renderer>().material.color = color;//共享代码的所有物体集体变色
                    gameObj.GetComponent<Renderer>().material.color = color;
                    // GetComponent<Renderer>().material.color = new Color32(166, 5, 5, 255);//相同代码物体改变成指定颜色
                }
                if (mHit.collider.gameObject.name == "Cube (10)")    //如果碰撞的点所在的物体的名字是“StartButton”(collider就是检测碰撞所需的碰撞器)
                {

                    print("game restart！");
                    //each touch will cause change of color
                    //float r = Random.Range(0f, 1f);
                    //float g = Random.Range(0f, 1f);
                    //float b = Random.Range(0f, 1f);
                    //Color color = new Color(r, g, b);
                    //gameObject.c.renderer.material.color = color;
                    //GetComponent<Renderer>().material.color = color;

                    GetComponent<Renderer>().material.color = new Color32(255, 255, 255, 255);
                }

            }
        }


    }
}
