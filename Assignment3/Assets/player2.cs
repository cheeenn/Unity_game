using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class player2 : MonoBehaviour {
    public float speed;
    public inventory inventory;
    public Text wintext;
    public Text losetext;
    public Text counttext;
    private Vector3 start;
    public GameObject player;
    private int count;
    public Transform canvas;

   
    [SerializeField]
    private float fillamount;

    [SerializeField]
    private Image content;
    private HPbar hpbar;

    // Use this for initialization
    void Start () {
        start = player.transform.position;
        CountText();
        count = 0;
    }
	
	// Update is called once per frame
	void Update () {
        Handlemove();

        Handlerbar(fillamount);

    }
    
    public void Handlemove()
    {
        float translation = speed * Time.deltaTime;
        transform.Translate(new Vector3(Input.GetAxis("Horizontal") * translation, 0, Input.GetAxis("Vertical") * translation));

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("pick up"))
        {
            //这里有Other， 为与player 发生碰撞的 object   
            other.gameObject.SetActive(false);
            count = count + 1;
            //每次变化都要重新给string
            CountText();
            inventory.Additem(other.GetComponent<Item>());
        }
        if (other.gameObject.CompareTag("enermy"))
        {

            //这里如果直接调用gameobject 就是player 本身
            fillamount = 0;
            //gameObject.SetActive(false);
            //player.transform.position = start;
            //gameObject.SetActive(true);
            lose_panel();
        }
        if (other.tag == "item")
        {
            
            inventory.Additem(other.GetComponent<Item>());
        }
        if (other.tag == "-hp")
        {
           // print("到了");
            if (fillamount <= 0.1)
            {
                fillamount = 0;
                lose_panel();
            }
            fillamount = fillamount - 0.1f;
           
        }
    }

    void CountText()
    {
        counttext.text = "score : " + count.ToString();
        if (count == 11)
        {
            wintext.text = "winer winer , chicken dinner";
            canvas.gameObject.SetActive(true);
            Time.timeScale = 0;
        }
    }
    public void lose_panel()
    {
        losetext.text = "you lose";
        canvas.gameObject.SetActive(true);
        Time.timeScale = 0;
    }
    private void Handlerbar(float fillamount)
    {
        content.fillAmount = fillamount;
    }


   
}
