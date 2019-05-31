using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestOfFlowerGhost: MonoBehaviour
{

	// public variables
	public GameObject chr;
	public Canvas Dialog_Prefab;
	public Sprite Nim;
	public Sprite FlowerGhost;
	public GameObject NPC_Sign;
	public Canvas Sign_Canvas;

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
	public bool IsQuestAccept;
	public bool IsQuestComplete;
	public bool IsQuestTurnIn;
	public Text QuestFollowText;

	private bool IsDiaOver;
    private GameObject OrangeGhost;

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
		QuestName = "* Flower Ghost's Letter";
		QuestDescription = "Flower Ghost is to send this letter to a little girl. But he is afraid of the Nightmares on the hill. Maybe you can help him send the letter.";
		QuestRequirement = "Letter Sent";
		QuestReq = 1;
		QuestCounter = 0;

		// Find GameObject
		chr = GameObject.Find("Nim");
        OrangeGhost = GameObject.Find("OrangeGhost");
	}

	// Update is called once per frame
	void Update()
	{
		NearJudge();
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

	// This function will be called by little girl
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
					Dia_Text.text = "Oh?  You’re looking for Penelope?  She lives just beyond that cave up ahead.";
					state = 1;
				}
				switch (state) {
				case 1:
					if (Input.GetKeyDown (KeyCode.Return)) {
						Text Dia_Text = Dialog.transform.GetChild (4).GetComponent<Text> ();
						Dia_Text.text = "Say, if you’re going that way, can you deliver this letter for me?  It’s dangerous, and I can’t afford to die again.";
						state = 2;
					}
					break;
				case 2:
					if (Input.GetKeyDown (KeyCode.Return)) {
						Text Dia_Text = Dialog.transform.GetChild (4).GetComponent<Text> ();
						Dia_Text.text = "Please bring it to Hat Ghost when you get the chance.  Thanks!";
						state = 3;
					}
					break;
				case 3:
					if (Input.GetKeyDown (KeyCode.Return)) {
						IsQuestAccept = true;
						//
						chr.SendMessage("AddQuest", "Flower Ghost");
						//
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
					Dia_Text.text = "Hat Ghost likes hanging around past that cave up ahead. He wears an orange hat.";
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
					Dia_Text.text = "Thanks for helping me out!  I owe you one.";
					state = IsQuestTurnIn?3: 1;
				}
				switch (state) {
				case 1:
					if (Input.GetKeyDown (KeyCode.Return)) {
						Text Dia_Text = Dialog.transform.GetChild (4).GetComponent<Text> ();
						Text Name = Dialog.transform.GetChild (3).GetComponent<Text> ();
						Name.text = " ";
						Dia_Text.text = "Flower Ghost's wish is granted!";
						chr.SendMessage ("WishGrantUpdate");
						state = 2;
					}
					break;
                    case 2:
                        if (Input.GetKeyDown(KeyCode.Return))
                        {
                            Text Dia_Text = Dialog.transform.GetChild(4).GetComponent<Text>();
                            Text Name = Dialog.transform.GetChild(3).GetComponent<Text>();
                            Name.text = "Flower Ghost";
                            Dia_Text.text = "Oh, and you have his reply as well? Wow,thank you so much.";
                            OrangeGhost.SendMessage("QuestCounterUpdate");
                            state = 3;
                        }
                        break;
                    case 3:
					if (Input.GetKeyDown (KeyCode.Return)) {
						DestroyCanvas ();
						UnFreezeNim ();
						//DisplaySign = true;
						IsDiaOver = true;
						IsQuestTurnIn = true;
						state = 4;
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
			Left.sprite = Nim;
			Image Right = Dialog.transform.GetChild (1).GetComponent<Image> ();
			Right.sprite = FlowerGhost;
			Text Name = Dialog.transform.GetChild (3).GetComponent<Text> ();
			Name.text = "Flower Ghost";
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
}
