using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class player_for_inv : MonoBehaviour {
    //public float speed;
    public inventory inventory;
    //public Text wintext;
    public Text losetext;
   // public Text counttext;
    //private Vector3 start;
    public GameObject player;
    //private int count;
    public Transform canvas;


    public QuestofPancake pancake_quest;
    public Text jumptext;
    public NimController nim_get_count;

    [SerializeField]
    private float fillamount1;

    [SerializeField]
    private Image content1;

    [SerializeField]
    private float fillamount2;

    [SerializeField]
    private Image content2;

    [SerializeField]
    private float fillamount3;

    [SerializeField]
    private Image content3;
    private HPbar hpbar;

    // Use this for initialization
    void Start () {
        //start = player.transform.position;
        //CountText();
        //count = 0;
        jumptext.text = "";
    }
	
	// Update is called once per frame
	void Update () {
        //Handlemove();

        Handlerbar(fillamount1, fillamount2, fillamount3);
      
        multi_jump_count();

        if (nim_get_count.currentHP < 1)
        {
            fillamount1 = 0;
            fillamount2 = 0;
            fillamount3 = 0;
            lose_panel();
        }
        if (nim_get_count.currentHP == 1)
        {
            fillamount1 = 1;
            fillamount2 = 0;
            fillamount3 = 0;
        }
        if (nim_get_count.currentHP == 2)
        {
            fillamount1 = 1;
            fillamount2 = 1;
            fillamount3 = 0;
        }
       
        if (nim_get_count.currentHP == 3)
        {
            fillamount1 = 1;
            fillamount2 = 1;
            fillamount3 = 1;
        }
    }
    
    public void Handlemove()
    {
       // float translation = speed * Time.deltaTime;
        //transform.Translate(new Vector3(Input.GetAxis("Horizontal") * translation, 0, Input.GetAxis("Vertical") * translation));

    }

    
    private void OnTriggerEnter(Collider other)
    {
        //if (other.gameObject.CompareTag("pick up"))
        //{
            //这里有Other， 为与player 发生碰撞的 object   
        //    other.gameObject.SetActive(false);
           // count = count + 1;
            //每次变化都要重新给string
        //    CountText();
        //    inventory.Additem(other.GetComponent<Item>());
        //}
        if (other.tag =="DEATHPLANE")
        {
            //other.gameObject.CompareTag("enermy")||other.tag == "spike"||
            //  print("到了");
            fillamount1 = 0;
            fillamount2 = 0;
            fillamount3 = 0;
            //这里如果直接调用gameobject 就是player 本身
            //gameObject.SetActive(false);
            //player.transform.position = start;
            //gameObject.SetActive(true);
            lose_panel();
        }
        if (other.tag == "item")
        {
            
            inventory.Additem(other.GetComponent<Item>());
        }
        if (other.tag == "FRUIT")
        {
             
            inventory.Additem(other.GetComponent<Item>());
        }

        if (other.tag == "-hp")
        {
           // print("到了");
           // if (fillamount1 <= 0.1)
           // {
           //     fillamount1 = 0;
           //     lose_panel();
           // }
          //  fillamount1 = fillamount1 - 0.1f;
           
        }
    }

    void CountText()
    {
      // counttext.text = "score : " + count.ToString();
      // if (count == 11)
      //  {
       //     wintext.text = "winer winer , chicken dinner";
      //      canvas.gameObject.SetActive(true);
      //      Time.timeScale = 0;
      //  }
    }
    public void lose_panel()
    {
        losetext.text = "you lose";
        canvas.gameObject.SetActive(true);
        Time.timeScale = 0;
    }
    private void Handlerbar(float fillamount1, float fillamount2, float fillamount3)
    {
        content1.fillAmount = fillamount1;
        content2.fillAmount = fillamount2;
        content3.fillAmount = fillamount3;
    }


    public void multi_jump_count()
    {
        if (pancake_quest.IsQuestTurnIn)
        {
            jumptext.text = "multi jump left: " + nim_get_count.jumpCount.ToString();
        }
    }
}
