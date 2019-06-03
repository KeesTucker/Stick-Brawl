using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlotUI : MonoBehaviour {

    public Image icon;
    public Image HUDicon;

    Item item;

    void Start()
    {

    }

	public void AddItem(Item newItem)
    {
        item = newItem;
        icon.sprite = item.icon;
        HUDicon.sprite = item.icon;
        icon.enabled = true;
    }

    public void ClearSlot()
    {
        item = null;
        if (HUDicon)
        {
            HUDicon.sprite = null;
        }
        icon.sprite = null;
        icon.enabled = false;
    }
}
