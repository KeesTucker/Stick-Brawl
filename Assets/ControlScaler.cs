using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlScaler : MonoBehaviour
{
    public RectTransform move;
    public RectTransform gun;

    void Start()
    {
        if (PlayerPrefs.HasKey("scaleMove"))
        {
            float scale = PlayerPrefs.GetFloat("scaleMove");
            move.localScale = new Vector2(scale, scale);
            SyncData.moveScale = scale;
        }
        else
        {
            PlayerPrefs.SetFloat("scaleMove", 1f);
            SyncData.moveScale = 1f;
        }
        if (PlayerPrefs.HasKey("scaleGun"))
        {
            float scale = PlayerPrefs.GetFloat("scaleGun");
            gun.localScale = new Vector2(scale, scale);
            SyncData.gunScale = scale;
        }
        else
        {
            PlayerPrefs.SetFloat("scaleGun", 1f);
            SyncData.gunScale = 1f;
        }

        if (PlayerPrefs.HasKey("fixedFloating"))
        {
            if (PlayerPrefs.GetInt("fixedFloating") == 1)
            {
                SyncData.fixedFloating = true;
            }
            else
            {
                SyncData.fixedFloating = false;
            }
        }
        else
        {
            PlayerPrefs.SetInt("fixedFloating", 0);
            SyncData.fixedFloating = false;
        }

        if (PlayerPrefs.HasKey("moveJX"))
        {
            move.localPosition = new Vector2(PlayerPrefs.GetFloat("moveJX"), PlayerPrefs.GetFloat("moveJY"));
        }
        else
        {
            PlayerPrefs.SetFloat("moveJX", transform.localPosition.x);
            PlayerPrefs.SetFloat("moveJY", transform.localPosition.y);
        }

        if (PlayerPrefs.HasKey("gunJX"))
        {
            gun.localPosition = new Vector2(PlayerPrefs.GetFloat("gunJX"), PlayerPrefs.GetFloat("gunJY"));
        }
        else
        {
            //PlayerPrefs.SetFloat("gunJX", transform.position.x);
            //PlayerPrefs.SetFloat("gunJY", transform.position.y);
        }
    }
}
