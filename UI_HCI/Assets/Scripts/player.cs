using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
//using UnityStandardAssets.Characters.ThirdPerson;
using UnityEngine.SceneManagement;

public class player : MonoBehaviour {

	public bool alive = true;


	void OnTriggerEnter(Collider other){
		if (other.gameObject.name == "BossVision") {
			other.transform.parent.GetComponent<BossAI> ().checkSight ();
		}
		else if(other.gameObject.name == "EnemyVision") {
			other.transform.parent.GetComponent<EnemyAIShoot> ().checkSight ();
		}
	}


}
