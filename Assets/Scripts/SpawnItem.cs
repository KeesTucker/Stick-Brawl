using Mirror;
using UnityEngine;

public class SpawnItem : NetworkBehaviour {

    public GameObject switchedWeapon;
    public SpawnWeapons spawnWeapons;
    public GameObject items;
    public GameObject WeaponItem;
    public LocaliseTransform localiseTransform;
    public Transform aim;
    public float force;

    // Use this for initialization
    void Start () {
        spawnWeapons = GameObject.Find("Items").GetComponent<SpawnWeapons>();
        items = GameObject.Find("Items");
        localiseTransform = GameObject.Find("Items").GetComponent<LocaliseTransform>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void CmdSpawnDropped(GameObject ItemPrefab, Vector3 position, int id, float direction, int bulletsLeft)
    {
        switchedWeapon = Instantiate(WeaponItem, position, Quaternion.identity);
        switchedWeapon.GetComponent<WeaponIndexHolder>().WeaponIndex = id;
        /*GameObject WeaponModel = Instantiate(
            spawnWeapons.Weapons[id].WeaponItem.itemModel,
            switchedWeapon.transform.position,
            switchedWeapon.transform.rotation);
        WeaponModel.transform.SetParent(switchedWeapon.transform);
        WeaponModel.transform.localPosition = new Vector3(0, 0, 0);*/
        switchedWeapon.transform.position = switchedWeapon.transform.position + new Vector3(direction, 1, 0);
        //WeaponModel.gameObject.layer = 11;
        switchedWeapon.GetComponent<BulletsLeft>().bullets = bulletsLeft;
        //for (int z = 0; z < WeaponModel.transform.childCount; z++)
        //{
        //    WeaponModel.transform.GetChild(z).gameObject.layer = 11;
        //}
        switchedWeapon.GetComponent<DamageDealer>().isAWeapon = true;
        switchedWeapon.GetComponent<DamageDealer>().weaponParticle = true;
        
        switchedWeapon.transform.SetParent(items.transform);
        Rigidbody rb = switchedWeapon.GetComponent<Rigidbody>();
        Vector3 dir = new Vector3(transform.position.x - aim.position.x, transform.position.y - aim.position.y, 0) * Random.Range(0.7f, 2.5f);
        int dirRot = Random.Range(-20, 20);
        for (int i = 0; i < 10; i++)
        {
            rb.AddForce(dir * -force * Time.deltaTime);
            rb.angularVelocity = new Vector3(0, 0, dirRot);
        }
        //localiseTransform.setTransformItem(WeaponModel, id);
        NetworkServer.Spawn(switchedWeapon);
        RpcSpawnDropped(switchedWeapon);
    }

    [ClientRpc]
    public void RpcSpawnDropped(GameObject gun)
    {
        gun.GetComponent<DamageDealer>().weaponParticle = true;
    }

    [Command]
    public void CmdSpawnKilled(GameObject ItemPrefab, Vector3 position, int id, float direction, int bulletsLeft)
    {
        if (id != 100 && SyncData.gameMode == 1)
        {
            switchedWeapon = Instantiate(WeaponItem, position, Quaternion.identity);
            switchedWeapon.GetComponent<WeaponIndexHolder>().WeaponIndex = id;
            /*GameObject WeaponModel = Instantiate(
                spawnWeapons.Weapons[id].WeaponItem.itemModel,
                switchedWeapon.transform.position,
                switchedWeapon.transform.rotation);
            WeaponModel.transform.SetParent(switchedWeapon.transform);
            WeaponModel.transform.localPosition = new Vector3(0, 0, 0);*/
            switchedWeapon.transform.position = switchedWeapon.transform.position + new Vector3(direction, 1, 0);
            //WeaponModel.gameObject.layer = 11;
            switchedWeapon.GetComponent<BulletsLeft>().bullets = bulletsLeft;
            //for (int z = 0; z < WeaponModel.transform.childCount; z++)
            //{
            //    WeaponModel.transform.GetChild(z).gameObject.layer = 11;
            //}
            switchedWeapon.transform.SetParent(items.transform);
            //localiseTransform.setTransformItem(WeaponModel, id);
            NetworkServer.Spawn(switchedWeapon);
        }
        else if (id != 100 && SyncData.gameMode == 2 && Random.Range(0, 2) == 1)
        {
            switchedWeapon = Instantiate(WeaponItem, position, Quaternion.identity);
            switchedWeapon.GetComponent<WeaponIndexHolder>().WeaponIndex = id;
            /*GameObject WeaponModel = Instantiate(
                spawnWeapons.Weapons[id].WeaponItem.itemModel,
                switchedWeapon.transform.position,
                switchedWeapon.transform.rotation);
            WeaponModel.transform.SetParent(switchedWeapon.transform);
            WeaponModel.transform.localPosition = new Vector3(0, 0, 0);*/
            switchedWeapon.transform.position = switchedWeapon.transform.position + new Vector3(direction, 1, 0);
            //WeaponModel.gameObject.layer = 11;
            switchedWeapon.GetComponent<BulletsLeft>().bullets = bulletsLeft;
            //for (int z = 0; z < WeaponModel.transform.childCount; z++)
            //{
            //    WeaponModel.transform.GetChild(z).gameObject.layer = 11;
            //}
            switchedWeapon.transform.SetParent(items.transform);
            //localiseTransform.setTransformItem(WeaponModel, id);
            NetworkServer.Spawn(switchedWeapon);
        }
    }
}
