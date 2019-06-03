using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameModeSet : MonoBehaviour {

    public StartGame startGame;
    public int gamemode;

    IEnumerator OnCollisionEnter(Collision collisionInfo)
    {
        if (collisionInfo.gameObject.layer == 24)
        {
            yield return new WaitForSeconds(1f);
            gameObject.SetActive(false);
            startGame.gamemode = gamemode;
        }
    }
}
