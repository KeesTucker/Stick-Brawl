using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuyItem : MonoBehaviour
{
    public ShopItemType itemType;
    public int id;
    public int cost;
    public string itemName;
    public int amount;
    public ShopItem item;

    public void Start()
    {
        if (PlayerPrefs.HasKey(itemType.ToString() + "selected"))
        {
            if (PlayerPrefs.GetInt(itemType.ToString() + "selected") == id)
            {
                Select();
            }
        }
    }

    public void Buy()
    {
        if (itemType != ShopItemType.Currency)
        {
            if (PlayerPrefs.HasKey("Owned " + itemType.ToString() + " number " + id.ToString()))
            {
                DeselectAll();

                Select();
            }
            else
            {
                if (PlayerPrefs.GetInt("Counters") >= cost)
                {
                    //Display Confirm

                    //If Confirmed
                    Confirmed();
                }
                else
                {
                    //Display Not enough counters message

                    //If Confirmed
                    OpenIAP();
                }
            }
        }
        else
        {
            //Open Google IAP
            PurchaseIAP();
        }
        FindObjectOfType<CreditsDisplay>().UpdateAmount();
    }

    public void Confirmed()
    {
        Debug.Log("Bought " + itemType.ToString() + " number " + id.ToString());
        PlayerPrefs.SetInt("Owned " + itemType.ToString() + " number " + id.ToString(), 1);
        PlayerPrefs.SetInt("Counters", PlayerPrefs.GetInt("Counters") - cost);
        transform.GetChild(3).GetChild(0).GetComponent<TMPro.TMP_Text>().text = "Owned!";
        if (transform.GetChild(3).childCount >= 2)
        {
            Destroy(transform.GetChild(3).GetChild(1).gameObject);
        }
        transform.GetChild(3).GetChild(0).GetComponent<RectTransform>().sizeDelta = new Vector2(170, transform.GetChild(3).GetChild(0).GetComponent<RectTransform>().sizeDelta.y);
        transform.GetChild(3).GetChild(0).localPosition = new Vector2(0, transform.GetChild(3).GetChild(0).localPosition.y);
        GetComponent<Image>().color = Color.green;

        DeselectAll();

        Select();
    }

    public void OpenIAP()
    {
        GameObject.Find("Store/Credits").GetComponent<DisplayStoreItems>().Switch();
    }

    public void PurchaseIAP()
    {
        //Open Google IAP

        //On purchased
        ConfirmedIAP();
    }

    public void ConfirmedIAP()
    {
        Debug.Log("Bought " + amount.ToString() + " counters!");
        PlayerPrefs.SetInt("Counters", PlayerPrefs.GetInt("Counters") + amount);
    }

    public void DeselectAll()
    {
        foreach (BuyItem item in FindObjectsOfType<BuyItem>())
        {
            if (PlayerPrefs.HasKey("Owned " + item.itemType.ToString() + " number " + item.id.ToString()))
            {
                Color color = item.gameObject.GetComponent<Image>().color;
                item.gameObject.GetComponent<Image>().color = new Color(color.r, color.g, color.b, 0.2f);
            }
        }
    }

    public void Select()
    {
        if (itemType == ShopItemType.Color || itemType == ShopItemType.Skin)
        {
            Color color = GetComponent<Image>().color;

            GetComponent<Image>().color = new Color(color.r, color.g, color.b, 0.7f);

            PlayerPrefs.SetInt(itemType.ToString() + "selected", id);

            if (itemType == ShopItemType.Skin)
            {
                GameObject.Find("LoadingPlayer").GetComponent<SkinApply>().sprites = item.sprites;
                GameObject.Find("LoadingPlayer").GetComponent<SkinApply>().UpdateSkin();

                if (item.applyOnSprite != null)
                {
                    GameObject.Find("LoadingPlayer").GetComponent<ColourSetterLoad>().SetColor(item.applyOnSprite.foregroundColor);
                    PlayerPrefs.SetInt("Owned " + ShopItemType.Color.ToString() + " number " + item.applyOnSprite.itemID.ToString(), 1);
                    PlayerPrefs.SetInt(ShopItemType.Color.ToString() + "selected", item.applyOnSprite.itemID);
                    foreach (SyncName syncName in FindObjectsOfType<SyncName>())
                    {
                        syncName.UpdateColor();
                    }
                }
            }
            else
            {
                GameObject.Find("LoadingPlayer").GetComponent<ColourSetterLoad>().SetColor(item.foregroundColor);
                foreach (SyncName syncName in FindObjectsOfType<SyncName>())
                {
                    syncName.UpdateColor();
                }
            }
            if (GameObject.Find("LocalConnection"))
            {
                GameObject.Find("LocalConnection").GetComponent<PlayerManagement>().CmdUpdateColorAndName(SyncData.name, SyncData.color, PlayerPrefs.GetInt(ShopItemType.Skin.ToString() + "selected"));
            }
        }
    }
}
