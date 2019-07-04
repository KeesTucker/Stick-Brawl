using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectWeapon : MonoBehaviour
{
    PlayerControl playerControl;
    // Start is called before the first frame update
    IEnumerator Start()
    {
        while (playerControl == null)
        {
            if (GameObject.Find("LocalPlayer"))
            {
                playerControl = GameObject.Find("LocalPlayer").GetComponent<PlayerControl>();
            }
            yield return null;
        }
    }

    public void SelectOne()
    {
        playerControl.scroll = 1;
        playerControl.one = true;
    }

    public void SelectTwo()
    {
        playerControl.scroll = 2;
        playerControl.two = true;
    }

    public void SelectThree()
    {
        playerControl.scroll = 3;
        playerControl.three = true;
    }

    public void SelectFour()
    {
        playerControl.scroll = 4;
        playerControl.four = true;
    }
}
