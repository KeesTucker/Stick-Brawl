using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class GenerateTerrain : NetworkBehaviour {

    public GameObject[] chunks;

    [SerializeField]
    public Biome[] BiomesCompare;

    public SyncListInt BiomesIndex = new SyncListInt();

    public GameObject loaderP;

    public UnityEngine.Object[] info;

    [SyncVar]
    public int size;

    public List<BiomeList> Biomes = new List<BiomeList>();
    public List<BiomeList> BiomesIndexes = new List<BiomeList>();

    public float currentPosition = 0;
    public bool done = false;

    [SyncVar]
    public float startPos;
	// Use this for initialization
	void Start () {
        if (isServer)
        {
            if (SyncData.gameMode == 2)
            {
                SyncData.worldSize = Mathf.Clamp(SyncData.worldSize / 3, 4, 6);
            }
            size = SyncData.worldSize;
        }
        info = Resources.LoadAll("Biomes", typeof(Biome));
        foreach (UnityEngine.Object fileInfo in info)
        {
            Biome biome = (Biome)fileInfo;
            Biomes.Add(new BiomeList { BiomeItem = biome, BiomeIndex = biome.BiomeIndex });
        }
        Biomes.Sort();
        BiomesIndexes = Biomes;
        if (isServer)
        {
            foreach (UnityEngine.Object fileInfo in info)
            {
                Biome biome = (Biome)fileInfo;
                Biomes.Add(new BiomeList { BiomeItem = biome, BiomeIndex = biome.BiomeIndex });
            }
            for (int i = 0; i < Biomes.Count; i++)
            {
                BiomesCompare[i] = Biomes[i].BiomeItem;
            }
            RandomizeBiome(BiomesCompare);
            for (int i = 0; i < BiomesCompare.Length; i++)
            {
                BiomesIndex.Add(BiomesCompare[i].BiomeIndex);
            }
        }
        if (isClient)
        {
            for (int i = 0; i < BiomesIndex.Count; i++)
            {
                BiomesCompare[i] = Biomes[BiomesIndex[i]].BiomeItem;
            }
        }
        if (isServer)
        {
            RandomizeArray(chunks);

            if (size > chunks.Length)
            {
                int inital = chunks.Length - 1;
                Array.Resize(ref chunks, size);
                int p = 0;
                int x = 0;
                for (int i = inital; i < chunks.Length; i++)
                {
                    while (p == x)
                    {
                        x = UnityEngine.Random.Range(0, inital);
                    }
                    chunks[i] = chunks[x];
                    p = x;
                }
            }
            else
            {
                Array.Resize(ref chunks, size);
            }

            for (int i = 0; i < chunks.Length; i++)
            {
                currentPosition -= chunks[i].GetComponent<ChunkData>().width;
            }
            startPos = currentPosition;
            currentPosition = currentPosition / 2;

            for (int i = 0; i < chunks.Length; i++)
            {
                GameObject chunk = Instantiate(chunks[i], new Vector3(currentPosition, 0, 0), Quaternion.identity);
                currentPosition += chunk.GetComponent<ChunkData>().width;
                NetworkServer.Spawn(chunk);
            }
        }
        done = true;
	}

    public void RandomizeArray(GameObject[] arr)
    {
        for (int i = arr.Length - 1; i >= 0; i--)
        {
            int r = UnityEngine.Random.Range(0, arr.Length);
            GameObject tmp = arr[i];
            arr[i] = arr[r];
            arr[r] = tmp;
        }
    }
    public void RandomizeBiome(Biome[] arr)
    {
        for (int i = arr.Length - 1; i >= 0; i--)
        {
            int r = UnityEngine.Random.Range(0, arr.Length);
            Biome tmp = arr[i];
            arr[i] = arr[r];
            arr[r] = tmp;
        }
    }
}
