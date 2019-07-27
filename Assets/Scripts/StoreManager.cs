using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoreManager : MonoBehaviour
{
    public GameObject message;

    public ShopItem[] shopItems;

    // Start is called before the first frame update
    IEnumerator Start()
    {
        yield return new WaitForEndOfFrame();
        if (PlayerPrefs.HasKey("openCount"))
        {
            if (PlayerPrefs.GetString("lastOpenDate") != System.DateTime.Today.ToLongDateString())
            {
                GameObject confirm = Instantiate(message, GameObject.Find("Canvas").transform);

                PlayerPrefs.SetInt("Energy", 7);
                PlayerPrefs.SetString("EnergyFullAt", System.DateTime.Now.ToLongTimeString());

                confirm.transform.GetChild(1).GetChild(0).GetComponent<TMPro.TMP_Text>().text = "Daily Counters!";
                confirm.transform.GetChild(1).GetChild(1).GetChild(1).GetComponent<TMPro.TMP_Text>().text = "Thanks for logging in! Here is 125 counters!";
                confirm.GetComponent<Animator>().SetTrigger("Entry");
                foreach (Image image in confirm.GetComponentsInChildren<Image>())
                {
                    image.enabled = true;
                }
                foreach (TMPro.TextMeshProUGUI text in confirm.GetComponentsInChildren<TMPro.TextMeshProUGUI>())
                {
                    text.enabled = true;
                }
                foreach (TMPro.TMP_Text text in confirm.GetComponentsInChildren<TMPro.TextMeshProUGUI>())
                {
                    text.enabled = true;
                }
                if (confirm.GetComponent<Image>())
                {
                    confirm.GetComponent<Image>().enabled = true;
                }
                if (confirm.GetComponent<TMPro.TextMeshProUGUI>())
                {
                    confirm.GetComponent<TMPro.TextMeshProUGUI>().enabled = true;
                }
                if (confirm.GetComponent<TMPro.TMP_Text>())
                {
                    confirm.GetComponent<TMPro.TMP_Text>().enabled = true;
                }
                PlayerPrefs.SetInt("Counters", PlayerPrefs.GetInt("Counters") + 125);
                FindObjectOfType<CreditsDisplay>().UpdateAmount();
            }
            PlayerPrefs.SetString("lastOpenDate", System.DateTime.Today.ToLongDateString());
            PlayerPrefs.SetInt("openCount", PlayerPrefs.GetInt("openCount") + 1);
        }
        else
        {
            PlayerPrefs.SetInt("openCount", 1);

            int id = Random.Range(0, shopItems.Length);

            PlayerPrefs.SetInt("Owned " + ShopItemType.Color.ToString() + " number " + id.ToString(), 1);
            GameObject.Find("LoadingPlayer").GetComponent<ColourSetterLoad>().SetColor(shopItems[id].foregroundColor);
            foreach (SyncName syncName in FindObjectsOfType<SyncName>())
            {
                syncName.UpdateColor();
            }
        }
    }
}
