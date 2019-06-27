using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayersLeft : MonoBehaviour {

    public PlayerManagement playerManagement;

    public int players;

    public TMPro.TMP_Text text;

    public float time = 0;
    public int mins = 0;

    SpawnRocketAI spawnRocket;
    HealthAI health;

    public GameObject secondsSurvived;
    public TMPro.TMP_Text textSeconds;

	// Use this for initialization
	IEnumerator Start () {
        while (playerManagement == null)
        {
            yield return null;
        }
        players = playerManagement.totalPlayers;
        text.text = "Players Left: " + players.ToString();
        if (SyncData.gameMode == 2)
        {
            while (playerManagement.playerSpawnedReal == null)
            {
                yield return null;
            }
            health = playerManagement.playerSpawnedReal.GetComponent<HealthAI>();
            spawnRocket = playerManagement.playerSpawnedReal.GetComponent<SpawnRocketAI>();
        }
	}
	
	// Update is called once per frame
	void Update () {
        if (playerManagement)
        {
            if (SyncData.gameMode == 1)
            {
                if (players != playerManagement.totalPlayers)
                {
                    players = playerManagement.totalPlayers;
                    text.text = "Players Left: " + players.ToString();
                }
            }
            else if (SyncData.gameMode == 2 && spawnRocket.ready && health.health > 0)
            {
                time += Time.deltaTime;
                if (time > 60)
                {
                    time -= 60f;
                    mins++;
                }
                text.text = ((mins * 60) + (int)time).ToString();
            }

            if (SyncData.gameMode == 2 && spawnRocket.ready && health.health <= 0)
            {
                secondsSurvived.SetActive(true);
                textSeconds.text = "YOU SURVIVED " + ((mins * 60) + (int)time).ToString() + " SECONDS";
                if (PlayerPrefs.HasKey("secondsSurvived"))
                {
                    if (PlayerPrefs.GetInt("secondsSurvived") < (mins * 60) + (int)time)
                    {
                        PlayerPrefs.SetInt("secondsSurvived", (mins * 60) + (int)time);
                    }
                }
                else
                {
                    PlayerPrefs.SetInt("secondsSurvived", (mins * 60) + (int)time);
                }
            }
        }
	}
}
