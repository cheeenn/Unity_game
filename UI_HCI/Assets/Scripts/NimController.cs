using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NimController : MonoBehaviour
{
    //variables involving movement speed
    public float speed;
    public float groundSpeed;  //how fast Nim goes when he's on the ground
    public float airSpeed;  //how fast our boi goes in the air
    public float jumpForce;  //force of jump
    public float maxSpeed;  //maximum movement speed
    public float multiJumpForceMultiplier = 1000;
    public Vector3 respawnPosition;

    public Text timeLeft;
    public float timeToJump = 2;
    public float timeLeftToJump;

    //directions/getting sprites to work
    public int direction = 1;
    public Quaternion lookLeft = Quaternion.Euler(0, 0, 0);
    public Quaternion lookRight = Quaternion.Euler(0, 180, 0);

    //variables for jumping
    public int maxJumps = 3;
    public int jumpCount;
    public float nextJumpTime;  //need an offset time between jumps
    public float jumpBuffer = .2f;
    public float gravityConstant = -50;
    bool frozen = false;
    public bool active;
    float angle = 0;
    float jumpDir;

    private Animator animator;
    private Rigidbody rb;
    GameObject star;
    GameObject trail;
    GameObject arrow;

    enum states { defaultState, jump, multiJump, drop, dead, frozen, hurt };
    states currentState;

    public int maxHP;
    public int currentHP;
    public int invincibilityTime;
    public float invincibilityOffTime;
    bool invincible;
    public int delay = 100;
    public SpriteRenderer spriteRen;
    int counter = 101;
    bool toggle = false;
    // Use this for initialization

    public GameObject bulletPrefab;
    private float bulletDeletionTime;
    private float bulletForce;
    private float bulletAngle;

    void Start()
    {
        active = true;
          groundSpeed = 80;  //how fast Nim goes when he's on the ground
          airSpeed = 15;  //how fast our boi goes in the air
          jumpForce = 1140;  //force of jump
          maxSpeed = 15;  //maximum movement speed
          multiJumpForceMultiplier = 1000;
        jumpBuffer = .15f;
        timeToJump = 1f;
        maxHP = 3;
        currentHP = maxHP;
        invincibilityTime = 3;
        invincible = false;
        toggle = false;

        star = GameObject.Find("Star");
        trail = GameObject.Find("TrailTest");
        arrow = GameObject.Find("Arrow");
        timeLeft.text = "";
        respawnPosition = transform.position;
        rb = GetComponent<Rigidbody>();
        spriteRen = GetComponent<SpriteRenderer>();

        bulletDeletionTime = 1;
        bulletForce = 1000;

    rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxSpeed);  //set maximum velocity

        direction = 1;
        Physics.gravity = new Vector3(0, gravityConstant, 0);

        animator = this.GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(currentState);
        //Put all your code under Update() here.
        setAnimation();
        if (currentState != states.multiJump)
        {
            clearTrail();
        }

    }

    //for the physics
    private void FixedUpdate()
    {
        //determine current state
        if (rb.velocity.y < 0)
        {
            Physics.gravity = new Vector3(0, gravityConstant, 0);
        }
        if (isOnGround())
        {
            Physics.gravity = new Vector3(0, gravityConstant, 0);
            currentState = states.defaultState;
            jumpCount = maxJumps;
            if (currentState != states.jump && (Input.GetKeyDown(KeyCode.Space) || (Input.GetKeyDown(KeyCode.UpArrow)&&currentState!=states.multiJump) ))
            {
                currentState = states.jump;
            }
        }

        if (invincible && Time.time > invincibilityOffTime)
        {
            invincible = false;
        }

        if (invincible)
        {
            flashSprite();
        }
        else
        {
            spriteRen.enabled = true;
        }
        
        if (active)
        {
            setSpeed();
            // stop moving when release right or left key
            if (isOnGround() && (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.RightArrow)))
            {
                cancelMomentum();
            }

            //determing direction to face
            float mvmtHoriz = Input.GetAxis("Horizontal");
            float mvmtVert = Input.GetAxis("Vertical");

            //only allow movement if not frozen and not dead
            if (!frozen && currentState != states.dead)
            {
                walk(mvmtHoriz);
            }
            if (currentState == states.jump)
            {
                jump();
            }
            else if (!isOnGround() && jumpCount > 0 && Time.time > nextJumpTime)
            {
                multiJump();
            }
        }
        else
        {
            cancelMomentum();
        }
        

    }
    private void walk(float mvmtHoriz)
    {
        if (rb.velocity.magnitude > maxSpeed && isOnGround())
        {
            rb.velocity = rb.velocity.normalized * maxSpeed;
        }
        if (mvmtHoriz < 0 && direction == 1)
        {
            direction = -1;
            transform.rotation = lookRight;

        }
        else if (mvmtHoriz > 0 && direction == -1)
        {
            direction = 1;
            transform.rotation = lookLeft;
        }
        Vector3 movement = new Vector3(mvmtHoriz, 0, 0);

        // P's code:
        //rb.AddForce(movement * speed);

        // Y's change:
        rb.MovePosition(rb.position + movement / 7f);
    }
    private void jump()
    {
        cancelMomentum();
        currentState = states.defaultState;

        float mvmtJump = jumpForce;
        float mvmtHoriz = Input.GetAxis("Horizontal");

        // Y's change:
        // change from 120f to 110f
        Vector3 movement = new Vector3(mvmtHoriz * 110f, mvmtJump, 0);
        rb.AddForce(movement);
    }
    
    private void multiJump()
    {
        if (Input.GetKeyDown(KeyCode.Z) && !frozen)
        {

            frozen = true;
            timeLeftToJump = timeToJump;
        }
        else if (Input.GetKey(KeyCode.Z) && frozen && timeLeftToJump > 0)
        {
            timeLeftToJump -= Time.deltaTime;
            timeLeft.text = timeLeftToJump.ToString("F2");
            arrow.GetComponent<Renderer>().enabled = true;
            cancelMomentum();
            Physics.gravity = new Vector3(0, 0, 0);
            Vector3 movementDirection = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
            if (movementDirection.magnitude > 0)
            {
                angle = Vector3.Angle(Vector3.right, movementDirection);
                jumpDir = Mathf.Sign(Input.GetAxis("Vertical"));
            }
        }
        else if (Input.GetKeyUp(KeyCode.Z)&&frozen || (!Input.GetKey(KeyCode.Z) && frozen) || timeLeftToJump < 0)
        {
            timeLeft.text = "";
            arrow.GetComponent<Renderer>().enabled = false;
            timeLeftToJump = timeToJump;
            frozen = false;
            currentState = states.multiJump;
            cancelMomentum();
            Physics.gravity = new Vector3(0, gravityConstant, 0);
            Physics.gravity = Physics.gravity * .65f;
            star.GetComponent<starController>().hardResetPosition();
         
            Debug.Log(angle);
            Vector3 jumpyjump = new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad), jumpDir * Mathf.Sin(angle * Mathf.Deg2Rad), 0);
            Debug.Log(jumpyjump);
            rb.AddForce(multiJumpForceMultiplier * jumpyjump + Physics.gravity);
            jumpCount--;
            nextJumpTime = Time.time + jumpBuffer;
            
            GameObject bullet1 = (GameObject)Instantiate(bulletPrefab, transform.position, transform.rotation);
            Physics.IgnoreCollision(GetComponent<Collider>(), bullet1.GetComponent<Collider>());
            bullet1.transform.localScale = .8f* bullet1.transform.localScale;
            // Add velocity to the bullet
            bullet1.GetComponent<Rigidbody>().AddForce(-jumpyjump*bulletForce);
            Destroy(bullet1, bulletDeletionTime);
        }

    }
    void OnCollisionEnter(Collision col)
    {
		if (col.gameObject.name == "DeathPlane") {
			death ();
		} else if (col.gameObject.tag == "enermy" && !invincible || (col.gameObject.tag == "flying" && !invincible) || (col.gameObject.tag == "boss" && !invincible) || (col.gameObject.tag == "shootingenemy" && !invincible)) {
			takeDamage (col.gameObject.transform.position);
		} else if (col.gameObject.tag == "Platform" && Physics.Raycast (transform.position, -Vector3.up, .65f)) {
			//respawnPosition = transform.position;
		} 
        
    }
	void OnTriggerEnter(Collider col){
		if (col.gameObject.tag == "bossBullet" && !invincible) {
			takeDamage (col.gameObject.transform.position);
			Destroy (col.gameObject);
		}
        if (col.gameObject.tag == "heart")
        {
            if (currentHP < maxHP)
            {
                currentHP++;
                Destroy(col.gameObject);
            }
        }
    }

    public void takeDamage(Vector3 enemyPosition)
    {
        currentHP--;
        if(currentHP < 1)
        {
            death();
        }
        else
        {
            Debug.Log("owch");
            Vector3 knockbackDirection = new Vector3(-1*Mathf.Abs(transform.position.x - enemyPosition.x), Mathf.Abs(transform.position.y - enemyPosition.y), 0);
			//rb.AddForce (Vector3.up * 10f);
            invincibilityOffTime = Time.time + invincibilityTime;
            invincible = true;
            rb.AddForce(1200*(knockbackDirection));

        }
    }
    
    public void flashSprite()
    {
        Debug.Log("flash sprite");
        Debug.Log(counter);
        toggle = !toggle;
        if (toggle)
        {
            spriteRen.enabled = true;
        }
        else
        {
            spriteRen.enabled = false;
        }
    }
    public void death()
    {
        currentHP = maxHP;
        cancelMomentum();
        clearTrail();
        transform.position = respawnPosition;
    }

    private void cancelMomentum()
    {
        rb.velocity = new Vector3(0, 0, 0);
        rb.angularVelocity = new Vector3(0, 0, 0);
    }
    private void setAnimation()
    {      
        float mvmtHoriz = Input.GetAxis("Horizontal");
        if (mvmtHoriz != 0)
        {
            animator.SetInteger("State", 1);
        }
        else
        {
            animator.SetInteger("State", 0);
        }
    }
    public bool isOnGround()
    {
        return Physics.Raycast(transform.position, -Vector3.up, 1);
    }
    void clearTrail()
    {
        trail.GetComponent<TrailRenderer>().Clear();

    }
    void setSpeed()
    {
        if (isOnGround())
        {
            speed = groundSpeed;
        }
        else
        {
            speed = airSpeed;
        }
    }

}