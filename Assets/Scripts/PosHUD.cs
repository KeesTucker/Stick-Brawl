using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PosHUD : MonoBehaviour {

    public GenerateTerrain terrain;
    public RectTransform playerMarker;
    public RectTransform world;
    public float time;
    public int timeMultiplier;
    public SpawnRocketAI spawnRocket;

    // Use this for initialization
    void Start () {
        terrain = GameObject.Find("Terrain").GetComponent<GenerateTerrain>();
        playerMarker = GameObject.Find("PlayerUI/HUD/Panel/PlayerMarker").GetComponent<RectTransform>();
        world = GameObject.Find("PlayerUI/HUD/Panel/World").GetComponent<RectTransform>();
        spawnRocket = GetComponent<SpawnRocketAI>();
    }
	
	// Update is called once per frame
	void Update () {
        if (spawnRocket.hasAuthority)
        {
            playerMarker.localPosition = new Vector3(((transform.position.x / Mathf.Abs(terrain.startPos)) * 320f), playerMarker.localPosition.y, 0);
            if (spawnRocket)
            {
                if (spawnRocket.ready)
                {
                    time += Time.deltaTime;
                    if (time > 60)
                    {
                        timeMultiplier++;
                        time = 0;
                    }
                    if (SyncData.gameMode == 1)
                    {
                        world.localScale = new Vector3(Mathf.Clamp(((((float)SyncData.worldSize * 245f) - ((time + (timeMultiplier * 60f)) * 7f)) / (Mathf.Abs(terrain.startPos / 2))) * 0.6f, 0, 0.6f), 1, 1);
                    }
                }
            }
        }
    }
}
//time = (SyncData.worldSize * 35) - Mathf.Abs(transform.position.x / 7)
//time - (SyncData.worldSize * 35) = - Mathf.Abs(transform.position.x) / 7
//(-time + (SyncData.worldSize * 35)) * 7 = transform.position.x
//((-(time + (timeMultiplier * 60f)) + ((float)SyncData.worldSize * 35f)) * 7) / (Mathf.Abs((float)terrain.startPos) * 616f)