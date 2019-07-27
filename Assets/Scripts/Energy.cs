using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Energy : MonoBehaviour
{
    public System.DateTime energyFullAt;

    public int energy = 0;

    IEnumerator Start()
    {
        yield return new WaitForEndOfFrame();
        if (PlayerPrefs.HasKey("Energy"))
        {
            energyFullAt = System.DateTime.Parse(PlayerPrefs.GetString("EnergyFullAt"));
            Debug.Log(energyFullAt.ToLongTimeString());
        }
        else
        {
            PlayerPrefs.SetInt("Energy", 7);
        }
    }

    void Update()
    {
        int oldEnergy = energy;
        energy = 7 - Mathf.Clamp((energyFullAt - System.DateTime.Now).Minutes / 5, 0, 7);

        if (oldEnergy != energy)
        {
            PlayerPrefs.SetInt("Energy", energy);
        }
    }

    public void DepleteEnergy()
    {
        if (!PlayerPrefs.HasKey("BrawlPro"))
        {
            if (PlayerPrefs.GetInt("Energy") > 0)
            {
                if (PlayerPrefs.GetInt("Energy") == 7)
                {
                    energyFullAt = System.DateTime.Now.AddMinutes(5);
                    PlayerPrefs.SetString("EnergyFullAt", energyFullAt.ToLongTimeString());
                    Debug.Log(energyFullAt.ToLongTimeString());
                }
                else
                {
                    energyFullAt = energyFullAt.AddMinutes(5);
                    PlayerPrefs.SetString("EnergyFullAt", energyFullAt.ToLongTimeString());
                    Debug.Log(energyFullAt.ToLongTimeString());
                }
                PlayerPrefs.SetInt("Energy", PlayerPrefs.GetInt("Energy") - 1);
            }
            else
            {
                GameObject confirm;
                if (GameObject.Find("Canvas/Energy"))
                {
                    confirm = GameObject.Find("Canvas/Energy");
                }
                else if (GameObject.Find("PlayerUI/Energy"))
                {
                    confirm = GameObject.Find("PlayerUI/Energy");
                }
                else
                {
                    confirm = null;
                }
                if (confirm != null)
                {
                    confirm.transform.GetChild(1).GetChild(0).GetComponent<TMPro.TMP_Text>().text = "No Energy!";
                    confirm.transform.GetChild(1).GetChild(1).GetChild(1).GetComponent<TMPro.TMP_Text>().text = "You have used all your energy :(, would you like to watch an ad to refill it instantly? P.S; It will refill by itself over time.";
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
                    confirm.transform.GetChild(1).GetChild(1).GetChild(2).GetComponent<Button>().onClick.AddListener(WatchAd);
                }
            }
        }
        PlayerPrefs.Save();
    }

    public void WatchAd()
    {
        FindObjectOfType<ShowAds>().Energy();
        energyFullAt = System.DateTime.Now;
        PlayerPrefs.SetString("EnergyFullAt", energyFullAt.ToLongTimeString());
        Debug.Log(energyFullAt.ToLongTimeString());
    }
}
