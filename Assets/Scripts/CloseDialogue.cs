using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CloseDialogue : MonoBehaviour
{
    public GameObject parent;
    public void Destroy()
    {
        parent.GetComponent<Animator>().SetTrigger("Exit");
        StartCoroutine(Wait(parent));
        StartCoroutine(Animate());
    }

    IEnumerator Wait(GameObject target)
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
        if (target.GetComponent<Image>())
        {
            target.GetComponent<Image>().enabled = false;
        }
        if (target.GetComponent<TMPro.TextMeshProUGUI>())
        {
            target.GetComponent<TMPro.TextMeshProUGUI>().enabled = false;
        }
        if (target.GetComponent<TMPro.TMP_Text>())
        {
            target.GetComponent<TMPro.TMP_Text>().enabled = false;
        }
    }

    public void Hide()
    {
        parent.GetComponent<Animator>().SetTrigger("Exit");
        StartCoroutine(Wait(parent));
    }

    IEnumerator Animate()
    {
        yield return new WaitForSeconds(1f);
        Destroy(parent);
    }
}
