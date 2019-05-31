using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cutscene1 : MonoBehaviour {
    bool triggered;
    GameObject player;
    GameObject bossboss;
    SpriteRenderer spriteRen;

    public Canvas Dialog_Prefab;
    public Sprite Nim;
    public Sprite Pene;

    // private variables
    private Canvas Dialog;
    private Image Left;
    private Image Right;
    private Text DialogText;
    private int state = 0;
    

    bool cutsceneFinished;
    private Sprite[] sprites;
    public GameObject bossPrefab;
    enum states { start, penelopeDialog, nightmareAttack,penelopeHelp,nightmareLeave, cutsceneDone };
    states currentState;
    void Awake()
    {
        triggered = false;
        cutsceneFinished = false;
        player = GameObject.Find("Nim");
        state = 0;
        
    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
       
        if (triggered)
        {
            Debug.Log(currentState);
            float step = 6 * Time.deltaTime;
            if (currentState == states.start)
            {
                Physics.gravity = new Vector3(0, player.GetComponent<NimController>().gravityConstant * 10, 0);
                if (player.GetComponent<NimController>().isOnGround())
                {
                    GenerateDialog();
                    Text Dia_Text = Dialog.transform.GetChild(4).GetComponent<Text>();
                    Dia_Text.text = "Oh, hi.  My name is Penelope.  You were looking for me?";
                    state = 1;
                    
                    currentState = states.penelopeDialog;
                }
                
            }
            if (currentState == states.penelopeDialog)
            {
                switch (state)
                {
                    case 1:
                        if (Input.GetKeyDown(KeyCode.Return))
                        {
                            Text Dia_Text = Dialog.transform.GetChild(4).GetComponent<Text>();
                            Dia_Text.text = "???";
                            state = 2;
                        }
                        break;
                    case 2:
                        if (Input.GetKeyDown(KeyCode.Return))
                        {
                            DestroyCanvas();
                            bossboss = (GameObject)Instantiate(bossPrefab, new Vector3(150, -1, 1.5f), player.transform.rotation);
                            spriteRen = bossboss.GetComponent<SpriteRenderer>();
                            sprites = Resources.LoadAll<Sprite>("Art/boss");
                            spriteRen.sprite = sprites[0];
                            currentState = states.nightmareAttack;
                            state = 3;
                        }
                        break;

                    default:
                        break;
                }
              
              
            }
            else if (currentState == states.nightmareAttack)
            {
                bossboss.transform.position = Vector3.MoveTowards(bossboss.transform.position, new Vector3(148, 17f, 1.5f), step);
                if (Vector3.Distance(bossboss.transform.position, new Vector3(148, 17f, 1.5f)) < .2f)
                {
                    spriteRen.sprite = sprites[1];
                    Destroy(GameObject.Find("Penelope"));
                    currentState = states.penelopeHelp;

                }

            }
            else if (currentState == states.penelopeHelp)
            {
                GenerateDialog();
                Text Dia_Text = Dialog.transform.GetChild(4).GetComponent<Text>();
                Dia_Text.text = "What?  Let me go you big, dumb Nightmare!";
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    currentState = states.nightmareLeave;
                }

            }
            else if (currentState == states.nightmareLeave)
            {
                GenerateDialog();
                Text Dia_Text = Dialog.transform.GetChild(4).GetComponent<Text>();
                Dia_Text.text = "Heeeeeeellllllp!";
               

                spriteRen.flipX = true;
                bossboss.transform.position = Vector3.MoveTowards(bossboss.transform.position, new Vector3(177, 5, 1.5f), step * 1.5f);
                if (Vector3.Distance(bossboss.transform.position, new Vector3(177, 5, 1.5f)) < 3)
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
