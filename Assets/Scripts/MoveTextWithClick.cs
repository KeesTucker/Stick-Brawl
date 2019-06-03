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

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!disabled)
        {
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
