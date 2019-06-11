using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseDialogue : MonoBehaviour
{
    public GameObject parent;
    public void Destroy()
    {
        parent.GetComponent<Animator>().SetTrigger("Exit");
        StartCoroutine(Animate());
    }

    public void Hide()
    {
        parent.GetComponent<Animator>().SetTrigger("Exit");
    }

    IEnumerator Animate()
    {
        yield return new WaitForSeconds(1f);
        Destroy(parent);
    }
}
