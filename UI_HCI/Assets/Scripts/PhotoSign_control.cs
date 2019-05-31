using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhotoSign_control : MonoBehaviour
{

    // public variable
    public Object SignImage;
    public GameObject chr;
    public KeyCode InputKey;
    public Object CheckImage;
    public string SignImageName;
    public GameObject Anise;

    // private variable
    public float dist;
    private bool near;
    private bool Check;
    private bool showcheck;
    private bool HaveChecked;
    private bool show;

    // Use this for initialization
    void Start()
    {
        near = false;
        HaveChecked = false;
        show = false;
        Check = false;
        showcheck = false;
        SignImage = Resources.Load("Image/Instruction/" + SignImageName);
        CheckImage = Resources.Load("Image/Instruction/Check");
        chr = GameObject.Find("Nim");
    }

    // Update is called once per frame
    void OnGUI()
    {
        NearJudge();
        if (near)
            show = true;
        else
        {
            show = false;
        }

        if (show)
        {
            GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), (Texture)SignImage);
            if (Input.GetKeyDown(InputKey) && !Check && !HaveChecked)
            {
                showcheck = true;
            }
            if (showcheck)
            {
                GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), (Texture)CheckImage);
                Check = true;
            }
            Anise.SendMessage("QuestCounterUpdate");
            //StartCoroutine (AfterTime (1));
        }
        else
        {
            GUI.DrawTexture(new Rect(0, 0, 0, 0), (Texture)null);
            if (Check)
                HaveChecked = true;
            Check = false;
            showcheck = false;
        }
    }

    IEnumerator AfterTime(float time)
    {
        yield return new WaitForSecondsRealtime(time);
        HaveChecked = true;
        GUI.DrawTexture(new Rect(0, 0, 0, 0), (Texture)null);
        Check = false;
    }

    void NearJudge()
    {
        dist = Vector3.Distance(this.gameObject.transform.position, chr.transform.position);
        if (dist <= 4 && this.gameObject.transform.position[1] - chr.transform.position[1] < 5)
        {
            near = true;
        }
        else
        {
            near = false;
        }
    }
}
