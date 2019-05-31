using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemy : MonoBehaviour {

	private NimController Nim;
	public float moveSpeed;
	public float nimRange;
	public LayerMask nimLayer;
	public bool playerInRange;
	private bool alive = true;
	//enemy health
	public float health = 1f;

	public Canvas hpcanvas;
	public SpriteRenderer spriteRen;
	bool toggle = false;
	private Sprite[] sprites;

	bool isDead;
	public int counter;

	//pegaseas
	private GameObject Pegaseas;

	//death effect
	public GameObject deathEffect;

	void Start () {
		Nim = FindObjectOfType<NimController> ();
		spriteRen = GetComponent<SpriteRenderer>();
		//Debug.Log(spriteRen.sprite.name);
		sprites = Resources.LoadAll<Sprite>("Enemies/"+ spriteRen.sprite.name);
		spriteRen.sprite = sprites[0];
		Pegaseas = GameObject.Find ("Pegaseas");
		deathEffect = GameObject.FindGameObjectWithTag("deathEffect");



	}
	
	// Update is called once per frame
	void Update () {

		playerInRange = Physics.CheckSphere (transform.position, nimRange, nimLayer);

		if (playerInRange && alive) {
			transform.position = Vector3.MoveTowards (transform.position, Nim.transform.position, moveSpeed * Time.deltaTime);
		}

		if (gameObject != null && alive)
		{
			flashSprite();
		}
	}

	void OnDrawGizmosSelected(){
		Gizmos.DrawSphere (transform.position, nimRange);
	}

	void OnTriggerEnter(Collider col)
	{
		if (col.gameObject.tag == "bullet")
		{
			TakeDamageFly(1f);
		}

	}

	public void TakeDamageFly(float amount)
	{
		health -= amount;
		if (health <= 0) {
			alive = false;
			Destroy(hpcanvas. gameObject);
			gameObject.SetActive(false);
			Instantiate (deathEffect, transform.position, Quaternion.identity);
			Pegaseas.SendMessage ("QuestCounterUpdatePegaseas");

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
