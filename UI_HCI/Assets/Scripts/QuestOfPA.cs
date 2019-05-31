using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestOfPA : MonoBehaviour
{

	// public variables
	public GameObject chr;
	public Canvas Dialog_Prefab;
	public Sprite Nim;
	public Sprite PA;
	public GameObject NPC_Sign;
	public Canvas Sign_Canvas;
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
		QuestName = "* Peanut Archer's Wish";
		QuestRequirement = "enemies killed";
		QuestReq = 5;
		QuestCounter = 0;
		//spawnPoints = GameObject.FindGameObjectWithTag ("spawnpoint");

		// Find GameObject
		chr = GameObject.Find("Nim");
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

	// This function will be called by enemies
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
					Dia_Text.text = "This is awful!  I was chasing after that Nightmare that took Penelope, but I injured my leg and now I can’t climb the hill it fled to.";
					state = 1;
				}
				switch (state) {
				case 1:
					if (Input.GetKeyDown (KeyCode.Return)) {
						Text Dia_Text = Dialog.transform.GetChild (4).GetComponent<Text> ();
						Dia_Text.text = "You there! Are you going after that big Nightmare?";
						state = 2;
					}
					break;
				case 2:
					if (Input.GetKeyDown (KeyCode.Return)) {
						Text Dia_Text = Dialog.transform.GetChild (4).GetComponent<Text> ();
						Dia_Text.text = "In that case, I can teach you my special trick shot.  It’ll be a lot of help when you go fight that big Nightmare.";
						state = 3;
					}
					break;
				case 3:
					if (Input.GetKeyDown (KeyCode.Return)) {
						Text Dia_Text = Dialog.transform.GetChild (4).GetComponent<Text> ();
						Dia_Text.text = "But first, I need to see a show of strength.  Here, help me defeat all the Nightmares in this area, and then I’ll teach you my special move.  It should be easy.  Just press X to throw your Wishing Star at Nightmares.";
						state = 4;
					}
					break;
				case 4:
					if (Input.GetKeyDown (KeyCode.Return)) {
						IsQuestAccept = true;
						// Add quest to player
						chr.SendMessage("AddQuest", "PA");
						//
						DestroyCanvas ();
						UnFreezeNim ();
						EnemyAppear ();

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
					Dia_Text.text = "If you can’t even take out 3 Nightmares, I don’t think you are capable of rescuing Penelope";
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
					Dia_Text.text = IsQuestTurnIn? "Again, thank you for your help." : "Good job you! I think you are ready for your journey.";
					state = IsQuestTurnIn?7:1;
				}
				switch (state) {
                case 1:
				    if (Input.GetKeyDown (KeyCode.Return)) {
						Text Dia_Text = Dialog.transform.GetChild (4).GetComponent<Text> ();
						Dia_Text.text = "Let me teach you a stronger attack.";
						//Text Name = Dialog.transform.GetChild (3).GetComponent<Text> ();
						//Name.text = " ";
						state = 2;
					}
					break;
                case 2:
                    if (Input.GetKeyDown(KeyCode.Return))
                    {
                        Text Dia_Text = Dialog.transform.GetChild(4).GetComponent<Text>();
                        Dia_Text.text = "Hold X until your star becomes blue and release the key, you will have a stronger attack.";
                            //Text Name = Dialog.transform.GetChild (3).GetComponent<Text> ();
                            //Name.text = " ";
                        state = 3;
                    }
                    break;
                case 3:
                     if (Input.GetKeyDown(KeyCode.Return))
                     {
                        Text Dia_Text = Dialog.transform.GetChild(4).GetComponent<Text>();
                        Dia_Text.text = "Hold X until your star becomes blue and release the key, you will have a stronger attack.";
                        //Text Name = Dialog.transform.GetChild (3).GetComponent<Text> ();
                        //Name.text = " ";
                        state = 4;
                     }
                     break;
                case 4:
                     if (Input.GetKeyDown(KeyCode.Return))
                     {
                        Text Dia_Text = Dialog.transform.GetChild(4).GetComponent<Text>();
                        Dia_Text.text = "This will help you kill more nightmares.";
                        //Text Name = Dialog.transform.GetChild (3).GetComponent<Text> ();
                        //Name.text = " ";
                        state = 5;
                     }
                    break;
                case 5:
                     if (Input.GetKeyDown(KeyCode.Return))
                     {
                        Text Dia_Text = Dialog.transform.GetChild(4).GetComponent<Text>();
                        Dia_Text.text = "Again, thank you for your help.";
                        //Text Name = Dialog.transform.GetChild (3).GetComponent<Text> ();
                        //Name.text = " ";
                        state = 6;
                     }
                    break;
                case 6:
					if (Input.GetKeyDown (KeyCode.Return)) {
						Text Dia_Text = Dialog.transform.GetChild (4).GetComponent<Text> ();
						Dia_Text.text = "Peanut Archer's wish is granted!";
						Text Name = Dialog.transform.GetChild (3).GetComponent<Text> ();
						Name.text = " ";
						chr.SendMessage ("WishGrantUpdate");
						state = 7;
					}
					break;
				case 7:
					if (Input.GetKeyDown (KeyCode.Return)) {
						DestroyCanvas ();
						UnFreezeNim ();
						//DisplaySign = true;
						IsDiaOver = true;
						IsQuestTurnIn = true;
						state = 8;
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
			Right.sprite = PA;
			Text Name = Dialog.transform.GetChild (3).GetComponent<Text> ();
			Name.text = "Peanut Archer";
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
