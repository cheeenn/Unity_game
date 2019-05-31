using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CutsceneEnding : MonoBehaviour {
    public bool triggered;
    GameObject player;
    public GameObject penelopePrefab;
    GameObject penelope;
    SpriteRenderer spriteRen;
    Vector3 penelopeSpawn;

    public Canvas Dialog_Prefab;
    public Sprite Nim;
    public Sprite Pene;

    // private variables
    private Canvas Dialog;
    private Image Left;
    private Image Right;
    private Text DialogText;
    private int state = 0;

    private void Awake()
    {
        penelopeSpawn = new Vector3(190, 106.5f, 0);
        player = GameObject.Find("Nim");
        
        //triggerCutscene();
    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (triggered)
        {
            setSpriteDirections();
            Physics.gravity = new Vector3(0, player.GetComponent<NimController>().gravityConstant * 10, 0);
            if (player.GetComponent<NimController>().isOnGround()&&state==0)
            {
                GenerateDialog();
                Text Dia_Text = Dialog.transform.GetChild(4).GetComponent<Text>();
                Dia_Text.text = "Thank you saving me and deafeating that awful Nightmare.";
                state = 1;
            }
            switch (state)
            {
                case 1:
                    if (Input.GetKeyDown(KeyCode.Return))
                    {
                        Text Dia_Text = Dialog.transform.GetChild(4).GetComponent<Text>();
                        Dia_Text.text = "Oh yes, you were looking for me for something?";
                        state = 2;
                    }
                    break;
                case 2:
                    if (Input.GetKeyDown(KeyCode.Return))
                    {
                        Text Dia_Text = Dialog.transform.GetChild(4).GetComponent<Text>();
                        Dia_Text.text = "You want to find the last Wishing Whale and have your wish granted...?";
                        state = 3;
                    }
                    break;

                case 3:
                    if (Input.GetKeyDown(KeyCode.Return))
                    {
                        Text Dia_Text = Dialog.transform.GetChild(4).GetComponent<Text>();
                        Dia_Text.text = "...";
                        state = 4;
                    }
                    break;
                case 4:
                    if (Input.GetKeyDown(KeyCode.Return))
                    {
                        Text Dia_Text = Dialog.transform.GetChild(4).GetComponent<Text>();
                        Dia_Text.text = "...Very well.  I will help you.  You saved me, after all, and you're not like the others who seek out the Wishing Whale.";
                        state = 5;
                    }
                    break;

                case 5:
                    if (Input.GetKeyDown(KeyCode.Return))
                    {
                        Text Dia_Text = Dialog.transform.GetChild(4).GetComponent<Text>();
                        Dia_Text.text = "Let's go somewhere safer, and I'll tell you more about how to find the Wishing Whale.";
                        state = 6;
                    }
                    break;

                case 6:
                    if (Input.GetKeyDown(KeyCode.Return))
                    {
                        DestroyCanvas();
                    }
                    break;

                default:
                    break;
            }


        }
		
	}
    public void triggerCutscene()
    {
        triggered = true;
        penelope = Instantiate(penelopePrefab, penelopeSpawn, player.transform.rotation);
        spriteRen = penelope.GetComponent<SpriteRenderer>();
        //player.GetComponent<NimController>().active = false;
        //Physics.gravity = new Vector3(0, player.GetComponent<NimController>().gravityConstant * 10, 0);

    }
    void setSpriteDirections()
    {
        if (player.transform.position.x < penelope.transform.position.x)
        {
            player.GetComponent<NimController>().direction = 1;
            player.transform.rotation = player.GetComponent<NimController>().lookLeft;
            spriteRen.flipX = false;

        }
        else if (player.transform.position.x > penelope.transform.position.x)
        {
            player.GetComponent<NimController>().direction = -1;
            player.transform.rotation = player.GetComponent<NimController>().lookRight;
            
            spriteRen.flipX = true;
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
