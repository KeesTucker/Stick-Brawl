using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BiomeFinder : MonoBehaviour {

    GenerateTerrain generateTerrain;
    bool run = false;

	// Use this for initialization
	IEnumerator Start () {
        generateTerrain = GetComponent<GenerateTerrain>();
        while (!generateTerrain.done)
        {
            yield return null;
        }
        run = true;
    }
	
	// Update is called once per frame
	void Update () {
        if (run)
        {
            float width = generateTerrain.size / generateTerrain.BiomesCompare.Length;
            if (width < 1)
            {
                width = 1f;
            }
            int widthInt = (int)width;

            int biomeCount = 0;
            for (int i = 0; i < transform.childCount; i++)
            {
                if (transform.GetChild(i).GetChild(0).GetComponent<BiomeHolder>().isServer)
                {
                    transform.GetChild(i).GetChild(0).GetComponent<BiomeHolder>().biomeIndex = generateTerrain.BiomesCompare[Mathf.Clamp((int)(biomeCount / width), 0, generateTerrain.Biomes.Count - 1)].BiomeIndex;
                }
                
                transform.GetChild(i).GetChild(0).GetComponent<BiomeHolder>().GetBiome();
                biomeCount++;
            }
            run = false;
        }
	}
}
