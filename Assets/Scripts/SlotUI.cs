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
        HUDicon.color = Color.grey;
        icon.enabled = true;
    }

    public void ClearSlot()
    {
        item = null;
        if (HUDicon)
        {
            HUDicon.sprite = null;
            HUDicon.color = Color.clear;
        }
        icon.sprite = null;
        icon.enabled = false;
    }
}
