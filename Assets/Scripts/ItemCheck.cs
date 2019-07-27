using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class ItemCheck : NetworkBehaviour {

    public List<GameObject> items = new List<GameObject>();
    public List<float> itemDistanceRefrences = new List<float>();

    public Transform ragdoll;

    public float minDistance = 10000000;
    public GameObject closestItem;
    public int indexMin;
    public int weaponIndex;

    [SyncVar]
    public GameObject NetworkedItem;

    public bool e;

    public float pickupDistance;

    public AimShootAI aimShoot;

    public ShootAI shoot;

    public GameObject weaponItem;

    public RefrenceKeeperAI refrenceKeeper;

    public SpawnWeapons spawnWeapons;

    public SwitchWeaponAI switchWeapon;

    public Transform aim;

    public float direction;

    UpdateUI updateUI;

    public DestroyGunAI destroyGun;

    public Item fists;

    public bool isPlayer;

    // Use this for initialization
    IEnumerator Start () {
        spawnWeapons = GameObject.Find("Items").GetComponent<SpawnWeapons>();
        updateUI = GameObject.Find("PlayerUI").GetComponent<UpdateUIRefrence>().updateUI;
        yield return new WaitForSeconds(0.3f);
        checkDistances();
    }
	
	// Update is called once per frame
	void Update () {
        if (e)
        {
            e = false;
            if (isServer)
            {
                checkDistances();
                if (minDistance < pickupDistance)
                {
                    PickupItem();
                }
                else if (refrenceKeeper.inventoryCount > 3)
                {
                    ThrowItem();
                }
                
            }
        }
        if (!closestItem)
        {
            checkDistances();
        }
	}

    public void checkDistances()
    {
        GameObject[] itemsArray = GameObject.FindGameObjectsWithTag("WeaponItem");
        //if (itemsArray.Length > items.Count)
        //{
        items.Clear();
        itemDistanceRefrences.Clear();
        for (int i = 0; i < itemsArray.Length; i++)
        {
            itemDistanceRefrences.Add(20);
            items.Add(itemsArray[i]);
        }
        //}
        minDistance = 100000;
        for (int i = 0; i < items.Count; i++)
        {
            itemDistanceRefrences[i] = Vector3.Distance(items[i].transform.position, ragdoll.position);
            if (itemDistanceRefrences[i] < minDistance)
            {
                minDistance = itemDistanceRefrences[i];
                closestItem = items[i];
                indexMin = i;
            }
        }
    }

    public void PickupItem()
    {
        if (aim.transform.position.x > transform.position.x)
        {
            direction = 12;
        }
        else
        {
            direction = -12f;
        }

        weaponIndex = closestItem.GetComponent<WeaponIndexHolder>().WeaponIndex;

        refrenceKeeper.inventoryCount++;
        refrenceKeeper.inventoryCount = Mathf.Clamp(refrenceKeeper.inventoryCount, 0, 5);

        if (refrenceKeeper.inventoryCount <= 4)
        {
            refrenceKeeper.activeSlot = refrenceKeeper.inventoryCount - 1;
            refrenceKeeper.weaponInventory[refrenceKeeper.activeSlot] = spawnWeapons.Weapons[weaponIndex].WeaponItem;
        }
        else
        {
            if (refrenceKeeper.weaponHeld)
            {
                GetComponent<SpawnItem>().CmdSpawnDropped(weaponItem, transform.position, refrenceKeeper.weaponInventory[refrenceKeeper.activeSlot].id, direction, refrenceKeeper.weaponInventory[refrenceKeeper.activeSlot].currentBullets);
            }
            int id = refrenceKeeper.weaponInventory[refrenceKeeper.activeSlot].id;
            refrenceKeeper.weaponInventory[refrenceKeeper.activeSlot] = spawnWeapons.Weapons[weaponIndex].WeaponItem;
        }
        itemDistanceRefrences.RemoveAt(indexMin);
        items.RemoveAt(indexMin);

        switchWeapon.Switch(weaponIndex);

        aimShoot.scale = spawnWeapons.Weapons[weaponIndex].WeaponItem.scale;
        aimShoot.position = spawnWeapons.Weapons[weaponIndex].WeaponItem.position;
        aimShoot.positionFlipped = spawnWeapons.Weapons[weaponIndex].WeaponItem.positionFlipped;
        shoot.fireRate = spawnWeapons.Weapons[weaponIndex].WeaponItem.fireRate;
        shoot.recoil = spawnWeapons.Weapons[weaponIndex].WeaponItem.recoil;
        shoot.impact = spawnWeapons.Weapons[weaponIndex].WeaponItem.impact;
        shoot.bulletsLeft[refrenceKeeper.activeSlot] = closestItem.GetComponent<BulletsLeft>().bullets;
        //Update Stats

        //updateUI.UpdateSlotsUI();
        //updateUI.HighlightSlotOnPickup(refrenceKeeper.activeSlot);
        if (hasAuthority && isPlayer)
        {
            updateUI.UpdateSlotsUI();
            updateUI.HighlightSlotOnPickup(refrenceKeeper.activeSlot);
        }
        RpcPickupWeapon(weaponIndex, closestItem.GetComponent<BulletsLeft>().bullets);

        Destroy(closestItem);
        
        checkDistances();
    }

    public void ThrowItem()
    {
        if (refrenceKeeper.weaponHeld)
        {
            if (aim.transform.position.x > transform.position.x)
            {
                direction = 12;
            }
            else
            {
                direction = -12f;
            }
            if (hasAuthority)
            {
                GetComponent<SpawnItem>().CmdSpawnDropped(weaponItem, transform.position, refrenceKeeper.weaponInventory[Mathf.Clamp(refrenceKeeper.activeSlot, 0, refrenceKeeper.weaponInventory.Count - 1)].id, direction, refrenceKeeper.weaponInventory[Mathf.Clamp(refrenceKeeper.activeSlot, 0, refrenceKeeper.weaponInventory.Count - 1)].currentBullets);
            }
            refrenceKeeper.weaponInventory[Mathf.Clamp(refrenceKeeper.activeSlot, 0, refrenceKeeper.weaponInventory.Count - 1)] = fists;
            switchWeapon.Switch(100);
            if (hasAuthority && isPlayer)
            {
                updateUI.UpdateSlotsUI();
                updateUI.HighlightSlotOnPickup(refrenceKeeper.activeSlot);
            }
        }
    }

    public void DestroyHealth()
    {
        refrenceKeeper.weaponInventory[refrenceKeeper.activeSlot] = fists;
        switchWeapon.Switch(100);
        if (hasAuthority && isPlayer)
        {
            updateUI.UpdateSlotsUI();
            updateUI.HighlightSlotOnPickup(refrenceKeeper.activeSlot);
        }
    }

    [ClientRpc]
    public void RpcPickupWeapon(int wI, int bulletsLeft)
    {
        if (!isServer)
        {
            weaponIndex = wI;
            refrenceKeeper.inventoryCount++;
            refrenceKeeper.inventoryCount = Mathf.Clamp(refrenceKeeper.inventoryCount, 0, 5);

            if (refrenceKeeper.inventoryCount <= 4)
            {
                refrenceKeeper.activeSlot = refrenceKeeper.inventoryCount - 1;
                refrenceKeeper.weaponInventory[refrenceKeeper.activeSlot] = spawnWeapons.Weapons[weaponIndex].WeaponItem;
            }
            else
            {
                int id = refrenceKeeper.weaponInventory[refrenceKeeper.activeSlot].id;
                refrenceKeeper.weaponInventory[refrenceKeeper.activeSlot] = spawnWeapons.Weapons[weaponIndex].WeaponItem;
            }

            switchWeapon.Switch(weaponIndex);

            aimShoot.scale = spawnWeapons.Weapons[weaponIndex].WeaponItem.scale;
            aimShoot.position = spawnWeapons.Weapons[weaponIndex].WeaponItem.position;
            aimShoot.positionFlipped = spawnWeapons.Weapons[weaponIndex].WeaponItem.positionFlipped;
            shoot.fireRate = spawnWeapons.Weapons[weaponIndex].WeaponItem.fireRate;
            shoot.recoil = spawnWeapons.Weapons[weaponIndex].WeaponItem.recoil;
            shoot.impact = spawnWeapons.Weapons[weaponIndex].WeaponItem.impact;
            shoot.bulletsLeft[refrenceKeeper.activeSlot] = bulletsLeft;
            //Update Stats

            if (hasAuthority && isPlayer)
            {
                updateUI.UpdateSlotsUI();
                updateUI.HighlightSlotOnPickup(refrenceKeeper.activeSlot);
            }
        }
    }
}
