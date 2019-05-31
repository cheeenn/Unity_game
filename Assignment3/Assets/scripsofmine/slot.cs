using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class slot : MonoBehaviour , IPointerClickHandler
{

    private Stack<Item> items;
    public Text stacktxt;

    public Sprite slotempty;
    public Sprite slothighlight;

    public bool Isempty
    {
        get
        {
            return items.Count == 0;

        }
        
    }

    public bool isavailabe
    {
        get { return currentitem.maxsize > items.Count; }

    }



    public Item currentitem
    {
        get { return items.Peek(); }

    }



	// Use this for initialization
	void Start () {
        items = new Stack<Item>();
        RectTransform slotrect = GetComponent<RectTransform>();
        RectTransform txtrect = stacktxt.GetComponent<RectTransform>();

        //60%字体大小
        int txtsclefactor = (int)(slotrect.sizeDelta.x * 0.6);

        stacktxt.resizeTextMaxSize = txtsclefactor;
        stacktxt.resizeTextMinSize = txtsclefactor;
        txtrect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal,slotrect.sizeDelta.x);
        txtrect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, slotrect.sizeDelta.y);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Additem(Item item)
    {
        items.Push(item);
        if(items.Count>1)
        {
            stacktxt.text = items.Count.ToString();
        }
        Changesprite(item.spriteneutral,item.spritehighlighted);

    }

    private void Changesprite(Sprite neutral, Sprite highlight)
    {
        GetComponent<Image>().sprite = neutral;
        SpriteState st = new SpriteState();
        st.highlightedSprite = highlight;
        st.pressedSprite = neutral;
        GetComponent<Button>().spriteState = st;
    }
    private void useitem()
    {
        //检测是否有东西可以使用
        if (!Isempty)
        {
            items.Pop().use();

            stacktxt.text = items.Count > 1 ? items.Count.ToString() : string.Empty;
            if(Isempty)
            {
                Changesprite(slotempty,slothighlight);
                inventory.EmptySlots++;
            }
        }
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if(eventData.button == PointerEventData.InputButton.Right)
        {
            useitem();
        }
    }
}
