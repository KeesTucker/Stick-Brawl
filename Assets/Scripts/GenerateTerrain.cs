using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class GenerateTerrain : NetworkBehaviour {

    public GameObject[] chunks;

    private List<GameObject> chunksEdit = new List<GameObject>();

    private GameObject[] chunksFinal;

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
            SyncData.worldSize = 1;
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
            if (!GameObject.Find("LocalConnection").GetComponent<PlayerManagement>().isCampaign)
            {
                bool hasActivated = false;
                for (int i = 0; i < SyncData.maps.Length; i++)
                {
                    if (SyncData.maps[i] == true)
                    {
                        hasActivated = true;
                    }
                }
                if (hasActivated)
                {
                    for (int i = 0; i < SyncData.maps.Length; i++)
                    {
                        if (SyncData.maps[i])
                        {
                            chunksEdit.Add(chunks[i]);
                        }
                    }
                }
            }
            else
            {
                chunksEdit.Add(chunks[SyncData.chunkID]);
                Debug.Log(chunksEdit[0].ToString());
            }

            chunksFinal = chunksEdit.ToArray();

            if (chunksFinal.Length > 1)
            {
                RandomizeArray(chunksFinal);
            }

            if (size > chunksFinal.Length)
            {
                int inital = chunksFinal.Length - 1;
                Array.Resize(ref chunksFinal, size);
                int p = 0;
                int x = 0;
                for (int i = inital; i < chunksFinal.Length; i++)
                {
                    while (p == x)
                    {
                        x = UnityEngine.Random.Range(0, inital);
                    }
                    chunksFinal[i] = chunksFinal[x];
                    p = x;
                }
            }
            else if(chunksFinal.Length != size)
            {
                Array.Resize(ref chunksFinal, size);
            }

            for (int i = 0; i < chunksFinal.Length; i++)
            {
                currentPosition -= chunksFinal[i].GetComponent<ChunkData>().width;
            }
            startPos = currentPosition;
            currentPosition = currentPosition / 2;

            for (int i = 0; i < chunksFinal.Length; i++)
            {
                GameObject chunk = Instantiate(chunksFinal[i], new Vector3(currentPosition, 0, 0), Quaternion.identity);
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
