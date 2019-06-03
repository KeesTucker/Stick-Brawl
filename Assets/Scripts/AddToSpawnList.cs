using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddToSpawnList : MonoBehaviour {

	// Use this for initialization
	IEnumerator Start () {
        while (!GameObject.Find("Items"))
        {
            yield return null;
        }
        GameObject.Find("Items").GetComponent<SpawnWeapons>().spawns.Add(gameObject);
	}
}
