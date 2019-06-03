using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class BiomeHolder : NetworkBehaviour {
    
    [SyncVar]
    public int biomeIndex;

    public Biome biome;

    public void GetBiome()
    {
        biome = GameObject.Find("Terrain").GetComponent<GenerateTerrain>().BiomesIndexes[biomeIndex].BiomeItem;

        ColorTerrain[] colorTerrains = transform.GetComponentsInChildren<ColorTerrain>();
        foreach (ColorTerrain ct in colorTerrains)
        {
            ct.biome = biome;
            ct.color();
        }

    }

}
