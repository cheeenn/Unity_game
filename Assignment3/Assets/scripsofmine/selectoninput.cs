using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class selectoninput : MonoBehaviour {
    public EventSystem eventsystem;
    public GameObject selectedobject;
    private bool buttonSelected;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetAxisRaw("Vertical")!= 0 && buttonSelected == false)
        {
            eventsystem.SetSelectedGameObject(selectedobject);
            buttonSelected = true;
        }
	}
    private void OnDisable()
    {
        buttonSelected = false;
    }
}
