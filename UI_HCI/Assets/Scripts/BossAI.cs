	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;
	using UnityEngine.AI;

	public class BossAI : MonoBehaviour {

	public Canvas hpcanvas;
	public float bossHealth = 5f;

	//shooting
	public GameObject bossBulletPrefab;
	private float bulletForce;
	private float bulletDeleteTime;
	public Transform bossShoot;
	NimController playerDirection;

	//sprite animation
	public SpriteRenderer spriteRen;
	bool toggle = false;
	private Sprite[] sprites;
	public int counter;

	public GameObject player;
	public GameObject star;
	private NavMeshAgent nav;
	private string state = "idle";
	private bool alive = true;
	public Transform vision;
	private float wait = 0f;
	private float alertness = 20f;
    public bool activated;

	//enemy spawn
	public GameObject enemy1;
	public GameObject spawnPoint1;

	//death animation
	public GameObject deathEffect;



	void Start () {
        activated = false;
		nav = gameObject.GetComponent<NavMeshAgent> ();
		nav.speed = 5f;
		player = GameObject.Find ("Nim");
		bulletForce = 250f;
		star = GameObject.Find ("Star");
		playerDirection = player.GetComponent<NimController>();

        spriteRen = GetComponent<SpriteRenderer>();
        sprites = Resources.LoadAll<Sprite>("Art/boss");
        spriteRen.sprite = sprites[1];
		deathEffect = GameObject.FindGameObjectWithTag("deathEffect");


    }
	//check if we can see player
	public void checkSight(){
		if (alive) {
			RaycastHit rayHit;
			if (Physics.Linecast (vision.position, player.transform.position, out rayHit))
				print("hit " + rayHit.collider.gameObject.name);
		if (rayHit.collider.gameObject.tag == "Player") {
				if (state != "kill") {
					state = "shoot";
					nav.speed = 7f;
					
				}
			}
		}
	}


	void Update () {
		//Debug.DrawLine (vision.position, player.transform.position, Color.green);
	//transform.LookAt(Star);
	if (alive) {
			//idle state 
			if (state == "idle") {
				Vector3 randomPosition = Random.insideUnitSphere * alertness; //picks a random place to walk to within a sphere of radius 20
				NavMeshHit hit;
				NavMesh.SamplePosition (transform.position + randomPosition, out hit, 20f, NavMesh.AllAreas);
			Vector3 forward = transform.TransformDirection(transform.forward);

			Vector3 toOther = hit.position - transform.position;
			float DotResult = Vector3.Dot(forward, toOther);
			if (DotResult > 0) {
				transform.localRotation = Quaternion.Euler (0, 180, 0);
				DotResult = 1;

			}
			else if (DotResult < 0 ) {
				transform.localRotation = Quaternion.Euler (0, 0, 0);
				DotResult = -1;
			

			}
				nav.SetDestination (hit.position);
				state = "walk";

			}
			//walk state
			if (state == "walk") {
				if (nav.remainingDistance <= nav.stoppingDistance && !nav.pathPending) {
					state = "search";
					wait = 1f;
				}

			}
			//search state
			if (state == "search") {
			if (wait > 0f) {
				wait -= Time.deltaTime;
			
			}
				else {
					state = "idle";
				}
			}

			//chase state
			if (state == "shoot") {
				InvokeRepeating ("shoot", 0.5f, 7f);
				//lose sight of player
				float distance = Vector3.Distance(transform.position, player.transform.position);
				if (distance > 10f) {
				CancelInvoke ("shoot");
					state = "search";
				}

			}

		}
	}

	void OnTriggerEnter(Collider col)
	{
		if (col.gameObject.tag == "bullet")
		{
			TakeDamageBoss(1f);
		}

	}

		
	public void TakeDamageBoss(float amount)
	{
		EnemyAppear ();
		bossHealth -= amount;
		if (bossHealth <= 0) {
			alive = false;
			Destroy(hpcanvas. gameObject);
			gameObject.SetActive(false);
			Instantiate (deathEffect, transform.position, Quaternion.identity);
            GameObject.Find("EndingTrigger").GetComponent<CutsceneEnding>().triggerCutscene();
			//Destroy (gameObject);
		}
		else
		{
			counter = 8;
			flashSprite();
		}
	}

	public void flashSprite()
	{

		toggle = !toggle;
		if(counter > 0)
		{
			if (toggle)
			{
				spriteRen.sprite = sprites[0];
			}
			else
			{
				spriteRen.sprite = sprites[1];
			}
			counter--;
		}
		else
		{
			spriteRen.sprite = sprites[1];
		}


	}
	public void shoot(){

	//Vector3 dir = player.transform.position - bossBulletPrefab.transform.position;
	GameObject bossBullet = (GameObject)Instantiate (bossBulletPrefab, bossShoot.position, bossShoot.rotation);
	bossBullet.GetComponent<Rigidbody> ().AddForce ((Vector3)(star.transform.position - bossShoot.position).normalized * bulletForce);
	Destroy (GameObject.Find("BossBullet(Clone)"), 4f);

	}
	void EnemyAppear(){
			Instantiate (enemy1, spawnPoint1.transform.position, Quaternion.identity);


	}

}
