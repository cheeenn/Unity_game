
using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class inventory : MonoBehaviour {
    
    private RectTransform inventoryrect;
    private float inventorywidth, inventoryhight;
    public int slots;
    public int rows;
    public float slotpaddingleft, slotpaddingtop;
    public float slotsize;
    public GameObject slotprefab;
    private List<GameObject> allslots;
    private static int emptyslots;

    public static int EmptySlots
    {
        get { return emptyslots; }
        set { emptyslots = value; }
    }

	// Use this for initialization
	void Start () {
        CreateLayout();
    }
	
	// Update is called once per frame
	void Update () {
        

    }
    private void CreateLayout()
    {
        // slot: number of slot;
        // row:  number of row
        // slotsize: the size of each slot
        // slotpaddingleft: the left space between each slot

        allslots = new List<GameObject>();
        emptyslots = slots;
        inventorywidth = (slots / rows) * (slotsize + slotpaddingtop) + slotpaddingleft;

        inventoryhight = rows * (slotsize + slotpaddingtop) + slotpaddingtop;

        inventoryrect = GetComponent<RectTransform>();
        inventoryrect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal,inventorywidth);
        inventoryrect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, inventoryhight);
        int columns = slots / rows;
        for (int y=0;y<rows;y++)
        {
            for (int x = 0;x<columns;x++)
            {
                GameObject newslot = (GameObject)Instantiate(slotprefab);

                RectTransform slotrect = newslot.GetComponent<RectTransform>();
                newslot.name = "slot";
                newslot.transform.SetParent(this.transform.parent);
                slotrect.localPosition = inventoryrect.localPosition + new Vector3(slotpaddingleft * (x + 1) + (slotsize * x), -slotpaddingtop*(y+1)-(slotsize*y));

                slotrect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, slotsize);
                slotrect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, slotsize);
                allslots.Add(newslot);
            }
        }
    }


    public bool Additem(Item item)
    {
        if (item.maxsize ==1)
        {
            Placeempty(item);
            return true;
        }
        else
        {
            foreach(GameObject slot in allslots)
            {
                slot tmp = slot.GetComponent<slot>();
                if (!tmp.Isempty)
                {
                    if(tmp.currentitem.type == item.type && tmp. isavailabe)
                    {
                        tmp.Additem(item);
                        return true;
                    }
                }
            }
            if(emptyslots>0)
            {
                Placeempty(item);
            }
        }
        return false;

    }

    private bool Placeempty(Item item)
    {
        if(emptyslots >0)
        {
            foreach (GameObject slot in allslots)
            {
                slot tmp = slot.GetComponent<slot>();
                if (tmp.Isempty)
                {
                    tmp.Additem(item);
                    emptyslots--;
                    return true;
                }
            }
        }
        return false;
    }
}

