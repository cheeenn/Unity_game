using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestOfSB : MonoBehaviour
{

    // public variables
    public GameObject chr;
    public Canvas Dialog_Prefab;
    public Sprite Nim;
    public Sprite StarBeard;
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
        QuestName = "";
        QuestDescription = "";
        QuestRequirement = "";
        QuestReq = 1;
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
    public void QuestCounterUpdate()
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
                    Dia_Text.text = "Hello there, my name is Star Beard.";
                    state = 1;
                }
                switch (state)
                {
                    case 1:
                        Debug.Log("state 1");
                        if (Input.GetKeyDown(KeyCode.Return))
                        {
                            Text Dia_Text = Dialog.transform.GetChild(4).GetComponent<Text>();
                            Dia_Text.text = "Is that a Wishing Star you are holding? Ho ho ho, don’t be afraid. I'm just an old scholar who's been studying Wishing Whales his whole life.";
                            state = 2;
                        }
                        break;
                    case 2:
                        Debug.Log("state 2");
                        if (Input.GetKeyDown(KeyCode.Return))
                        {
                            Text Dia_Text = Dialog.transform.GetChild(4).GetComponent<Text>();
                            Dia_Text.text = "I presume you are going to find the last Wishing Whale, aren't you little one?";
                            state = 3;
                        }
                        break;
                    case 3:
                        Debug.Log("state 3");
                        if (Input.GetKeyDown(KeyCode.Return))
                        {
                            Text Dia_Text = Dialog.transform.GetChild(4).GetComponent<Text>();
                            Dia_Text.text = "Well, the journey will be hard.  The last Wishing Whale is hiding deep within the mountains, and he won’t come out unless he trusts you.";
                            state = 4;
                        }
                        break;
                    case 4:
                        Debug.Log("state 4");
                        if (Input.GetKeyDown(KeyCode.Return))
                        {
                            Text Dia_Text = Dialog.transform.GetChild(4).GetComponent<Text>();
                            Dia_Text.text = "Don’t lose hope.  There’s a girl who lives in these hills that is very close to the Wishing Whale.  Her name is Penelope.  She might be able to guide you.";
                            state = 5;
                        }
                        break;
                    case 5:
                        Debug.Log("state 5");
                        if (Input.GetKeyDown(KeyCode.Return))
                        {
                            Text Dia_Text = Dialog.transform.GetChild(4).GetComponent<Text>();
                            
                            Dia_Text.text = "But remember, even if she brings you to the Wishing Whale, you have to help grant other people’s wishes before yours will be granted.";
                            state = 6;
                        }
                        break;
                    case 6:
                        Debug.Log("state 6");
                        if (Input.GetKeyDown(KeyCode.Return))
                        {
                            Text Dia_Text = Dialog.transform.GetChild(4).GetComponent<Text>();
                            Dia_Text.text = "Now go. I wish you a safe journey.";
                            state = 7;
                        }
                        break;

                    case 7:
                        if (Input.GetKeyDown(KeyCode.Return))
                        {
                            IsQuestAccept = true;
                            // Add quest to player
                            //chr.SendMessage("AddQuest", "Pegaseas");
                            // 
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
            else if (IsQuestAccept && !IsQuestComplete)
            {
                // Telling you quest unfinished
                if (Input.GetKeyDown(KeyCode.C))
                {
                    FreezeNim();
                    Destroy(Sign);
                    GenerateDialog();
                    Text Dia_Text = Dialog.transform.GetChild(4).GetComponent<Text>();
                    Dia_Text.text = "What are you waiting for? You have a journey waiting for you.";
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
                    Dia_Text.text = "";
                    state = 1;
                }
                switch (state)
                {
                    case 1:
                        if (Input.GetKeyDown(KeyCode.Return))
                        {
                            Text Dia_Text = Dialog.transform.GetChild(4).GetComponent<Text>();
                            Text Name = Dialog.transform.GetChild(3).GetComponent<Text>();
                            Name.text = " ";
                            Dia_Text.text = "";
                            //chr.SendMessage("WishGrantUpdate");
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
            Right.sprite = StarBeard;
            Text Name = Dialog.transform.GetChild(3).GetComponent<Text>();
            Name.text = "StarBeard";
        }
    }

    void DestroyCanvas()
    {
        Destroy(Dialog.gameObject);
    }

    void NearJudge()
    {
        dist = Vector3.Distance(this.gameObject.transform.position, chr.transform.position);
        if (dist <= 3 && this.gameObject.transform.position[1] - chr.transform.position[1] < 3.5)
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
}
