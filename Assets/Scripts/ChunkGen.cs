using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkGen : MonoBehaviour {

    public int numChunks = 10;

    public GameObject chunk;

    public int chunkWidth = 1000;

    public int chunkHeight = 40;

    public float blockWidth = 5.62f;

	// Use this for initialization
	IEnumerator Start () {
        transform.position = new Vector3(chunkWidth * numChunks * blockWidth / -2, -chunkHeight * blockWidth, 0);
        for (int i = 0; i < numChunks; i++)
        {
            GameObject spawnedChunk = Instantiate(
                chunk,
                transform.position + new Vector3(chunkWidth * blockWidth * i, 0, -7.5f),
                transform.rotation);
            spawnedChunk.transform.SetParent(transform);
            yield return new WaitForEndOfFrame();
        }
	}
}
