using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class quit : MonoBehaviour {

    public void OnApplicationQuit()
    {
#if UNITY_EDITOR 
        UnityEditor.EditorApplication.isPlaying = false;

#else
        Application.Quit();
#endif
    }
}
