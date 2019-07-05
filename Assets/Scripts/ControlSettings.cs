using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ControlSettings : MonoBehaviour
{
    //Joysticks
    public TMPro.TMP_Text scaleMove;
    public TMPro.TMP_Text scaleGun;

    public RectTransform move;
    public RectTransform gun;

    public TMPro.TMP_Text fixedFloating;
    public TMPro.TMP_Text moveSticks;
    private bool moving;

    public CanvasGroup gunCanvas;
    public CanvasGroup moveCanvas;

    void Start()
    {
        if (PlayerPrefs.HasKey("scaleMove"))
        {
            float scale = PlayerPrefs.GetFloat("scaleMove");
            scaleMove.text = scale.ToString();
            move.localScale = new Vector2(scale, scale);
            SyncData.moveScale = scale;
        }
        else
        {
            PlayerPrefs.SetFloat("scaleMove", 1.5f);
            SyncData.moveScale = 1.5f;
        }
        if (PlayerPrefs.HasKey("scaleGun"))
        {
            float scale = PlayerPrefs.GetFloat("scaleGun");
            scaleGun.text = scale.ToString();
            gun.localScale = new Vector2(scale, scale);
            SyncData.gunScale = scale;
        }
        else
        {
            PlayerPrefs.SetFloat("scaleGun", 1.5f);
            SyncData.gunScale = 1.5f;
        }

        if (PlayerPrefs.HasKey("fixedFloating"))
        {
            if (PlayerPrefs.GetInt("fixedFloating") == 1)
            {
                SyncData.fixedFloating = true;
                fixedFloating.text = "Floating";
            }
            else
            {
                SyncData.fixedFloating = false;
                fixedFloating.text = "Fixed";
            }
        }
        else
        {
            PlayerPrefs.SetInt("fixedFloating", 0);
            SyncData.fixedFloating = false;
            fixedFloating.text = "Fixed";
        }
    }

    public void GunScaleDown()
    {
        GunScale(-0.1f);
    }
    public void GunScaleUp()
    {
        GunScale(0.1f);
    }

    void GunScale(float amount)
    {
        float scale = Mathf.Clamp((float)Math.Round(PlayerPrefs.GetFloat("scaleGun") + amount, 1, MidpointRounding.ToEven), 0.2f, 2.5f);
        scaleGun.text = scale.ToString();
        gun.localScale = new Vector2(scale, scale);
        SyncData.gunScale = scale;
        PlayerPrefs.SetFloat("scaleGun", scale);
    }

    public void MoveScaleDown()
    {
        MoveScale(-0.1f);
    }
    public void MoveScaleUp()
    {
        MoveScale(0.1f);
    }

    void MoveScale(float amount)
    {
        float scale = Mathf.Clamp((float)Math.Round(PlayerPrefs.GetFloat("scaleMove") + amount, 1, MidpointRounding.ToEven), 0.2f, 2.5f);
        scaleMove.text = scale.ToString();
        move.localScale = new Vector2(scale, scale);
        SyncData.moveScale = scale;
        PlayerPrefs.SetFloat("scaleMove", scale);
    }

    public void FixedFloating()
    {
        if (PlayerPrefs.GetInt("fixedFloating") == 1)
        {
            SyncData.fixedFloating = false;
            fixedFloating.text = "Fixed";
            PlayerPrefs.SetInt("fixedFloating", 0);
        }
        else
        {
            SyncData.fixedFloating = true;
            fixedFloating.text = "Floating";
            PlayerPrefs.SetInt("fixedFloating", 1);
        }
    }

    public void Move()
    {
        if (!moving)
        {
            moveSticks.text = "Tap to confirm";
            moving = true;
            gunCanvas.blocksRaycasts = true;
            gunCanvas.interactable = true;
            moveCanvas.blocksRaycasts = true;
            moveCanvas.interactable = true;
        }
        else
        {
            moveSticks.text = "Move sticks";
            moving = false;
            gunCanvas.blocksRaycasts = false;
            gunCanvas.interactable = false;
            moveCanvas.blocksRaycasts = false;
            moveCanvas.interactable = false;
        }
    }

    public void CloseMove()
    {
        if (moving)
        {
            Move();
        }
    }

    public void Reset()
    {
        SyncData.fixedFloating = false;
        fixedFloating.text = "Fixed";
        PlayerPrefs.SetInt("fixedFloating", 0);

        float scale = 1;

        scaleMove.text = scale.ToString();
        move.localScale = new Vector2(scale, scale);
        SyncData.moveScale = scale;

        scaleGun.text = scale.ToString();
        gun.localScale = new Vector2(scale, scale);
        SyncData.gunScale = scale;

        PlayerPrefs.SetFloat("scaleGun", scale);
        PlayerPrefs.SetFloat("scaleMove", scale);

        gunCanvas.transform.GetChild(0).GetComponent<MoveJoystick>().UpdatePos();
        moveCanvas.transform.GetChild(0).GetComponent<MoveJoystick>().UpdatePos();

        PlayerPrefs.SetFloat("moveJX", moveCanvas.transform.GetChild(0).localPosition.x);
        PlayerPrefs.SetFloat("moveJY", moveCanvas.transform.GetChild(0).localPosition.y);

        PlayerPrefs.SetFloat("gunJX", gunCanvas.transform.GetChild(0).localPosition.x); //PROBLEM
        PlayerPrefs.SetFloat("gunJY", gunCanvas.transform.GetChild(0).localPosition.y);
    }
}
