using System.Collections.Generic;
using UnityEngine;
using Mirror;
using System.Collections;

public class SpawnWeapons : NetworkBehaviour {

    public GameObject WeaponItemPrefab;

    public int spawnNumber = 20;

    public int WeaponIndex;

    public Transform ItemsParent;

    public MeshCollider[] colliders;

    public Renderer[] renderers;

    public Object[] info;

    public Material WeaponMat;

    public GameObject WeaponModel;

    public List<GameObject> spawns = new List<GameObject>();
    
    // Use this for initialization

    public List<WeaponList> Weapons = new List<WeaponList>();

    public bool done = false;

    IEnumerator Start()
    {
        info = Resources.LoadAll("Items", typeof(Item));
        foreach (Object fileInfo in info)
        {
            Item item = (Item)fileInfo;
            Weapons.Add(new WeaponList { WeaponItem = item, WeaponIndex = item.id });
        }
        Weapons.Sort();
        done = true;
        
        if (isServer)
        {
            StartCoroutine("Wait");
        }

        if (SyncData.gameMode == 2)
        {
            while (true)
            {
                yield return new WaitForSeconds(30f);
                StartCoroutine("Wait");
            }
        }
    }

    IEnumerator Wait()
    {
        while (!GameObject.Find("LocalPlayer"))
        {
            yield return null;
        }
        SpawnRocketAI spawnRocket = GameObject.Find("LocalPlayer").GetComponent<SpawnRocketAI>();
        while (!spawnRocket.ready)
        {
            yield return null;
        }
        yield return new WaitForSeconds(0.3f);
        for (int i = 0; i < spawns.Count; i++)
        {
            WeaponIndex = Random.Range(0, Weapons.Count - 1);
            if (Random.Range(0, 5) == 2)
            {
                WeaponIndex = 15;
            }
            GameObject WeaponItem = Instantiate(
                WeaponItemPrefab,
                spawns[i].transform.position,
                transform.rotation);
            WeaponItem.GetComponent<WeaponIndexHolder>().WeaponIndex = WeaponIndex;
            WeaponItem.transform.SetParent(ItemsParent);
            WeaponItem.GetComponent<BulletsLeft>().bullets = Weapons[WeaponIndex].WeaponItem.magazineSize;
            NetworkServer.Spawn(WeaponItem);
        }
    }
}
