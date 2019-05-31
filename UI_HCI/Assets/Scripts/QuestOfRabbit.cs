using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestOfRabbit : MonoBehaviour
{

	// public variables
	public GameObject chr;
	public Canvas Dialog_Prefab;
	public Sprite Nim;
	public Sprite Rabbit;
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

	private bool IsDiaOver;


	//
	// Fruit (0/1)

	//
	// * Pancakse's Fruit


	// Use this for initialization
	void Start()
	{
		NearJudge();
		DialogueEdit();
		DisplaySign = true;
	}

	// Update is called once per frame
	void Update()
	{
		NearJudge();
		DialogueEdit();
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
				if(!Sign)
					Sign = Instantiate (NPC_Sign, Sign_Canvas.transform);
				state = 0;
				DisplaySign = false;
			} else {
				if (Sign)
					Destroy (Sign);
			}
				if (Input.GetKeyDown (KeyCode.C) && !Dialog) {
					FreezeNim ();
					Destroy (Sign);
					GenerateDialog ();
					Text Dia_Text = Dialog.transform.GetChild (4).GetComponent<Text> ();
					Dia_Text.text = IsDiaOver? "Yaaaaaaaay" : "Wow!  You found me all the way up here.  You must be good at jumping.";
					state = IsDiaOver?3:1;
				}
				switch (state) {
				case 1:
					if (Input.GetKeyDown (KeyCode.Return)) {
						Text Dia_Text = Dialog.transform.GetChild (4).GetComponent<Text> ();
						Dia_Text.text = "I’ve been playing hide and seek. I’m so happy someone finally found me after five days of hiding!";
						state = 2;
					}
					break;
			//	case 2:
			//	if (Input.GetKeyDown (KeyCode.Return)) {
			//		Text Dia_Text = Dialog.transform.GetChild (4).GetComponent<Text> ();
			//		Dia_Text.text = "You must be super good at double jumping.";
			//		state = 3;
			//	}
			//		break;
			//	case 3:
			//	if (Input.GetKeyDown (KeyCode.Return)) {
			//		Text Dia_Text = Dialog.transform.GetChild (4).GetComponent<Text> ();
			//		Dia_Text.text = "I've been playing hide and seek.";
			//		state = 4;
			//	}
			//		break;
			//case 4:
			//	if (Input.GetKeyDown (KeyCode.Return)) {
			//		Text Dia_Text = Dialog.transform.GetChild (4).GetComponent<Text> ();
			//		Dia_Text.text = "I'm so happy someone finally found me after five days of hiding!";
			//		state = 5;
			//	}
			//	break;
			case 2:
				if (Input.GetKeyDown (KeyCode.Return)) {
					Text Dia_Text = Dialog.transform.GetChild (4).GetComponent<Text> ();
					Text Name = Dialog.transform.GetChild (3).GetComponent<Text> ();
					Name.text = " ";
					Dia_Text.text = "Tomato Bun's wish is granted!";
					chr.SendMessage ("WishGrantUpdate");
					state = 3;
				}
				break;
				case 3:
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
			}
		else {
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
			Right.sprite = Rabbit;
			Text Name = Dialog.transform.GetChild (3).GetComponent<Text> ();
			Name.text = "Tomato Bun";
		}
	}

	void DestroyCanvas (){
        if (Dialog)
        {
            Destroy(Dialog.gameObject);
        }
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
