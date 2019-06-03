using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class WeaponIndexHolder : NetworkBehaviour {

    public Transform items;
    public GameObject WeaponModel;
    public MeshCollider[] colliders;
    public Renderer[] renderers;
    public Material WeaponMat;

    [SyncVar]
    public int WeaponIndex = 9999;

    IEnumerator Start()
    {
        items = GameObject.Find("Items").transform;
        while (WeaponIndex == 9999)
        {
            yield return null;
        }
        GetModel();
        if (SyncData.gameMode == 2)
        {
            yield return new WaitForSeconds(30f);
            Destroy(gameObject);
        }
    }

    public void GetModel()
    {
        transform.parent = items;
        WeaponModel = Instantiate(
                items.gameObject.GetComponent<SpawnWeapons>().Weapons[WeaponIndex].WeaponItem.itemModel,
                transform.position,
                transform.rotation);
        WeaponModel.gameObject.layer = 11;
        for (int z = 0; z < WeaponModel.transform.childCount; z++)
        {
            WeaponModel.transform.GetChild(z).gameObject.layer = 11;
        }
        WeaponModel.transform.SetParent(transform);
        colliders = WeaponModel.transform.GetComponentsInChildren<MeshCollider>();
        /*foreach (MeshCollider collider in colliders)
        {
            collider.convex = true;
        }*/
        renderers = WeaponModel.transform.GetComponentsInChildren<Renderer>();
        foreach (Renderer renderer in renderers)
        {
            renderer.material = WeaponMat;
        }
        WeaponModel.transform.localPosition = items.gameObject.GetComponent<SpawnWeapons>().Weapons[WeaponIndex].WeaponItem.spawnPosition;
        WeaponModel.transform.localEulerAngles = items.gameObject.GetComponent<SpawnWeapons>().Weapons[WeaponIndex].WeaponItem.spawnRotation;
        WeaponModel.transform.localScale = items.gameObject.GetComponent<SpawnWeapons>().Weapons[WeaponIndex].WeaponItem.spawnScale * 2;
        items.gameObject.GetComponent<LocaliseTransform>().setTransformItem(gameObject.transform.GetChild(0).gameObject, WeaponIndex);
    }
}
