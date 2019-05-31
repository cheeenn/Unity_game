using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestOfPene : MonoBehaviour {

	// public variables
	private GameObject chr;
	private GameObject FlowerGhost;
	public Canvas Dialog_Prefab;
	public Sprite Nim;
	public Sprite Pene;
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
	private bool IsQuestAccept;
	public bool IsQuestComplete;
	public bool IsQuestTurnIn;
	public Text QuestFollowText;

	private bool IsDiaOver;

	private bool SelfQuestComplete;
	private bool IsFGQuestAccept;
	private bool IsFGQuestComplete;

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
		SelfQuestComplete = false;
		IsFGQuestAccept = false;
		IsFGQuestComplete = false;

		// Find GameObject
		chr = GameObject.Find("Nim");
		FlowerGhost = GameObject.Find ("FlowerGhost");
	}

	// Update is called once per frame
	void Update()
	{
		IsFGQuestAccept = FlowerGhost.GetComponent<QuestOfFlowerGhost>().IsQuestAccept ;
		IsFGQuestComplete =FlowerGhost.GetComponent<QuestOfFlowerGhost>().IsQuestComplete ;
		NearJudge();
		DialogueEdit();
		//Completejudge();
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

	void DialogueEdit()
	{
		if (near) {
			SignJudge ();
			if (DisplaySign) {
				if (!Sign)
					Sign = Instantiate (NPC_Sign, Sign_Canvas.transform);
				state = 0;
				DisplaySign = false;
			} else {
				if (Sign)
					Destroy (Sign);
			}

			if (IsFGQuestAccept && !IsFGQuestComplete) {
				// complete fg quest
				if (Input.GetKeyDown (KeyCode.C) && !Dialog) {
					FreezeNim ();
					Destroy (Sign);
					GenerateDialog ();
					Text Dia_Text = Dialog.transform.GetChild (4).GetComponent<Text> ();
					Dia_Text.text = "Hi, my name is Penelope.";
					state = 1;
				}
				switch (state) {
				case 1:
					if (Input.GetKeyDown (KeyCode.Return)) {
						Text Dia_Text = Dialog.transform.GetChild (4).GetComponent<Text> ();
						Dia_Text.text = "This is for me?";
						state = 2;
					}
					break;
				case 2:
					if (Input.GetKeyDown (KeyCode.Return)) {
						Text Dia_Text = Dialog.transform.GetChild (4).GetComponent<Text> ();
						Dia_Text.text = "After all this time, he still remember Lyra's favorite flowers.";
						state = 3;
					}
					break;
				case 3:
					if (Input.GetKeyDown (KeyCode.Return)) {
						Text Dia_Text = Dialog.transform.GetChild (4).GetComponent<Text> ();
						Dia_Text.text = "Thank you for bringing them here.";
						state = 4;
					}
					break;
				case 4:
					if (Input.GetKeyDown (KeyCode.Return)) {
						Text Dia_Text = Dialog.transform.GetChild (4).GetComponent<Text> ();
						Dia_Text.text = "I'm sure Lyra really appreciates it.";
						state = 5;
					}
					break;
				case 5:
					if (Input.GetKeyDown (KeyCode.Return)) {
						Text Dia_Text = Dialog.transform.GetChild (4).GetComponent<Text> ();
						Dia_Text.text = "I've been leaving flowers on this spot to help her rest.";
						state = 6;
					}
					break;
				case 6:
					if (Input.GetKeyDown (KeyCode.Return)) {
						//IsQuestAccept = true;
						//
						//chr.SendMessage ("AddQuest", "Flower Ghost");
						//
						FlowerGhost .SendMessage ("QuestCounterUpdate");
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
						Dia_Text.text = "Hi there.";
						state = 1;
					}
					switch (state) {
					case 1:
						if (Input.GetKeyDown (KeyCode.Return)) {
							Text Dia_Text = Dialog.transform.GetChild (4).GetComponent<Text> ();
							Dia_Text.text = "That's a Wishing Star. I haven't see one of those in a long time.";
							state = 2;
						}
						break;
					case 2:
						if (Input.GetKeyDown (KeyCode.Return)) {
							Text Dia_Text = Dialog.transform.GetChild (4).GetComponent<Text> ();
							Dia_Text.text = "I thought all the Wishing Stars had disappeared.";
							state = 3;
						}
						break;
					case 3:
						if (Input.GetKeyDown (KeyCode.Return)) {
							Text Dia_Text = Dialog.transform.GetChild (4).GetComponent<Text> ();
							Dia_Text.text = "You're off to see the Wishing Whales, aren't you?";
							state = 4;
						}
						break;
					case 4:
						if (Input.GetKeyDown (KeyCode.Return)) {
							Text Dia_Text = Dialog.transform.GetChild (4).GetComponent<Text> ();
							Dia_Text.text = "Well, just remember that you also have to help grant the wishes of 100 people if you want your wish granted.";
							state = 5;
						}
						break;
					case 5:
						if (Input.GetKeyDown (KeyCode.Return)) {
							Text Dia_Text = Dialog.transform.GetChild (4).GetComponent<Text> ();
							Dia_Text.text = "Thanks for stopping by.";
							state = 6;
						}
						break;
					case 6:
						if (Input.GetKeyDown (KeyCode.Return)) {
							Text Dia_Text = Dialog.transform.GetChild (4).GetComponent<Text> ();
							Dia_Text.text = "I've been feeling kinda hopeless about thigns since Lyra and others ...";
							state = 7;
						}
						break;
					case 7:
						if (Input.GetKeyDown (KeyCode.Return)) {
							Text Dia_Text = Dialog.transform.GetChild (4).GetComponent<Text> ();
							Dia_Text.text = "Anyway, seeing that Wishing Star makes me feel a lot better than I have in a long time.";
							state = 8;
						}
						break;
					case 8:
						if (Input.GetKeyDown (KeyCode.Return)) {
							Text Dia_Text = Dialog.transform.GetChild (4).GetComponent<Text> ();
							Text Name = Dialog.transform.GetChild (3).GetComponent<Text> ();
							Name.text = " ";
							Dia_Text.text = "Penelope's wish is granted!";
							chr.SendMessage ("WishGrantUpdate");
							state = 9;
						}
						break;
					case 9:
						if (Input.GetKeyDown (KeyCode.Return)) {
							IsQuestAccept = true;
							IsQuestComplete = true;
							//
							//chr.SendMessage ("AddQuest", "Flower Ghost");
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

				} else {
					// Finished Quest
					if (Input.GetKeyDown (KeyCode.C)) {
						FreezeNim ();
						Destroy (Sign);
						GenerateDialog ();
						Text Dia_Text = Dialog.transform.GetChild (4).GetComponent<Text> ();
						Dia_Text.text = "Good Luck, friend.";
						state = 1;
					}
					switch (state) {
					case 1:
						if (Input.GetKeyDown (KeyCode.Return)) {
							Text Dia_Text = Dialog.transform.GetChild (4).GetComponent<Text> ();
							Dia_Text.text = "As long as you saty true to yourself, your journey will not fail.";
							state = 2;
						}
						break;
					case 2:
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
			} 
		}else {
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
			Right.sprite = Pene;
			Text Name = Dialog.transform.GetChild (3).GetComponent<Text> ();
			Name.text = "Penelope";
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
}
