using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestOfPegaseas : MonoBehaviour
{

    // public variables
    public GameObject chr;
    public Canvas Dialog_Prefab;
    public Sprite Nim;
    public Sprite Pegaseas;
    public GameObject NPC_Sign;
    public Canvas Sign_Canvas;


    // private variables
    public bool DisplaySign;
    private float dist;
    private bool near;
    private Canvas Dialog;
    private GameObject Sign;
    private Image Left;
    private Image Right;
    private Text DialogText;
    private int state = 0;

    // Quest Variables
    public GameObject CheckedWish;
    public Canvas QuestCanvas;
    public Text QuestText_Name;
    public Text QuestText_Req_Cout;
    private Text QuestText_private;
    private string QuestName;
    private string QuestDescription;
    private string QuestRequirement;
    private int QuestReq;
    private int QuestCounter;
    private bool IsQuestAccept;
    public bool IsQuestComplete;
    public bool IsQuestTurnIn;
    public Text QuestFollowText;

    private bool IsDiaOver;

	//enemy spawn
	public GameObject enemy1;
	public GameObject enemy2;
	public GameObject enemy3;
	public GameObject enemy4;
	public GameObject enemy5;
	public GameObject spawnPoint1;
	public GameObject spawnPoint2;
	public GameObject spawnPoint3;
	public GameObject spawnPoint4;
	public GameObject spawnPoint5;

    //
    // Fruit (0/1)

    //
    // * Pancakse's Fruit


    // Use this for initialization
    void Start()
    {
        DialogueEdit();
        IsQuestAccept = false;
        IsQuestComplete = false;
        IsQuestTurnIn = false;
        DisplaySign = true;
        QuestName = "* Kill nightmares for Pegaseas";
        QuestDescription = "Please kill 5 nightmares to help Pegaseas";
        QuestRequirement = "Nightmares killed";
        QuestReq = 5;
        QuestCounter = 0;
        IsDiaOver = false;
        // Find GameObject
        chr = GameObject.Find("Nim");

    }

    // Update is called once per frame
    void Update()
    {
        NearJudge();
        SignJudge();

        DialogueEdit();
        Completejudge();
        //WishDisplayUpdate ();
    }

    void FreezeNim()
    {
        Rigidbody NIM = chr.GetComponent<Rigidbody>();
        NIM.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY;
    }

    void UnFreezeNim()
    {
        Rigidbody NIM = chr.GetComponent<Rigidbody>();
        NIM.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY;
    }

    // This function will be called by fruits
    public void QuestCounterUpdatePegaseas()
    {
        if (IsQuestAccept)
            QuestCounter++;
    }

    void WishDisplayUpdate()
    {
        if (IsQuestAccept)
        {
            QuestText_Name.text = QuestName;
            QuestText_Req_Cout.text = QuestRequirement + " (" + QuestCounter + "/" + QuestReq + ")";
        }
        if (IsQuestTurnIn)
        {
            QuestText_Name.text = "";
            QuestText_Req_Cout.text = "";
        }
    }

    void WishFollowUpdate()
    {
        QuestFollowText.text = QuestRequirement + " (" + QuestCounter + "/" + QuestReq + ")";
        if (IsQuestComplete)
            CheckedWish.SetActive(true);
    }

    void DialogueEdit()
    {
        if (near)
        {
            SignJudge();
            if (DisplaySign)
            {
                if (!Sign)
                    Sign = Instantiate(NPC_Sign, Sign_Canvas.transform);
                state = 0;
                DisplaySign = false;
            }
            else
            {
                if (Sign)
                    Destroy(Sign);
            }
            if (!IsQuestAccept && !IsQuestComplete)
            {
                // Dialog:
                // Press E to initiate a dialog canvas
                // Each time you press enter, you will go to the next stage.

                // Accept Quest

                if (Input.GetKeyDown(KeyCode.C) && !Dialog)
                {
                    FreezeNim();
                    Destroy(Sign);
                    GenerateDialog();
                    Text Dia_Text = Dialog.transform.GetChild(4).GetComponent<Text>();
                    Dia_Text.text = "Oh dear… Oh dear…";
                    state = 1;
                }
                switch (state)
                {
                    case 1:
                        Debug.Log("state 1");
                        if (Input.GetKeyDown(KeyCode.Return))
                        {
                            Text Dia_Text = Dialog.transform.GetChild(4).GetComponent<Text>();
                            Dia_Text.text = "I want to go visit my sister in the mountains, but there are so many Nightmares in the sky.  It’s not safe to fly.";
                            state = 2;
                        }
                        break;
                    case 2:
                        Debug.Log("state 1");
                        if (Input.GetKeyDown(KeyCode.Return))
                        {
                            Text Dia_Text = Dialog.transform.GetChild(4).GetComponent<Text>();
                            Dia_Text.text = "If only there were 5 less flying Nightmares around here.  Then I can fly over to my sister’s house easily.";
                            state = 3;
                        }
                        break;
                    case 3:
                        if (Input.GetKeyDown(KeyCode.Return))
                        {
                            IsQuestAccept = true;
                            // Add quest to player
                            chr.SendMessage("AddQuest", "Pegaseas");
                            // 
                            DestroyCanvas();
                            UnFreezeNim();
							EnemyAppear ();
                            //DisplaySign = true;
                            IsDiaOver = true;
                        }
                        break;
                    default:
                        break;
                }

            }
            else if (IsQuestAccept && !IsQuestComplete)
            {
                // Telling you quest unfinished
                if (Input.GetKeyDown(KeyCode.C))
                {
                    FreezeNim();
                    Destroy(Sign);
                    GenerateDialog();
                    Text Dia_Text = Dialog.transform.GetChild(4).GetComponent<Text>();
                    Dia_Text.text = "I wish I could fly, but there are too many flying Nightmares in the sky. If only there were 5 or so less..";
                    state = 1;
                }
                switch (state)
                {
                    case 1:
                        if (Input.GetKeyDown(KeyCode.Return))
                        {
                            DestroyCanvas();
                            UnFreezeNim();
                            //DisplaySign = true;
                            IsDiaOver = true;
                        }
                        break;
                    default:
                        break;
                }

            }
            else
            {
                // Finished Quest
                if (Input.GetKeyDown(KeyCode.C))
                {
                    FreezeNim();
                    Destroy(Sign);
                    GenerateDialog();
                    Text Dia_Text = Dialog.transform.GetChild(4).GetComponent<Text>();
                    Dia_Text.text = "Thank you!  I can go see my sister now!";
                    state = IsQuestTurnIn?2: 1;
                }
                switch (state)
                {
                    case 1:
                        if (Input.GetKeyDown(KeyCode.Return))
                        {
                            Text Dia_Text = Dialog.transform.GetChild(4).GetComponent<Text>();
                            Text Name = Dialog.transform.GetChild(3).GetComponent<Text>();
                            Name.text = "";
                            Dia_Text.text = "Pegaseas's wish is granted!";
                            chr.SendMessage("WishGrantUpdate");
                            state = 2;
                        }
                        break;
                    case 2:
                        if (Input.GetKeyDown(KeyCode.Return))
                        {
                            DestroyCanvas();
                            UnFreezeNim();
                            //DisplaySign = true;
                            IsDiaOver = true;
                            IsQuestTurnIn = true;
                            state = 3;
                        }
                        break;
                    default:
                        break;
                }
            }
        }
        else
        {
            Destroy(Sign);
            DisplaySign = true;
        }


    }

    void GenerateDialog()
    {
        if (!Dialog)
        {
            Dialog = Instantiate(Dialog_Prefab);
            Image Left = Dialog.transform.GetChild(0).GetComponent<Image>();
            Left.sprite = (Sprite)Nim;
            Image Right = Dialog.transform.GetChild(1).GetComponent<Image>();
            Right.sprite = Pegaseas;
            Text Name = Dialog.transform.GetChild(3).GetComponent<Text>();
            Name.text = "Pegaseas";
        }
    }

    void DestroyCanvas()
    {
        Destroy(Dialog.gameObject);
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
            IsDiaOver = false;
        }
    }

    void Completejudge()
    {
        if (QuestCounter >= QuestReq)
            IsQuestComplete = true;
    }

    void SignJudge()
    {
        if (near)
        {
            if (IsDiaOver || Dialog)
            {
                DisplaySign = false;
            }
            else
            {
                DisplaySign = true;
            }
        }
        else
        {
            DisplaySign = false;
        }
    }

	void EnemyAppear(){
		if(IsQuestAccept == true){

			Instantiate (enemy1, spawnPoint1.transform.position, Quaternion.identity);
			Instantiate (enemy2, spawnPoint2.transform.position, Quaternion.identity);
			Instantiate (enemy3, spawnPoint3.transform.position, Quaternion.identity);			
			Instantiate (enemy4, spawnPoint4.transform.position, Quaternion.identity);
			Instantiate (enemy5, spawnPoint5.transform.position, Quaternion.identity);

		}	
	}
}
