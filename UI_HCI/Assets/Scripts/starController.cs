using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class starController : MonoBehaviour
{
    private Rigidbody rb;
    GameObject player;
    GameObject trail;
    float offset = 1f;
    public float maxDistFromNim = 1.5f;
    public Vector3 speed = new Vector3(3, 0, 0);
    public float shootDistance = 5;
  

    public float enemyDamage = 1f;

    private bool attack = false;
    private float nextFire;
    public int chargeCounter;
    public int chargeTime;
	private GameObject PA;
	private GameObject Pegaseas;

    public SpriteRenderer spriteRen;
    bool toggle = false;
    private Sprite[] sprites;

    public GameObject bulletPrefab;
    private float bulletDeletionTime;
    private float bulletForce;
    private float bulletAngle;
    NimController playerScript;
    // Use this for initialization
    void Start()
    {
        spriteRen = GetComponent<SpriteRenderer>();
        sprites = Resources.LoadAll<Sprite>("Art/stars");
        chargeTime = 150;
        rb = GetComponent<Rigidbody>();
        player = GameObject.Find("Nim");
        playerScript = player.GetComponent<NimController>();
        Physics.IgnoreCollision(player.GetComponent<Collider>(), GetComponent<Collider>());
		PA = GameObject.Find ("PeanutArcher");
		Pegaseas = GameObject.Find ("Pegaseas");
        spriteRen.sprite = sprites[0];

        bulletDeletionTime = 1f;
        bulletForce = 1000;
        bulletAngle = Mathf.Deg2Rad * 20;

}

    void Update()
    {
        Vector3 destination = player.transform.position;
        destination.x = destination.x - offset * playerScript.direction;
        destination.y = destination.y + .2f;
        destination.z = destination.z - .2f;
        float dst = Vector3.Distance(destination, transform.position);
        if (dst > maxDistFromNim && !attack)
        {
            Vector3 vect = destination - transform.position;
            vect = vect.normalized;
            vect *= (dst - maxDistFromNim);
            transform.position += vect;
        }


    }
    void OnTriggerEnter(Collider col)
    {
		if (col.gameObject.name == "Enemy(Clone)" || col.gameObject.tag == "enermy") 
		{
			col.transform.GetComponent<EnemyAI> ().TakeDamageAI (enemyDamage);
			//Destroy(col.gameObject)
			Debug.Log ("Killenemy");
            //PA.SendMessage ("QuestCounterUpdate");
		} 
		else if (col.gameObject.tag == "flying") 
		{
			col.transform.GetComponent<FlyingEnemy> ().TakeDamageFly (enemyDamage);
			//Pegaseas.SendMessage ("QuestCounterUpdatePegaseas");

		}
		else if (col.gameObject.tag == "boss") 
		{
			col.transform.GetComponent<BossAI> ().TakeDamageBoss (enemyDamage);
		}
		else if (col.gameObject.tag == "shootingenemy") 
		{
			col.transform.GetComponent<EnemyAIShoot> ().TakeDamageShoot (enemyDamage);
		}
        else
        {
            Physics.IgnoreCollision(col, GetComponent<Collider>());
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        Vector3 destination = player.transform.position;
        if (Input.GetKeyDown(KeyCode.X) && Time.time > nextFire)
        {
            attack = true;
            nextFire = Time.time + .7f;
            transform.position = destination;
        }

        if (attack)
        {
            shoot();
        }
        else
        {
            resetPosition();
        }
        if(Input.GetKey(KeyCode.X) && !attack)
        {
            chargeCounter++;
            if(chargeCounter > chargeTime)
            {
                Debug.Log("charged");
                spriteRen.enabled = true;
                spriteRen.sprite = sprites[1];
            }
            else
            {
                Debug.Log("Charging...");
                flashSprite();
            }
        }
        else if (!Input.GetKey(KeyCode.X)&& chargeCounter > chargeTime)
        {
            chargeCounter = 0;
            spriteRen.sprite = sprites[0];
            chargedShot();
        }
        else
        {
            spriteRen.enabled = true;
            chargeCounter = 0;
        }

    }
    void resetPosition()
    {
        Vector3 destination = player.transform.position;
        destination.x = destination.x - offset * playerScript.direction;
        destination.y = destination.y + .2f;
        destination.z = destination.z - .2f;
        transform.position = Vector3.Lerp(rb.position, destination, 3 * Time.deltaTime);
    }
    public void hardResetPosition()
    {
        Vector3 destination = player.transform.position;
        destination.x = destination.x - offset * playerScript.direction;
        destination.y = destination.y + .2f;
        destination.z = destination.z - .2f;
        transform.position = destination;
    }
    public void shoot()
    {
        Vector3 destination = player.transform.position;
        destination.x = destination.x + shootDistance * playerScript.direction;
        transform.position = Vector3.Lerp(rb.position, destination, 8 * Time.deltaTime);
        if (Time.time > nextFire)
        {
            attack = false;
        }
    }
    public void chargedShot()
    {
        Debug.Log("BOOM");
        GameObject bullet1 = (GameObject)Instantiate(bulletPrefab, transform.position,transform.rotation);
        Physics.IgnoreCollision(player.GetComponent<Collider>(), bullet1.GetComponent<Collider>());
        // Add velocity to the bullet
        bullet1.GetComponent<Rigidbody>().AddForce(new Vector3(1, 0, 0) * bulletForce * playerScript.direction);

        GameObject bullet2 = (GameObject)Instantiate(bulletPrefab, transform.position, transform.rotation);
        Physics.IgnoreCollision(player.GetComponent<Collider>(), bullet2.GetComponent<Collider>());
        // Add velocity to the bullet
        bullet2.GetComponent<Rigidbody>().AddForce(new Vector3(Mathf.Cos(bulletAngle), Mathf.Sin(bulletAngle), 0) * bulletForce * playerScript.direction);

        GameObject bullet3 = (GameObject)Instantiate(bulletPrefab, transform.position, transform.rotation);
        Physics.IgnoreCollision(player.GetComponent<Collider>(), bullet3.GetComponent<Collider>());
        Physics.IgnoreCollision(bullet1.GetComponent<Collider>(), bullet3.GetComponent<Collider>());
        Physics.IgnoreCollision(bullet2.GetComponent<Collider>(), bullet3.GetComponent<Collider>());
        Physics.IgnoreCollision(bullet1.GetComponent<Collider>(), bullet2.GetComponent<Collider>());
        // Add velocity to the bullet
        bullet3.GetComponent<Rigidbody>().AddForce(new Vector3(Mathf.Cos(bulletAngle), -1*Mathf.Sin(bulletAngle), 0)*bulletForce * playerScript.direction);

        Destroy(bullet1, bulletDeletionTime);
        Destroy(bullet2, bulletDeletionTime);
        Destroy(bullet3, bulletDeletionTime);

    }
    public void flashSprite()
    {
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
}
