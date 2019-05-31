using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Assignmentcode : MonoBehaviour {
    public GameObject cube1;
    public GameObject cube2;
    public GameObject cube3;
    public GameObject cube4;
    public GameObject cube5;
    public GameObject cube6;
    public GameObject cube7;
    public GameObject cube8;
    public GameObject cube9;

    //public GameObject[] cube = { 'cube1', 'cube2', 'cube3', 'cube4', cube5, cube6, cube7, cube8, cube9, };
    public float distance_1;
    public float distance_2;
    public float distance_3;
    public float distance_4;
    public float distance_5;
    public float distance_6;
    public float distance_7;
    public float distance_8;
    public float distance_9;
    public float[] distance_ = new float[8];
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButton(0))
        {

        }
        //松开鼠标右键时
        if (Input.GetMouseButtonUp(0))
        {
            //获取屏幕坐标
            Vector3 mScreenPos = Input.mousePosition;
            //定义射线
            Ray mRay = Camera.main.ScreenPointToRay(mScreenPos);
            RaycastHit mHit;

            //判断射线是否击中地面
            if (Physics.Raycast(mRay, out mHit))
            {
                GameObject gameObj = mHit.collider.gameObject;
                // int i = 0;
                //   while (i<9)
                // {

                    //distance_[0] = Vector3.Distance(gameObj.transform.position, cube1.transform.position);
                    distance_1 = Vector3.Distance(gameObj.transform.position, cube1.transform.position); //与cube1的距离
                    distance_2 = Vector3.Distance(gameObj.transform.position, cube2.transform.position);
                    distance_3 = Vector3.Distance(gameObj.transform.position, cube3.transform.position);
                    distance_4 = Vector3.Distance(gameObj.transform.position, cube4.transform.position);
                    distance_5 = Vector3.Distance(gameObj.transform.position, cube5.transform.position);
                    distance_6 = Vector3.Distance(gameObj.transform.position, cube6.transform.position);
                    distance_7 = Vector3.Distance(gameObj.transform.position, cube7.transform.position);
                    distance_8 = Vector3.Distance(gameObj.transform.position, cube8.transform.position);
                    distance_9 = Vector3.Distance(gameObj.transform.position, cube9.transform.position);
                if (mHit.collider.gameObject.name == gameObj.name)    //如果碰撞的点所在的物体的名字是“StartButton”(collider就是检测碰撞所需的碰撞器)
                    {

                        print("游戏开始！");
                        //each touch will cause change of color
                        float r = Random.Range(0f, 1f);
                        float g = Random.Range(0f, 1f);
                        float b = Random.Range(0f, 1f);
                        Color color = new Color(r, g, b,255);
                    Color colorclose = new Color32(255, 255, 255,255);
                    Color color2 = new Color32(166, 5, 5, 255);
                    //距离为1 以内的物体变h色
                    if (0 < distance_1 && distance_1 <= 1)
                        {
                            if (cube1.GetComponent<Renderer>().material.color== color2)
                            {
                                cube1.GetComponent<Renderer>().material.color = colorclose;
                            }
                            else
                            {
                                cube1.GetComponent<Renderer>().material.color = color2;
                            }
                            
                        }
                    if (0 < distance_2 && distance_2 <= 1)
                    {
                        if (cube2.GetComponent<Renderer>().material.color == color2)
                        {
                            cube2.GetComponent<Renderer>().material.color = colorclose;
                        }
                        else
                        {
                            cube2.GetComponent<Renderer>().material.color = color2;
                        }
                        //cube2.GetComponent<Renderer>().material.color = color2;
                    }
                    if (0 < distance_3 && distance_3 <= 1)
                    {
                        if (cube3.GetComponent<Renderer>().material.color == color2)
                        {
                            cube3.GetComponent<Renderer>().material.color = colorclose;
                        }
                        else
                        {
                            cube3.GetComponent<Renderer>().material.color = color2;
                        }
                        //cube3.GetComponent<Renderer>().material.color = color2;

                    }
                    if (0 < distance_4 && distance_4 <= 1)
                    {
                        if (cube4.GetComponent<Renderer>().material.color == color2)
                        {
                            cube4.GetComponent<Renderer>().material.color = colorclose;
                        }
                        else
                        {
                            cube4.GetComponent<Renderer>().material.color = color2;
                        }
                        //cube4.GetComponent<Renderer>().material.color = color2;
                    }
                    if (0 < distance_5 && distance_5 <= 1)
                    {
                        if (cube5.GetComponent<Renderer>().material.color == color2)
                        {
                            cube5.GetComponent<Renderer>().material.color = colorclose;
                        }
                        else
                        {
                            cube5.GetComponent<Renderer>().material.color = color2;
                        }
                        //cube5.GetComponent<Renderer>().material.color = color2;
                    }
                    if (0 < distance_6 && distance_6 <= 1)
                    {

                        if (cube6.GetComponent<Renderer>().material.color == color2)
                        {
                            cube6.GetComponent<Renderer>().material.color = colorclose;
                        }
                        else
                        {
                            cube6.GetComponent<Renderer>().material.color = color2;
                        }
                        //cube6.GetComponent<Renderer>().material.color = color2;
                    }
                    if (0 < distance_7 && distance_7 <= 1)
                    {
                        if (cube7.GetComponent<Renderer>().material.color == color2)
                        {
                            cube7.GetComponent<Renderer>().material.color = colorclose;
                        }
                        else
                        {
                            cube7.GetComponent<Renderer>().material.color = color2;
                        }
                        //cube7.GetComponent<Renderer>().material.color = color2;
                    }
                    if (0 < distance_8 && distance_8 <= 1)
                    {
                        if (cube8.GetComponent<Renderer>().material.color == color2)
                        {
                            cube8.GetComponent<Renderer>().material.color = colorclose;
                        }
                        else
                        {
                            cube8.GetComponent<Renderer>().material.color = color2;
                        }
                        //cube8.GetComponent<Renderer>().material.color = color2;
                    }
                    if (0 < distance_9 && distance_9 <= 1)
                    {

                        if (cube9.GetComponent<Renderer>().material.color == color2)
                        {
                            cube9.GetComponent<Renderer>().material.color = colorclose;
                        }
                        else
                        {
                            cube9.GetComponent<Renderer>().material.color = color2;
                        }
                        //cube9.GetComponent<Renderer>().material.color = color2;
                    }
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
                //   i--;
                //    print(i);
                //  }
                //  i = 0;
            }
        }
    }
}
