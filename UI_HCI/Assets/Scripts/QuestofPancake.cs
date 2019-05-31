using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestofPancake : MonoBehaviour
{

	// public variables
	public GameObject chr;
	public Canvas Dialog_Prefab;
	public Sprite Nim;
	public Sprite Pancake;
	public GameObject NPC_Sign;
	public Canvas Sign_Canvas;
	public GameObject Wall;

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
    private GameObject SIGN;

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
		QuestName = "* Pancake's Fruit";
		QuestDescription = "Pancake wants some fruits. Maybe you can help him find some.";
		QuestRequirement = "fruits";
		QuestReq = 1;
		QuestCounter = 0;
		IsDiaOver = false;
		// Find GameObject
		chr = GameObject.Find("Nim");
        SIGN = GameObject.Find("MJInstructionSign");
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

	void FreezeNim(){
		Rigidbody NIM = chr.GetComponent<Rigidbody> ();
		NIM.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ| RigidbodyConstraints.FreezeRotationZ| RigidbodyConstraints.FreezeRotationX| RigidbodyConstraints.FreezeRotationY;
	}

	void UnFreezeNim(){
		Rigidbody NIM = chr.GetComponent<Rigidbody> ();
		NIM.constraints = RigidbodyConstraints.FreezePositionZ| RigidbodyConstraints.FreezeRotationZ| RigidbodyConstraints.FreezeRotationX| RigidbodyConstraints.FreezeRotationY;
	}

	// This function will be called by fruits
	public void QuestCounterUpdate(){
		if (IsQuestAccept)
			QuestCounter++;
	}

	void WishDisplayUpdate (){
		if (IsQuestAccept) {
			QuestText_Name.text = QuestName;
			QuestText_Req_Cout.text = QuestRequirement + " (" + QuestCounter + "/" + QuestReq + ")";
		}
		if (IsQuestTurnIn) {
			QuestText_Name.text = "";
			QuestText_Req_Cout.text = "";
		}
	}

	void WishFollowUpdate(){
		QuestFollowText.text = QuestRequirement + " (" + QuestCounter + "/" + QuestReq + ")";
		if (IsQuestComplete)
			CheckedWish.SetActive (true);
	}

    void DialogueEdit()
    {
		if (near) {
			SignJudge ();
			if (DisplaySign) {
				if(!Sign)
				Sign = Instantiate (NPC_Sign, Sign_Canvas.transform);
				state = 0;
				DisplaySign = false;
			} else {
				if (Sign)
					Destroy (Sign);
			}
			if (!IsQuestAccept && !IsQuestComplete) {
				// Dialog:
				// Press E to initiate a dialog canvas
				// Each time you press enter, you will go to the next stage.

				// Accept Quest

				if (Input.GetKeyDown (KeyCode.C) && !Dialog) {
					FreezeNim ();
					Destroy (Sign);
					GenerateDialog ();
					Text Dia_Text = Dialog.transform.GetChild (4).GetComponent<Text> ();
					Dia_Text.text = "Hi there.  I’m Pancake.  You’re travelling into the Sleeping Hills, right?";
					state = 1;
				}
				switch (state) {
				case 1:
					Debug.Log ("state 1");
					if (Input.GetKeyDown (KeyCode.Return)) {
						Text Dia_Text = Dialog.transform.GetChild (4).GetComponent<Text> ();
						Dia_Text.text = "Well, you’ll need a special skill if you want to go farther in.  If you help me get that fruit above me, I’ll teach you the special skill.";
						state = 2;
					}
					break;
				case 2:
					if (Input.GetKeyDown (KeyCode.Return)) {
						IsQuestAccept = true;
						// Add quest to player
						chr.SendMessage("AddQuest", "Pancake");
						//
						Destroy (Wall);
						DestroyCanvas ();
						UnFreezeNim ();
						//DisplaySign = true;
						IsDiaOver = true;
					}
					break;
				default:
					break;
				}

			} else if (IsQuestAccept && !IsQuestComplete) {
				// Telling you quest unfinished
				if (Input.GetKeyDown (KeyCode.C)) {
					FreezeNim ();
					Destroy (Sign);
					GenerateDialog ();
					Text Dia_Text = Dialog.transform.GetChild (4).GetComponent<Text> ();
					Dia_Text.text = "Do you have my fruits yet?";
					state = 1;
				}
				switch (state) {
				case 1:
					if (Input.GetKeyDown (KeyCode.Return)) {
						DestroyCanvas ();
						UnFreezeNim ();
						//DisplaySign = true;
						IsDiaOver = true;
					}
					break;
				default:
					break;
				}

			} else {
				// Finished Quest
				if (Input.GetKeyDown (KeyCode.C)) {
					FreezeNim ();
					Destroy (Sign);
					GenerateDialog ();
					Text Dia_Text = Dialog.transform.GetChild (4).GetComponent<Text> ();
					Dia_Text.text = IsQuestTurnIn?"Thank you so much!":"WoW, thank you so much! You granted my wish!";
					state = IsQuestTurnIn?5:1;
				}
				switch (state) {
				case 1:
					if (Input.GetKeyDown (KeyCode.Return)) {
						Text Dia_Text = Dialog.transform.GetChild (4).GetComponent<Text> ();
						Dia_Text.text = "I heard that you have to grant 100 wishes for the Wishing Whale to grant your own wish.  I really hope that’s just a rumor, 100 wishes sounds like a lot.";
						state = 2;
					}
					break;
				case 2:
					if (Input.GetKeyDown (KeyCode.Return)) {
						Text Dia_Text = Dialog.transform.GetChild (4).GetComponent<Text> ();
						Dia_Text.text = "Oh, I almost forgot. Here, I’ll teach you how to do multi-jump.";
						state = 3;
					}
					break;
                case 3:
                    if (Input.GetKeyDown(KeyCode.Return))
                    {
                        Text Dia_Text = Dialog.transform.GetChild(4).GetComponent<Text>();
                        Dia_Text.text = "When you do your multi-jump, you will also shoot your star backwards. That will help you a lot on your journey.";
                        state = 4;
                    }
                    break;
                case 4:
					if (Input.GetKeyDown (KeyCode.Return)) {
						Text Dia_Text = Dialog.transform.GetChild (4).GetComponent<Text> ();
						Text Name = Dialog.transform.GetChild (3).GetComponent<Text> ();
						Name.text = " ";
						Dia_Text.text = "Pancakes's wish is granted!";
						chr.SendMessage ("WishGrantUpdate");
						state = 5;
					}
					break;
				case 5:
					if (Input.GetKeyDown (KeyCode.Return)) {
						DestroyCanvas ();

                            SIGN.SendMessage("Display");

						UnFreezeNim ();
						//DisplaySign = true;
						IsDiaOver = true;
						IsQuestTurnIn = true;
						state = 6;
					}
					break;
				default:
					break;
				}
			}
		} else {
			Destroy (Sign);
			DisplaySign = true;
		}


	}

    void GenerateDialog(){
		if (!Dialog) {
			Dialog = Instantiate (Dialog_Prefab);
			Image Left = Dialog.transform.GetChild (0).GetComponent<Image> ();
			Left.sprite = (Sprite)Nim;
			Image Right = Dialog.transform.GetChild (1).GetComponent<Image> ();
			Right.sprite = Pancake;
			Text Name = Dialog.transform.GetChild (3).GetComponent<Text> ();
			Name.text = "Pancake";
		}
	}

	void DestroyCanvas (){
		Destroy (Dialog.gameObject);
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

	void SignJudge(){
		if (near) {
			if (IsDiaOver || Dialog) {
				DisplaySign = false;
			} else {
				DisplaySign = true;
			}
		} else {
			DisplaySign = false;
		}
	}
}
