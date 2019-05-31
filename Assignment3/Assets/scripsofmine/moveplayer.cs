using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//You should have a basic scene with at least four platforms 
//and a player object that can move back and forth along the platforms. 

public class moveplayer : MonoBehaviour {
    public float speed;
    private Rigidbody rb;
    private Rigidbody en_rb;
    private Rigidbody en_rb2;
    public KeyCode Key;
    public float jumpspeed;
    public float fillmultiplier = 2.5f;
    public float lowjumpmultiplier = 2f;
    protected Collider coll;
    public GameObject player;
    public GameObject enermy;
    public GameObject enermy2;
    //public bool canjump = false;
    //public GameObject platform;
    private Vector3 start_pos;
    private Vector3 start;
    private float limit=0;
    private float limit2 = 0;
    private int count;
    public Text counttext;
    public Text wintext;
    public Text losetext;
    public Transform canvas;
    public Button image;
    public Button cancle;


    public inventory inventory;
    // Use this for initialization
    void Start () {
        count = 0;
        //canjump = false;
        en_rb = enermy.GetComponent<Rigidbody>();
        en_rb2 = enermy2.GetComponent<Rigidbody>();
        rb = player.GetComponent<Rigidbody>();//build connection 在 rigidbody 和 rb 之间,一般建立联系都少不了它
        coll = GetComponent<Collider>();
        start_pos = player.transform.position;
        start = player.transform.position;
        print("start="+start);
        CountText();
        // counttext.text = "score : " + count.ToString();
    }
	
	// Update is called once per frame
	void Update () {


        // if (rb.GetComponent<CharacterController>().isGrounded)
        //    print("We are grounded");

        Ray ray = new Ray(rb.transform.position, -rb.transform.up);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            // 如果射线与平面碰撞，打印碰撞物体信息  
          //  Debug.Log("碰撞对象: " + hit.collider.name);
            // 在场景视图中绘制射线  
           // Debug.DrawLine(ray.origin, hit.point, Color.yellow);
        }
       
      
        if (limit == 1)
        {
            player.transform.position = start;
            gameObject.SetActive(true);
            limit = 0;
        }



    }

    // Update for pythical movement
    void FixedUpdate()
    {

        //print("transform.position" + transform.position);
       // print("Vector3.down" + Vector3.down);
       // print("coll.bounds.extents.y + 0.1f" + (coll.bounds.extents.y + 2f));
        bool Grounded = Physics.Raycast(transform.position, Vector3.down, coll.bounds.extents.y + 1f);
        if (Input.GetMouseButtonDown(0))
            {
                print("ground?"+ Grounded);
                if (Grounded)
                {
                    
                        rb.GetComponent<Rigidbody>().velocity = jumpspeed * Vector3.up;
                        print("We are grounded");

            }
        }
        //判断物体状态
        if (rb.velocity.y < 0)//代表y轴负数，下落
        {
            // - 1 是因为系统初始有1的自重
            //print(Vector3.up);//(0,1,0)

            rb.velocity += Vector3.up * Physics.gravity.y * (fillmultiplier - 1) * Time.deltaTime;
            // print("到了");
        }
        else if (rb.velocity.y > 0 && Input.GetMouseButtonUp(0)) //如果跳起时，跳跃没有长按，则为小跳
        {
            rb.velocity += Vector3.up * Physics.gravity.y * (lowjumpmultiplier - 1) * Time.deltaTime;
            // print("到了");
        }





        float horizontalvalue = Input.GetAxis("Horizontal");   //左右
      
        float verticalvalue = Input.GetAxis("Vertical");      //上下
  
        //xz平面移动，垂直Y 方向受力为0
        Vector3 move = new Vector3(horizontalvalue, 0.0f, verticalvalue);

        rb.AddForce(move*speed);
        
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
    //遇到的Collider (也就是碰撞)，然后判断tag，如果属于tag，则消失

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "item")
        {
            inventory.Additem(other.GetComponent<Item>());
        }
        if (other.gameObject.CompareTag("enermy"))
        {
            limit = 1;
            //这里如果直接调用gameobject 就是player 本身
            gameObject.SetActive(false);
            print("i am here with limit=" + limit);
            player.transform.position = start;
            gameObject.SetActive(true);
            lose_panel();
        }
        if (other.gameObject.CompareTag("pick up"))
        {
            //这里有Other， 为与player 发生碰撞的 object   
            other.gameObject.SetActive(false);

            count = count + 1;
            //每次变化都要重新给string
            CountText();

        }


    }
}

//Destroy(other.gameObject);
