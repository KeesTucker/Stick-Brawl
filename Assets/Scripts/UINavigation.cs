using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UINavigation : MonoBehaviour
{
    public int targetMenu;
    public GameObject[] menus;

    public GameObject enablePanel;
    public GameObject[] disablePanel;

    public bool isSelectable = false;

    public GameObject[] selectThisButtonPanels;

    private Image buttonImage;
    private Button button;
    private MoveTextWithClick moveTextWithClick;

    public bool startOpen;
    public GameObject startObject;

    public bool selected = false;

    public bool isChild = false;

    IEnumerator Start()
    {
        if (isSelectable)
        {
            buttonImage = transform.GetChild(0).GetComponent<Image>();
            button = GetComponent<Button>();
            moveTextWithClick = GetComponent<MoveTextWithClick>();
        }

        yield return new WaitForEndOfFrame();

        if (startOpen)
        {
            if (startObject)
            {
                Enable(startObject);
            }
        }
        /*else
        {
            if (startObject)
            {
                Disable(startObject);
            }
        }*/
    }

    public void Switch()
    {
        if (gameObject.name != "SinglePlayer")
        {
            SyncData.openCampaignScreen = false;
        }
        for (int i = 0; i < menus.Length; i++)
        {
            if ((menus[i].GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("IdleMinor") || menus[i].GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("IdleMain")) && targetMenu != i)// && gameObject.activeInHierarchy)
            {
                Disable(menus[i]);
            }
        }
        if (menus[targetMenu].GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("IdleMinorUp") || menus[targetMenu].GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("IdleMainDown"))
        {
            Enable(menus[targetMenu]);
        }
        
        for (int i = 0; i < disablePanel.Length; i++)
        {
            if (disablePanel[i].GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("IdlePanel") && disablePanel[i] != enablePanel)// && gameObject.activeInHierarchy)
            {
                Disable(disablePanel[i]);
            }
        }
        if (enablePanel.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("IdlePanelOut") || enablePanel.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("IdleMainDown"))
        {
            Enable(enablePanel);
        }

        if (!isChild)
        {
            foreach (GameObject button in GameObject.FindGameObjectsWithTag("ButtonMenu"))
            {
                if (button.GetComponent<UINavigation>())
                {
                    if (button.name != gameObject.name)
                    {
                        button.GetComponent<UINavigation>().selected = false;
                    }
                    else
                    {
                        button.GetComponent<UINavigation>().selected = true;
                    }
                }
            }

            selected = true;
        }
    }

    void Disable(GameObject target)
    {
        if (target.GetComponent<Animator>())
        {
            target.GetComponent<Animator>().SetTrigger("Exit");
        }
        StartCoroutine(WaitToFalse(target));
    }

    IEnumerator WaitToFalse(GameObject target)
    {
        yield return new WaitForSeconds(0.5f);
        foreach (Image image in target.GetComponentsInChildren<Image>())
        {
            image.enabled = false;
        }
        foreach (TMPro.TextMeshProUGUI text in target.GetComponentsInChildren<TMPro.TextMeshProUGUI>())
        {
            text.enabled = false;
        }
        foreach (TMPro.TMP_Text text in target.GetComponentsInChildren<TMPro.TextMeshProUGUI>())
        {
            text.enabled = false;
        }
        if (GetComponent<Image>())
        {
            GetComponent<Image>().enabled = false;
        }
        if (GetComponent<TMPro.TextMeshProUGUI>())
        {
            GetComponent<TMPro.TextMeshProUGUI>().enabled = false;
        }
        if (GetComponent<TMPro.TMP_Text>())
        {
            GetComponent<TMPro.TMP_Text>().enabled = false;
        }
    }

    void Enable(GameObject target)
    {
        foreach (Image image in target.GetComponentsInChildren<Image>())
        {
            image.enabled = true;
        }
        if (GetComponent<Image>())
        {
            GetComponent<Image>().enabled = true;
        }
        if (GetComponent<TMPro.TextMeshProUGUI>())
        {
            GetComponent<TMPro.TextMeshProUGUI>().enabled = true;
        }
        if (GetComponent<TMPro.TMP_Text>())
        {
            GetComponent<TMPro.TMP_Text>().enabled = true;
        }
        foreach (TMPro.TextMeshProUGUI text in target.GetComponentsInChildren<TMPro.TextMeshProUGUI>())
        {
            text.enabled = true;
        }
        foreach (TMPro.TMP_Text text in target.GetComponentsInChildren<TMPro.TextMeshProUGUI>())
        {
            text.enabled = true;
        }
        if (target.GetComponent<Animator>())
        {
            target.GetComponent<Animator>().SetTrigger("Entry");
        }
    }

    void OnGUI()
    {
        if (isSelectable)
        {
            if (selected)
            {
                buttonImage.color = Color.white;
                button.interactable = false;
                if (!moveTextWithClick.disabled)
                {
                    moveTextWithClick.disabled = true;
                    moveTextWithClick.Disable();
                }
            }
            else
            {
                buttonImage.color = Color.grey;
                button.interactable = true;
                if (moveTextWithClick.disabled)
                {
                    moveTextWithClick.disabled = false;
                    moveTextWithClick.Enable();
                }
            }
        }
    }
}
