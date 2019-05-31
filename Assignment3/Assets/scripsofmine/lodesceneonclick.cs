using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.SceneManagement;

public class lodesceneonclick : MonoBehaviour {
    public Transform canvas;

    public void Loadbyindex(int sceneindex)
    {
        EditorSceneManager.LoadScene(sceneindex);
        canvas.gameObject.SetActive(true);
        Time.timeScale = 1;
    }

}
