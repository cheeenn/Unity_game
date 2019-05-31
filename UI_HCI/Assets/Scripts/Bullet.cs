using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
	Rigidbody rb;
	public float BulletSpeed;
	public GameObject bullet;
	public GameObject bullet2;

	void Start () {
		rb = GetComponent<Rigidbody> ();
		rb.AddRelativeForce (0, 0, BulletSpeed, ForceMode.Impulse);
		bullet = GameObject.Find ("Bullet");
		bullet2 = GameObject.Find ("Bullet 2(Clone)");
		Destroy (bullet, 5);
		Destroy (bullet2, 5);
		


	}
	

}
