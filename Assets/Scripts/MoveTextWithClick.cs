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

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!disabled)
        {
            if (audioSource == null)
            {
                audioSource = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AudioSource>();
            }
            audioSource.PlayOneShot(audioSource.clip, SyncData.sfx / 100f * 0.6f);
            rectTransform.position = rectTransform.position - new Vector3(0, amountDown, 0);
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (!disabled)
        {
            rectTransform.position = rectTransform.position + new Vector3(0, amountDown, 0);
        }
    }

    void Start()
    {
        audioSource = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AudioSource>();
        if (disabled)
        {
            Disable();
        }
    }

    public void Disable()
    {
        if (disabled)
        {
            rectTransform.position = rectTransform.position - new Vector3(0, amountDown, 0);
        }
    }

    public void Enable()
    {
        if (!disabled)
        {
            rectTransform.position = rectTransform.position + new Vector3(0, amountDown, 0);
        }
    }
}
