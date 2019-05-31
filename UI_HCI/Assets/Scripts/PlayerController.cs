// This script is for Nim's quest control.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{

    public Canvas AllQuest_Canvas;

    public bool QuestOn;

    public Button FollowWish;

    public Button[] QuestButton;
    public Text QuestDes;

    public string[] QuestName;
    public string QuestFollow;
    public bool[] IsQuestComplete;

    // All the NPC with quests
    public GameObject Pancake;
    public GameObject FlowerGhost;
    public GameObject PeanutArcher;
    public GameObject Anise;
    public GameObject DandyLion;
    public GameObject Pegaseas;
    public GameObject StarHat;
    public GameObject OrangeGhost;

    public Text QuestFollowText;

    /*
	public Canvas SignCanvas;
	public GameObject SignImage;
	public GameObject CheckImage;
	public KeyCode InputKey;
	*/
    public GameObject CheckedWish;
    public GameObject WishLogo;

    /*
	private GameObject Sign;
	private GameObject Check;
	private bool Havechecked;
	*/

    public bool IsAssigned;

    public int WishGranted;

    // For instruction
    private Object Instruction;
    private bool IsDisplay;
    private Object ControlInstruction;
    private bool IsControlDisplay;

    //for hiddenstars
    private int HiddenFound;

    void Start()
    {
        QuestOn = false;
        for (int i = 0; i < 8; i++)
        {
            QuestName[i] = "";
            IsQuestComplete[i] = false;
        }
        //Havechecked = false;
        IsAssigned = false;
        WishGranted = 0;
        IsDisplay = false;
        IsControlDisplay = false;
        Instruction = Resources.Load("Image/Instruction/WishListInstruction");
        ControlInstruction = Resources.Load("Image/Instruction/WishListControl");

        // Find NPC
        Pancake = GameObject.Find("Pancake");
        Anise = GameObject.Find("Anise");
        PeanutArcher = GameObject.Find("PeanutArcher");
        Pegaseas = GameObject.Find("Pegaseas");
        StarHat = GameObject.Find("StarHat");
        FlowerGhost = GameObject.Find("FlowerGhost");
        DandyLion = GameObject.Find("DandyLion");
        OrangeGhost = GameObject.Find("OrangeGhost");

        HiddenFound = 0;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl) || Input.GetKeyDown(KeyCode.RightControl))
        {
            if (!QuestOn)
            {
                QuestDisplay();
                Debug.Log("Display");
                Time.timeScale = 0;
            }
            else
            {
                QuestUndisplay();
                Time.timeScale = 1;
            }
        }

        QuestDisplayOnScreen();


        if (QuestName[0] != "" && (!IsQuestComplete[0]) && (!IsAssigned))
        {
            WishLogo.SetActive(true);
            QuestFollow = QuestName[0];
        }

        CheckComplete();
        for (int i = 0; i < 8; i++)
        {
            if (QuestName[i] == QuestFollow && (IsQuestComplete[i]))
            {
                QuestFollow = NextQuest(i);
            }
        }
        DeleteCompleteQuest();
    }

    string NextQuest(int i)
    {
        for (int j = 0; j < 8; j++)
        {
            if (!IsQuestComplete[j])
            {
                return QuestName[j];
            }
        }
        return "";
    }

    void OnGUI()
    {

        if (QuestName[0] != "" && (!IsDisplay))
        {
            // Display Instruction
            Time.timeScale = 0;
            GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), (Texture)Instruction);
            if (Input.GetKeyDown(KeyCode.Return))
            {
                Time.timeScale = 1;
                GUI.DrawTexture(new Rect(0, 0, 0, 0), (Texture)Instruction);
                IsDisplay = true;
            }
        }

        if (QuestName[0] != "" && (!IsControlDisplay) && IsDisplay)
        {
            GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), (Texture)ControlInstruction);
            if (Input.GetKeyDown(KeyCode.LeftControl) || Input.GetKeyDown(KeyCode.RightControl))
            {
                GUI.DrawTexture(new Rect(0, 0, 0, 0), (Texture)ControlInstruction);
                IsControlDisplay = true;
            }
        }
    }

    void WishGrantUpdate()
    {
        WishGranted++;
    }

    void CheckComplete()
    {
        for (int i = 0; i < 8; i++)
        {
            switch (QuestName[i])
            {
                case "Pancake":
                    IsQuestComplete[i] = Pancake.GetComponent<QuestofPancake>().IsQuestTurnIn;
                    break;
                case "Flower Ghost":
                    IsQuestComplete[i] = FlowerGhost.GetComponent<QuestOfFlowerGhost>().IsQuestTurnIn;
                    break;
                case "PA":
                    IsQuestComplete[i] = PeanutArcher.GetComponent<QuestOfPA>().IsQuestTurnIn;
                    break;
                case "StarHat":
                    IsQuestComplete[i] = StarHat.GetComponent<QuestOfSH>().IsQuestTurnIn;
                    break;
                case "DandyLion":
                    IsQuestComplete[i] = DandyLion.GetComponent<QuestOfDL>().IsQuestTurnIn;
                    break;
                case "Pegaseas":
                    IsQuestComplete[i] = Pegaseas.GetComponent<QuestOfPegaseas>().IsQuestTurnIn;
                    break;
                case "Anise":
                    IsQuestComplete[i] = Anise.GetComponent<QuestOfAnise>().IsQuestTurnIn;
                    break;
                case "OrangeGhost":
                    IsQuestComplete[i] = OrangeGhost.GetComponent<QuestOfOG>().IsQuestTurnIn;
                    break;
                default:
                    break;
            }
        }
    }

    void DeleteCompleteQuest()
    {
        for (int i = 0; i < 8; i++)
        {
            if (IsQuestComplete[i])
                QuestName[i] = "";
        }
    }

    // Call by other NPC
    public void AddQuest(string name)
    {
        for (int i = 0; i < 8; i++)
        {
            if (QuestName[i] == "")
            {
                QuestName[i] = name;
                break;
            }
        }
    }

    void QuestDisplay()
    {
        AllQuest_Canvas.gameObject.SetActive(true);
        QuestDes.text = "";
        FollowWish.gameObject.SetActive(false);
        CheckedWish.SetActive(false);
        QuestFollowText.text = "";
        ButtonDistrubute();
        QuestOn = true;
    }

    void ButtonDistrubute()
    {
        for (int i = 0; i < 8; i++)
        {
            switch (QuestName[i])
            {
                case "Pancake":
                    QuestButton[i].gameObject.SetActive(true);
                    QuestButton[i].GetComponentInChildren<Text>().text = "Pancake's Fruits";
                    QuestButton[i].onClick.AddListener(DisplayPancake);
                    break;
                case "Flower Ghost":
                    QuestButton[i].gameObject.SetActive(true);
                    QuestButton[i].GetComponentInChildren<Text>().text = "Flower Ghost's Letter";
                    QuestButton[i].onClick.AddListener(DisplayFG);
                    break;
                case "PA":
                    QuestButton[i].gameObject.SetActive(true);
                    QuestButton[i].GetComponentInChildren<Text>().text = "Peanut Archer's Wish";
                    QuestButton[i].onClick.AddListener(DisplayPA);
                    break;
                case "StarHat":
                    QuestButton[i].gameObject.SetActive(true);
                    QuestButton[i].GetComponentInChildren<Text>().text = "Star Hat's Wish";
                    QuestButton[i].onClick.AddListener(DisplaySH);
                    break;
                case "Anise":
                    QuestButton[i].gameObject.SetActive(true);
                    QuestButton[i].GetComponentInChildren<Text>().text = "Anise's Wish";
                    QuestButton[i].onClick.AddListener(DisplayAnise);
                    break;
                case "Pegaseas":
                    QuestButton[i].gameObject.SetActive(true);
                    QuestButton[i].GetComponentInChildren<Text>().text = "Pegaseas's Wish";
                    QuestButton[i].onClick.AddListener(DisplayPegaseas);
                    break;
                case "DandyLion":
                    QuestButton[i].gameObject.SetActive(true);
                    QuestButton[i].GetComponentInChildren<Text>().text = "Dandy Lion's Wish";
                    QuestButton[i].onClick.AddListener(DisplayDL);
                    break;
                case "OrangeGhost":
                    QuestButton[i].gameObject.SetActive(true);
                    QuestButton[i].GetComponentInChildren<Text>().text = "Orange Ghost's Wish";
                    QuestButton[i].onClick.AddListener(DisplayOG);
                    break;
                default:
                    QuestButton[i].gameObject.SetActive(false);
                    break;
            }
        }
    }
    void DisplaySH()
    {
        CheckedWish.SetActive(false);
        FollowWish.gameObject.SetActive(true);
        QuestDes.text = "Star Hat want to take a good look at that star, will you take it for him?";
        StarHat.SendMessage("WishFollowUpdate");
        FollowWish.onClick.AddListener(FollowSH);
    }
    void FollowSH()
    {
        IsAssigned = true;
        QuestFollow = "StarHat";
    }
    void DisplayAnise()
    {
        CheckedWish.SetActive(false);
        FollowWish.gameObject.SetActive(true);
        QuestDes.text = "Anise want to take a photo of Sleeping Hills but too afraid of these nightmares. Can you help her take that photo?";
        Anise.SendMessage("WishFollowUpdate");
        FollowWish.onClick.AddListener(FollowAnise);
    }
    void FollowAnise()
    {
        IsAssigned = true;
        QuestFollow = "Anise";
    }
    void DisplayDL()
    {
        CheckedWish.SetActive(false);
        FollowWish.gameObject.SetActive(true);
        QuestDes.text = "Dandy Lion lost his favoriate yarn. Can you help him find it?";
        DandyLion.SendMessage("WishFollowUpdate");
        FollowWish.onClick.AddListener(FollowDL);
    }
    void FollowDL()
    {
        IsAssigned = true;
        QuestFollow = "DandyLion";
    }
    void DisplayOG()
    {
        CheckedWish.SetActive(false);
        FollowWish.gameObject.SetActive(true);
        QuestDes.text = "Orange Ghost wants you to send his reply back to Flower Ghost.";
        OrangeGhost.SendMessage("WishFollowUpdate");
        FollowWish.onClick.AddListener(FollowOG);
    }
    void FollowOG()
    {
        IsAssigned = true;
        QuestFollow = "OrangeGhost";
    }
    void DisplayPegaseas()
    {
        CheckedWish.SetActive(false);
        FollowWish.gameObject.SetActive(true);
        QuestDes.text = "Pegaseas is afraid of these nightmares. Can you help him kill some of those?";
        Pegaseas.SendMessage("WishFollowUpdate");
        FollowWish.onClick.AddListener(FollowPegaseas);
    }
    void FollowPegaseas()
    {
        IsAssigned = true;
        QuestFollow = "Pegaseas";
    }

    void DisplayPancake()
    {
        CheckedWish.SetActive(false);
        FollowWish.gameObject.SetActive(true);
        QuestDes.text = "Pancake wants some fruits. Maybe you can help him find some?";
        Pancake.SendMessage("WishFollowUpdate");
        FollowWish.onClick.AddListener(FollowPancake);
    }
    void FollowPancake()
    {
        IsAssigned = true;
        QuestFollow = "Pancake";
    }

    void DisplayFG()
    {
        CheckedWish.SetActive(false);
        FollowWish.gameObject.SetActive(true);
        QuestDes.text = "Flower Ghost is going to send this letter to a little girl, but he is so afraid of the Nightmares around the hill. He asked you to send the letter for him.";
        FlowerGhost.SendMessage("WishFollowUpdate"); ;
        FollowWish.onClick.AddListener(FollowFG);
    }
    void FollowFG()
    {
        IsAssigned = true;
        QuestFollow = "Flower Ghost";
    }

    void DisplayPA()
    {
        CheckedWish.SetActive(false);
        FollowWish.gameObject.SetActive(true);
        QuestDes.text = "Peanut Archer is dealing with the Nightmares on the hill. He wishes you can help him kill some of them.";
        PeanutArcher.SendMessage("WishFollowUpdate");
        FollowWish.onClick.AddListener(FollowPA);
    }
    void FollowPA()
    {
        IsAssigned = true;
        QuestFollow = "Peanut Archer";
    }

    void QuestDisplayOnScreen()
    {
        switch (QuestFollow)
        {
            case "Pancake":
                Pancake.SendMessage("WishDisplayUpdate");
                break;
            case "Flower Ghost":
                FlowerGhost.SendMessage("WishDisplayUpdate");
                break;
            case "Peanut Archer":
                PeanutArcher.SendMessage("WishDisplayUpdate");
                break;
            case "DandyLion":
                DandyLion.SendMessage("WishDisplayUpdate");
                break;
            case "StarHat":
                StarHat.SendMessage("WishDisplayUpdate");
                break;
            case "Pegaseas":
                Pegaseas.SendMessage("WishDisplayUpdate");
                break;
            case "Anise":
                Anise.SendMessage("WishDisplayUpdate");
                break;
            case "OrangeGhost":
                OrangeGhost.SendMessage("WishDisplayUpdate");
                break;
            case "":
                WishLogo.SetActive(false);
                break;
            default:
                break;
        }
    }

    void QuestUndisplay()
    {
        AllQuest_Canvas.gameObject.SetActive(false);
        QuestOn = false;
    }
    void HiddenUpdate()
    {
        HiddenFound++;
    }
}
