using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Itemtype { mana , health, fruit};


public class Item : MonoBehaviour {

    public Itemtype type;
    public Sprite spriteneutral;
    public Sprite spritehighlighted;
    public int maxsize;

	public void use()
    {
        switch (type)
        {
            case Itemtype.mana:
                Debug.Log("i just used a mana potion");
                break;
            case Itemtype.fruit:
                Debug.Log("i just used a fruit potion");
                break;
            case Itemtype.health:
                Debug.Log("i just used a health potion");
                break;
			default:
				break;

        }

    }
}
