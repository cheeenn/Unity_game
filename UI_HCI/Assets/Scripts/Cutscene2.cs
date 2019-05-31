using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cutscene2 : MonoBehaviour
{
    bool triggered;
    GameObject player;
    GameObject bossboss;
    SpriteRenderer spriteRen;

    public Canvas Dialog_Prefab;
    public Sprite Nim;
    public Sprite Pene;

    // private variables
    private bool DisplaySign;
    private float dist;
    private bool near;
    private Canvas Dialog;
    private GameObject Sign;
    private Image Left;
    private Image Right;
    private Text DialogText;


    bool cutsceneFinished;
    private Sprite[] sprites;
    public GameObject bossPrefab;
    enum states { start, nightmareLeave, cutsceneDone };
    states currentState;
    void Awake()
    {
        triggered = false;
        cutsceneFinished = false;
        player = GameObject.Find("Nim");
       

    }
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (triggered)
        {
            Debug.Log(currentState);
            float step = 20 * Time.deltaTime;
            if (currentState == states.start)
            {
                Physics.gravity = new Vector3(0, player.GetComponent<NimController>().gravityConstant * 10, 0);
                if (player.GetComponent<NimController>().isOnGround())
                {
                    GenerateDialog();
                    Text Dia_Text = Dialog.transform.GetChild(4).GetComponent<Text>();
                    Dia_Text.text = "aaaaaAAAAAAAAAAAAAaaaaaaa";
                    bossboss = (GameObject)Instantiate(bossPrefab, new Vector3(220, 55, 1.5f), player.transform.rotation);
                    spriteRen = bossboss.GetComponent<SpriteRenderer>();
                    sprites = Resources.LoadAll<Sprite>("Art/boss");
                    spriteRen.sprite = sprites[1];
                    spriteRen.flipX = true;
                    currentState = states.nightmareLeave;
                }

            }
            
            
            else if (currentState == states.nightmareLeave)
            {
                
         
                bossboss.transform.position = Vector3.MoveTowards(bossboss.transform.position, new Vector3(150, 62, -1.5f), step);
                if (Vector3.Distance(bossboss.transform.position, new Vector3(150, 62, -1.5f)) < 3)
                {
                    Destroy(bossboss);
                    cutsceneFinished = true;
                    DestroyCanvas();
                    player.GetComponent<NimController>().active = true;
                    Physics.gravity = new Vector3(0, player.GetComponent<NimController>().gravityConstant, 0);
                    currentState = states.cutsceneDone;
                }

            }
            else if (currentState == states.cutsceneDone)
            {

            }



        }

    }
    void OnTriggerEnter(Collider col)
    {

        if (col.gameObject == player && !triggered)
        {
            triggered = true;
            currentState = states.start;
            player.GetComponent<NimController>().active = false;
            Physics.gravity = new Vector3(0, player.GetComponent<NimController>().gravityConstant * 10, 0);
        }

    }

    void GenerateDialog()
    {
        if (!Dialog)
        {
            Dialog = Instantiate(Dialog_Prefab);
            Image Left = Dialog.transform.GetChild(0).GetComponent<Image>();
            Left.sprite = Nim;
            Image Right = Dialog.transform.GetChild(1).GetComponent<Image>();
            Right.sprite = Pene;
            Text Name = Dialog.transform.GetChild(3).GetComponent<Text>();
            Name.text = "Penelope";
        }
    }
    void DestroyCanvas()
    {
        Destroy(Dialog.gameObject);
    }
}
