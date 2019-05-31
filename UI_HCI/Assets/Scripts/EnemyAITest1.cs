using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
//using UnityStandardAssets.Characters.ThirdPerson;
using UnityEngine.SceneManagement;


public class EnemyAITest1 : MonoBehaviour {

	public GameObject Nim;
	private NavMeshAgent nav;
	private string state = "idle";
	private bool alive = true;
	public Transform vision;
	public GameObject enemy;
	public Rigidbody body;
	private float wait = 0f;
	private float alertness = 10f;

	//enemy health
	public float health = 3f;

	public Canvas hpcanvas;
	public SpriteRenderer spriteRen;
	bool toggle = false;
	private Sprite[] sprites;

	bool isDead;
	public int counter;

	//looking!
	private NimController NimPlayer;
	public float moveSpeed;
	public float nimRange;
	public LayerMask nimLayer;
	public bool playerInRange;

	void Start () {
		spriteRen = GetComponent<SpriteRenderer>();
		//Debug.Log(spriteRen.sprite.name);
		sprites = Resources.LoadAll<Sprite>("Enemies/"+ spriteRen.sprite.name);
		spriteRen.sprite = sprites[0];


		nav = gameObject.GetComponent<NavMeshAgent> ();
		nav.speed = 2.2f;
		Nim = GameObject.FindGameObjectWithTag ("Player");
		enemy = GameObject.FindGameObjectWithTag ("enermy");
		NimPlayer = FindObjectOfType<NimController> ();
		//body = enemy.GetComponent<Rigidbody> ();

	}
	//draws range checker
	void OnDrawGizmosSelected(){
		Gizmos.DrawSphere (transform.position, nimRange);
	}

	//check if we can see player
//	public void checkSight(){
//		if (alive) {
//			RaycastHit rayHit;
//			if (Physics.Linecast (vision.position, Nim.transform.position, out rayHit))
//				print("hit " + rayHit.collider.gameObject.name);
//			if (rayHit.collider.gameObject.name == "Nim") {
//				if (state != "kill") {
//					state = "chase";
//					nav.speed = 3.5f;
//
//				}
//			}
//		}
//	}

	void Update () {

		playerInRange = Physics.CheckSphere (transform.position, nimRange, nimLayer);

		if (alive && playerInRange) {
			transform.position = Vector3.MoveTowards (transform.position, Nim.transform.position, moveSpeed * Time.deltaTime);
		}
	}


	//    private void Update()
	//    {
	//        if (gameObject != null && alive)
	//        {
	//            flashSprite();
	//        }
	//        
	//    }

	void FixedUpdate () {
		//Debug.DrawLine (vision.position, player.transform.position, Color.green);
		if (gameObject != null && alive)
		{
			flashSprite();
		}
		if (alive && gameObject != null) {
			//idle state 
			if (state == "idle") {
				//get copy of forward vector (blue vector in world)
				//Vector3 forward = transform.forward;
				Debug.Log(alive);
				//Vector3 forward = transform.TransformDirection(enemy.transform.forward);

				//zero out the z component to only get the direction in the 
				//x, y plane
				//forward.z = 0;

				//picks a random place to walk to within a sphere of radius 20 
				Vector3 randomPosition = Random.insideUnitSphere * alertness; 
				NavMeshHit hit;
				NavMesh.SamplePosition (transform.position + randomPosition, out hit, 10f, NavMesh.AllAreas);
				//figure out what direction the enemy will go by using dot product
//				Vector3 toOther = hit.position - enemy.transform.position;
//				float DotResult = Vector3.Dot(forward, toOther);
//				if (DotResult > 0) {
//					enemy.transform.localRotation = Quaternion.Euler (0, 180, 0);
//					print ("looking right");
//
//				}
//				if (DotResult < 0 ) {
//					enemy.transform.localRotation = Quaternion.Euler (0, 0, 0);
//					print ("looking left!");
//
//				}
				nav.SetDestination (hit.position);
				state = "walk";

			}
			//walk state
			if (state == "walk") {
				if (nav.remainingDistance <= nav.stoppingDistance && !nav.pathPending) {
					state = "idle";
					wait = 2f;
				}

			}


			//chase state
			if (state == "chase") {
				nav.destination = Nim.transform.position;
				float distance = Vector3.Distance (transform.position, Nim.transform.position);
				if (distance > 10f) {
					state = "idle";
				}
				//kill player
				else if (nav.remainingDistance <= nav.stoppingDistance + 1f && !nav.pathPending) {
					state = "kill";
					Nim.GetComponent<player> ().alive = false;
					Nim.GetComponent<NimController> ().enabled = false;


					//NimController death = NimController ();
					//death.death ();
					//Invoke ("changelevel", 2f);

				}
			}

		}
	}
	void OnTriggerEnter(Collider col)
	{
		if (col.gameObject.tag == "bullet")
		{
			TakeDamage(1f);
		}

	}

	public void TakeDamage(float amount)
	{
		health -= amount;
		if (health <= 0) {
			alive = false;
			Destroy(hpcanvas. gameObject);
			gameObject.SetActive(false);

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
				spriteRen.sprite = sprites[1];
			}
			else
			{
				spriteRen.sprite = sprites[0];
			}
			counter--;
		}
		else
		{
			spriteRen.sprite = sprites[0];
		}


	}
}
