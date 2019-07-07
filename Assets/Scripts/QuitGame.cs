using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class QuitGame : MonoBehaviour
{
    public void QuitGameMethod()
    {
        Application.Quit();
    }

    public void Home()
    {
        NetworkManager.singleton.StopClient();
    }
}
