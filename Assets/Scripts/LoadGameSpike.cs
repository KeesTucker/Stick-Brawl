using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadGameSpike : MonoBehaviour {

    public StartGame startGame;

	void OnCollisionEnter(Collision info)
    {
        if (info.gameObject.layer == 24)
        {
            startGame.StartHost();
        }
    }
}
