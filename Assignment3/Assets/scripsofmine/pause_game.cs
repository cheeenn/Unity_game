using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pause_game : MonoBehaviour {

    public Transform canvas;
    public Transform canvas2;
    // Update is called once per frame
    void Update () {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pause();
        }

    }
	
    public void pause()
    {
        if (canvas.gameObject.activeInHierarchy == false)
        {
            canvas.gameObject.SetActive(true);
            //时间静止
            Time.timeScale = 0;
        }
     
        else
        {
            canvas.gameObject.SetActive(false);
            //时间恢复
            Time.timeScale = 1;
        }
    }
}
