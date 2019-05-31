using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointScript : MonoBehaviour {
    public bool triggered;
    GameObject player;
    SpriteRenderer spriteRen;
    private Sprite[] sprites;
    public GameObject[] checkpoints;
    void Awake()
    {
        triggered = false;
        player = GameObject.Find("Nim");
        spriteRen = GetComponent<SpriteRenderer>();
        sprites = Resources.LoadAll<Sprite>("Art/checkpoint");
    }

    // Use this for initialization
    void Start () {
        checkpoints = GameObject.FindGameObjectsWithTag("checkpoint");
        
    }
	
	// Update is called once per frame
	void Update () {
        if (triggered)
        {
            spriteRen.sprite = sprites[1];
        }
        else
        {
            spriteRen.sprite = sprites[0];

        }
	}
    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject==player)
        {
            foreach (GameObject checkpoint in checkpoints)
            {
                checkpoint.GetComponent<CheckpointScript>().triggered = false;
            }

            triggered = true;
            player.GetComponent<NimController>().respawnPosition = player.transform.position;
        }
        
    }
}
