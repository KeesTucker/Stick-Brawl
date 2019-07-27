using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MoveTextWithClick : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public RectTransform rectTransform;

    public float amountDown = 5;

    public bool disabled = false;

    private AudioSource audioSource;

    public float amountDownScaled;

    public void OnPointerDown(PointerEventData eventData)
    {
        amountDownScaled = amountDown * Screen.currentResolution.height / 1080f;
        if (!disabled)
        {
            if (audioSource == null)
            {
                if (GameObject.FindGameObjectWithTag("MainCamera"))
                {
                    if (GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AudioSource>())
                    {
                        audioSource = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AudioSource>();
                        audioSource.PlayOneShot(audioSource.clip, SyncData.sfx / 100f * 0.6f);
                    }
                }
            }
            else
            {
                audioSource.PlayOneShot(audioSource.clip, SyncData.sfx / 100f * 0.6f);
            }
            
            rectTransform.position = rectTransform.position - new Vector3(0, amountDownScaled, 0);
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (!disabled)
        {
            rectTransform.position = rectTransform.position + new Vector3(0, amountDownScaled, 0);
        }
    }

    IEnumerator Start()
    {
        yield return new WaitForSeconds(0.1f);
        if (GameObject.FindGameObjectWithTag("MainCamera"))
        {
            if (GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AudioSource>())
            {
                audioSource = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AudioSource>();
            }
        }
        if (disabled)
        {
            Disable();
        }
    }

    public void Disable()
    {
        if (disabled)
        {
            rectTransform.position = rectTransform.position - new Vector3(0, amountDownScaled, 0);
        }
    }

    public void Enable()
    {
        if (!disabled)
        {
            rectTransform.position = rectTransform.position + new Vector3(0, amountDownScaled, 0);
        }
    }
}
