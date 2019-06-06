using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class MoveJoystick : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public bool isMoveJoystick;
    public bool positioning;

    public void OnPointerDown(PointerEventData eventData)
    {
        positioning = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        positioning = false;
    }

    void Start()
    {
        if (isMoveJoystick)
        {
            if (PlayerPrefs.HasKey("moveJX"))
            {
                transform.localPosition = new Vector2(PlayerPrefs.GetFloat("moveJX"), PlayerPrefs.GetFloat("moveJY"));
            }
            else
            {
                PlayerPrefs.SetFloat("moveJX", transform.localPosition.x);
                PlayerPrefs.SetFloat("moveJY", transform.localPosition.y);
            }
        }
        else
        {
            if (PlayerPrefs.HasKey("gunJX"))
            {
                transform.localPosition = new Vector2(PlayerPrefs.GetFloat("gunJX"), PlayerPrefs.GetFloat("gunJY"));
            }
            else
            {
                //PlayerPrefs.SetFloat("gunJX", transform.position.x);
                //PlayerPrefs.SetFloat("gunJY", transform.position.y);
            }
        }

    }

    void Update()
    {
        if (positioning && isMoveJoystick)
        {
            transform.position = new Vector3(Mathf.Clamp(Input.mousePosition.x, 0, Screen.width / 2), Input.mousePosition.y, Input.mousePosition.z);
            PlayerPrefs.SetFloat("moveJX", transform.localPosition.x);
            PlayerPrefs.SetFloat("moveJY", transform.localPosition.y);
        }
        else if (positioning)
        {
            transform.position = new Vector3(Mathf.Clamp(Input.mousePosition.x, Screen.width / 2, Screen.width), Input.mousePosition.y, Input.mousePosition.z);
            PlayerPrefs.SetFloat("gunJX", transform.localPosition.x);
            PlayerPrefs.SetFloat("gunJY", transform.localPosition.y);
        }
    }

    public void UpdatePos()
    {
        if (isMoveJoystick)
        {
            transform.position = new Vector3(256, 256, 0);
        }
        else
        {
            transform.position = new Vector3(Screen.width - 256, 256, 0); //PROBLEM
        }
    }
}
