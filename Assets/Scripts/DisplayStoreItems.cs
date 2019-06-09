using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayStoreItems : MonoBehaviour
{
    public ShopItemType itemType;

    public Vector2 itemSize;

    public Vector2 internalSize;

    public ShopItem[] items;

    public GameObject prefab;

    public Color owned;
    public Color selected;

    public GameObject content;

    public int id;

    public Image[] buttonImages;
    public Button[] buttons;
    public MoveTextWithClick[] moveTextWithClicks;

    public void Switch()
    {
        StartCoroutine(Animate());

        if (itemType != ShopItemType.Currency)
        {
            PlayerPrefs.SetInt("lastShop", id);
        }
    }

    IEnumerator Animate()
    {
        content.GetComponent<Animator>().SetTrigger("Switch");

        for (int i = 0; i < buttonImages.Length; i++)
        {
            buttonImages[i].transform.GetChild(0).GetComponent<Image>().color = Color.grey;
            buttons[i].interactable = true;
            if (moveTextWithClicks[i].disabled)
            {
                moveTextWithClicks[i].disabled = false;
                moveTextWithClicks[i].Enable();
            }
        }

        if (itemType != ShopItemType.Currency)
        {
            buttonImages[id].transform.GetChild(0).GetComponent<Image>().color = Color.white;
            buttons[id].interactable = false;
            if (!moveTextWithClicks[id].disabled)
            {
                moveTextWithClicks[id].disabled = true;
                moveTextWithClicks[id].Disable();
            }
        }

        yield return new WaitForSeconds(0.2f);
        ChangeContent();
    }

    void ChangeContent()
    {
        for (int i = 0; i < content.transform.childCount; i++)
        {
            Destroy(content.transform.GetChild(i).gameObject);
        }

        content.GetComponent<GridLayoutGroup>().cellSize = itemSize;

        for (int i = 0; i < items.Length; i++)
        {
            GameObject currentCell = Instantiate(prefab, content.transform);
            
            if (itemType != ShopItemType.BrawlPro)
            {
                currentCell.GetComponent<BuyItem>().cost = items[i].cost;
                currentCell.GetComponent<BuyItem>().itemType = items[i].shopItemType;
                currentCell.GetComponent<BuyItem>().id = items[i].itemID;
                currentCell.GetComponent<BuyItem>().itemName = items[i].itemName;
                currentCell.GetComponent<BuyItem>().item = items[i];

                currentCell.transform.GetChild(0).GetComponent<TMPro.TMP_Text>().text = items[i].name;
                currentCell.transform.GetChild(1).GetComponent<Image>().color = items[i].backgroundColor;
                currentCell.transform.GetChild(2).GetComponent<Image>().color = items[i].foregroundColor;
                currentCell.transform.GetChild(2).GetComponent<Image>().sprite = items[i].sprite;
                currentCell.transform.GetChild(3).GetChild(0).GetComponent<TMPro.TMP_Text>().text = items[i].cost.ToString();

                if (PlayerPrefs.HasKey("Owned " + items[i].shopItemType.ToString() + " number " + items[i].itemID.ToString()) || items[i].cost == 0)
                {
                    currentCell.GetComponent<Image>().color = new Color(Color.green.r, Color.green.g, Color.green.b, 0.2f);
                    currentCell.transform.GetChild(3).GetChild(0).GetComponent<TMPro.TMP_Text>().text = "Owned!";
                    Destroy(currentCell.transform.GetChild(3).GetChild(1).gameObject);
                    currentCell.transform.GetChild(3).GetChild(0).GetComponent<RectTransform>().sizeDelta = new Vector2(170, currentCell.transform.GetChild(3).GetChild(0).GetComponent<RectTransform>().sizeDelta.y);
                    currentCell.transform.GetChild(3).GetChild(0).localPosition = new Vector2(0, currentCell.transform.GetChild(3).GetChild(0).localPosition.y);
                }
            }
            if (itemType == ShopItemType.Maps)
            {
                currentCell.transform.GetChild(2).GetComponent<RectTransform>().sizeDelta = internalSize;
            }
            if (itemType == ShopItemType.Currency)
            {
                Destroy(currentCell.transform.GetChild(3).GetChild(1).gameObject);
                currentCell.transform.GetChild(3).GetChild(0).GetComponent<RectTransform>().sizeDelta = new Vector2(170, currentCell.transform.GetChild(3).GetChild(0).GetComponent<RectTransform>().sizeDelta.y);
                currentCell.transform.GetChild(3).GetChild(0).localPosition = new Vector2(0, currentCell.transform.GetChild(3).GetChild(0).localPosition.y);
                currentCell.transform.GetChild(3).GetChild(0).GetComponent<TMPro.TMP_Text>().text = items[i].cost.ToString() + " USD";
                currentCell.transform.GetChild(3).GetChild(0).GetComponent<TMPro.TMP_Text>().color = Color.white;
                currentCell.GetComponent<BuyItem>().amount = items[i].amount;
                if (items[i].cost == 0)
                {
                    currentCell.transform.GetChild(3).GetChild(0).GetComponent<TMPro.TMP_Text>().text = "Free!";
                }
            }
        }
    }
}
