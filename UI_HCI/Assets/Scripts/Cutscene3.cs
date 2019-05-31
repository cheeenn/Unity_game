using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cutscene3 : MonoBehaviour
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
    private int state = 0;


    bool cutsceneFinished;
    private Sprite[] sprites;
    public GameObject bossPrefab;
    enum states { start, penelopeDialog, nightmareAttack, penelopeHelp, nightmareLeave, cutsceneDone };
    states currentState;
    void Awake()
    {
        triggered = false;
        cutsceneFinished = false;
        player = GameObject.Find("Nim");
        state = 0;

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
            float step = 6 * Time.deltaTime;
            if (currentState == states.start)
            {
                Physics.gravity = new Vector3(0, player.GetComponent<NimController>().gravityConstant * 10, 0);
                if (player.GetComponent<NimController>().isOnGround())
                {
                    GenerateDialog();
                    Text Dia_Text = Dialog.transform.GetChild(4).GetComponent<Text>();
                    Dia_Text.text = "It's you!";
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
                            Dia_Text.text = "This Nightmare is dangerous, but maybe if we work together, we can take it down!";
                         
                            state = 2;
                        }
                        break;
                    case 2:
                        if (Input.GetKeyDown(KeyCode.Return))
                        {
                            
                            currentState = states.cutsceneDone;
                            cutsceneFinished = true;
                            DestroyCanvas();
							//EnemyAppear ();
                            player.GetComponent<NimController>().active = true;
                            Physics.gravity = new Vector3(0, player.GetComponent<NimController>().gravityConstant, 0);

                            state = 3;
                        }
                        break;

                    default:
                        break;
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

//	void EnemyAppear(){
//		{
//
//			Instantiate (bossPrefab, bossSpawnPoint.transform.position, Quaternion.identity);
//
//
//		}	
//	}
}
