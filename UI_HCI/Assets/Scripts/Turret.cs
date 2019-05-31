using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour {
	Transform Nim;
	public Transform turretEnd;
	public GameObject turretBulletPrefab;
	public GameObject turret;
	private float turretBulletForce;
	private float turretBulletDeleteTime;
	NimController playerDirection;
	Rigidbody rb;
	// Use this for initialization
	void Start () {
		Nim = GameObject.FindWithTag ("Player").transform;
		rb = GetComponent<Rigidbody> ();
		turretBulletForce = 1000;
		turretBulletDeleteTime = 3f;
		turret = GameObject.FindGameObjectWithTag ("Turret");

	}
	
	// Update is called once per frame
	void Update () {
		transform.LookAt (Nim);
	}

	void OnTriggerEnter(Collider other){
		if (other.gameObject.tag == "Player") {
			StartCoroutine ("shootNim");
		}
	}

	void OnTriggerExit(Collider other){
		if (other.gameObject.tag == "Player") {
			StopCoroutine ("shootNim");
		}
	}

//	IEnumerator Shooting(){
//		while (true) {
//			Instantiate (bullet, turretEnd.position, turretEnd.rotation);
//			bullet.tag = "EnemyBullet";
//			yield return new WaitForSeconds (2);
//
//		}
//	}

	IEnumerator shootNim(){
		GameObject turretBullet = (GameObject)Instantiate (turretBulletPrefab, turretEnd.position, turretEnd.rotation);
		turretBullet.GetComponent<Rigidbody> ().AddForce (new Vector3 (1, 0, 0) * turretBulletForce * playerDirection.direction);
		yield return new WaitForSeconds (1);
		//Destroy (turretBullet, turretBulletDeleteTime);
		Destroy (GameObject.FindGameObjectWithTag("turretBullet"), turretBulletDeleteTime);

	}


}
