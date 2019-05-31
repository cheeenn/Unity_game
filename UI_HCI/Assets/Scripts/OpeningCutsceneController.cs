using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class OpeningCutsceneController : MonoBehaviour {
    Object[] frames;
    private int currentFrame;
    private float timeBetween;
    private float lastButtonTime;

	// Use this for initialization
	void Start () {
        currentFrame = 0;
        timeBetween = 0.5f;
        lastButtonTime = Time.time;
        frames = Resources.LoadAll("Image/OpeningCast", typeof(Texture2D));
        
        Debug.Log("yes");
        Debug.Log(frames.Length);
	}
	
	// Update is called once per frame
	void OnGUI () {
        if (currentFrame < frames.Length)
        {
            GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), (Texture)frames[currentFrame]);
            if (Input.GetKeyUp(KeyCode.Return)&&Time.time-lastButtonTime>timeBetween)
            {
                lastButtonTime = Time.time;
                Debug.Log(currentFrame);
                GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), (Texture) frames[currentFrame]);
                currentFrame++;
            }
        }
		
	}
}
