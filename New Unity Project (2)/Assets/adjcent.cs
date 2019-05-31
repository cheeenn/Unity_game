using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class adjcent : MonoBehaviour {
    public GameObject cube1;
    public GameObject cube2;
    public float distance_;
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
                GameObject gameObj = mHit.collider.gameObject;
                distance_ = Vector3.Distance(gameObj.transform.position, cube1.transform.position);
                if (mHit.collider.gameObject.name == gameObj.name)    //如果碰撞的点所在的物体的名字是“StartButton”(collider就是检测碰撞所需的碰撞器)
                {

                    //距离为1 以内的物体变h色
                    if (0 < distance_ && distance_ <= 1)
                    {
                        cube1.GetComponent<Renderer>().material.color = new Color32(166, 5, 5, 255);
                    }
                }
            }
        }
    }
}
