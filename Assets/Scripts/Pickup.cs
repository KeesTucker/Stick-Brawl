using System.Collections;
using UnityEngine;
using Mirror;
using System.Collections.Generic;

public class Pickup : NetworkBehaviour {

    public float PickupDistance; //Distance required to pick up an object
    public float RealDistance; //The distance to the object

    public Transform Weapon; //Transform of the Weapon item
    public GameObject WeaponObject; //Game Object of the Weapon item
    public GameObject WeaponHand; //The Game Object which holds all the players weapons which are toggled on and off
    public GameObject RealWeapon; //The Weapon to activate based on Weapon index
    public Transform Player;
    public Transform bulletPositioner; //The Game Object which serves as a spawn point for bullets
    public GameObject Weapon0; //Child Weapon items
    public GameObject Weapon1; // ""

    public int inventoryCount;

    [SyncVar]
    public bool deactivated = false; //A bool which determines if this object has been picked up

    public int listSizeGrabber; //The index of the closest Weapon

    [SyncVar]
    public int WeaponIndex = 99; //The index of the Weapon which this object represents

    public int NumberOfWeapons = 5; //Total number of Weapons

    public int individualCount; //Total number of Weapon items spawned in

    public float min; //Holds the minimum value of a list

    //Refrences to scripts:

    public Shoot shoot;
    public AimShoot aimShoot;
    public RefrenceKeeper refrenceKeeper;
    public ServerRefrenceKeeper serverRefrenceKeeper;
    public SpawnWeapons spawnWeapons;
    public LocaliseTransform localiseTransform;
    public SwitchWeapon switchWeapon;
    public UpdateUI updateUI;

    public Item currentItem;

    public Item Pistol;
    public Item TacticalSMG;
    public Item BoltActionSniper;
    public Item SemiAutoSniper;
    public Item HuntingRifle;

    public Transform Items;

    public bool switchSpawned;

    public GameObject switchedWeapon;

    public bool done = false;

    private GameObject WeaponModel;
    public List<WeaponList> Weapons = new List<WeaponList>();
    public MeshCollider[] colliders;
    public Renderer[] renderers;
    public Material WeaponMat;
    public GameObject mouseFollow;

    public float direction;

    IEnumerator Start()
    {
        yield return new WaitForEndOfFrame();
        mouseFollow = GameObject.Find("MouseFollower");
        Weapons = GameObject.Find("Items").GetComponent<SpawnWeapons>().Weapons;
        serverRefrenceKeeper = GameObject.Find("Server Refrences").GetComponent<ServerRefrenceKeeper>();
        spawnWeapons = GameObject.Find("Items").GetComponent<SpawnWeapons>();
        localiseTransform = GameObject.Find("Items").GetComponent<LocaliseTransform>();
        switchWeapon = GameObject.Find("Weapon").GetComponent<SwitchWeapon>();
        shoot = GameObject.Find("Weapon").GetComponent<Shoot>(); //Get script
        Items = GameObject.Find("Items").transform;
        bulletPositioner = GameObject.Find("Bullet Positioner").transform; //Get the bullet positioner object
        
        //Wait because some of the next parts have not completed yet

        aimShoot = GameObject.Find("Ragdoll").GetComponent<AimShoot>(); //Get script
        refrenceKeeper = GameObject.Find("Local").GetComponent<RefrenceKeeper>();
        refrenceKeeper.itemDistanceRefrences.Add(0); //Add a new space in the list for this item
        yield return new WaitForEndOfFrame();
        updateUI = refrenceKeeper.updateUI;

        WeaponHand = refrenceKeeper.WeaponParent;

        Player = GameObject.Find("Ragdoll").transform;

        if (isClient)
        {
            transform.parent = Items;
            WeaponModel = Instantiate(
                    Weapons[WeaponIndex].WeaponItem.itemModel,
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
            WeaponModel.transform.localPosition = Weapons[WeaponIndex].WeaponItem.spawnPosition;
            WeaponModel.transform.localEulerAngles = Weapons[WeaponIndex].WeaponItem.spawnRotation;
            WeaponModel.transform.localScale = Weapons[WeaponIndex].WeaponItem.spawnScale;
        }

        localiseTransform.setTransformItem(gameObject.transform.GetChild(0).gameObject, WeaponIndex);

        //Update global list of total items
        refrenceKeeper.itemCount++;

        NumberOfWeapons = serverRefrenceKeeper.numOfWeapons;

        individualCount = refrenceKeeper.itemCount; //Update private version of itemCount to add a private "id" so this script knows which part of the list is it's own entry;
        done = true;
    }

    // Update is called once per frame
    void Update () {
        if (done)
        {
            if (mouseFollow.transform.position.x > Player.position.x)
            {
                direction = 6f;
            }
            else
            {
                direction = -6f;
            }
            if (serverRefrenceKeeper.e) //If e is depressed and an object has not already been picked up
            {
                if (deactivated)
                {
                    RealDistance = 20;
                }
                else
                {
                    RealDistance = Vector3.Distance(Weapon.position, Player.position); //Get distance between player and this 
                }
                refrenceKeeper.itemDistanceRefrences[individualCount - 1] = RealDistance; //Update the public list of item distances
                if (RealDistance == 0)
                {
                    RealDistance = 20;
                }
                min = RealDistance;
                for (int i = 0; i < refrenceKeeper.itemDistanceRefrences.Count; i++)
                {
                    if (refrenceKeeper.itemDistanceRefrences[i] < min && refrenceKeeper.itemDistanceRefrences[i] != 0)
                    {
                        min = refrenceKeeper.itemDistanceRefrences[i];
                    }
                }
                if (RealDistance < PickupDistance && RealDistance <= min && serverRefrenceKeeper.e && !deactivated) //If item is within range and it is the closest object and an object hasnt already been picked up
                {
                    serverRefrenceKeeper.e = false; //Tell all other scripts an item has been picked up this keypress
                    deactivated = true;
                    refrenceKeeper.itemDistanceRefrences[individualCount - 1] = 20; //Remove the picked up object from the distances list

                    switchWeapon.Switch(WeaponIndex);

                    if (refrenceKeeper.weaponInventory.Count < 4)
                    {
                        inventoryCount = refrenceKeeper.weaponInventory.Count;
                        refrenceKeeper.weaponInventory.Add(spawnWeapons.Weapons[WeaponIndex].WeaponItem);
                        refrenceKeeper.activeSlot = refrenceKeeper.weaponInventory.Count - 1;
                        updateUI.HighlightSlotOnPickup(refrenceKeeper.activeSlot);
                        updateUI.UpdateSlotsUI();
                    }
                    else
                    {
                        int id = refrenceKeeper.weaponInventory[refrenceKeeper.activeSlot].id;
                        inventoryCount = 3;
                        /*switchedWeapon = Instantiate(gameObject, Player.position, Player.rotation);
                        switchedWeapon.GetComponent<Pickup>().WeaponIndex = refrenceKeeper.weaponInventory[refrenceKeeper.activeSlot].id;
                        Destroy(switchedWeapon.transform.GetChild(0).gameObject);
                        GameObject WeaponModel = Instantiate(
                            spawnWeapons.Weapons[refrenceKeeper.weaponInventory[refrenceKeeper.activeSlot].id].WeaponItem.itemModel,
                            switchedWeapon.transform.position,
                            switchedWeapon.transform.rotation);
                        WeaponModel.transform.SetParent(switchedWeapon.transform);
                        WeaponModel.transform.localPosition = new Vector3(0, 0, 0);
                        switchedWeapon.transform.position = switchedWeapon.transform.position + new Vector3(0, 4, 0);
                        WeaponModel.gameObject.layer = 11;
                        switchedWeapon.GetComponent<BulletsLeft>().bullets = shoot.bulletsLeft[refrenceKeeper.activeSlot];
                        for (int z = 0; z < WeaponModel.transform.childCount; z++)
                        {
                            WeaponModel.transform.GetChild(z).gameObject.layer = 11;
                        }
                        switchedWeapon.GetComponent<Pickup>().switchSpawned = true;
                        switchedWeapon.GetComponent<Pickup>().deactivated = false;
                        switchedWeapon.transform.SetParent(Items);
                        localiseTransform.setTransformItem(WeaponModel, refrenceKeeper.weaponInventory[refrenceKeeper.activeSlot].id);*/
                        refrenceKeeper.weaponInventory[refrenceKeeper.activeSlot] = spawnWeapons.Weapons[WeaponIndex].WeaponItem;
                        updateUI.UpdateSlotsUI();
                        updateUI.HighlightSlotOnPickup(refrenceKeeper.activeSlot);
                        GameObject.Find("LocalRelay").GetComponent<SpawnItem>().CmdSpawnDropped(gameObject, Player.position, id, direction, gameObject.GetComponent<BulletsLeft>().bullets);
                    }

                    aimShoot.scale = spawnWeapons.Weapons[WeaponIndex].WeaponItem.scale;
                    aimShoot.position = spawnWeapons.Weapons[WeaponIndex].WeaponItem.position;
                    aimShoot.positionFlipped = spawnWeapons.Weapons[WeaponIndex].WeaponItem.positionFlipped;
                    shoot.fireRate = spawnWeapons.Weapons[WeaponIndex].WeaponItem.fireRate;
                    shoot.recoil = spawnWeapons.Weapons[WeaponIndex].WeaponItem.recoil;
                    shoot.impact = spawnWeapons.Weapons[WeaponIndex].WeaponItem.impact;
                    shoot.bulletsLeft[refrenceKeeper.activeSlot] = gameObject.GetComponent<BulletsLeft>().bullets;
                    //Update Stats
                    refrenceKeeper.inventoryCount++;
                    refrenceKeeper.inventoryCount = Mathf.Clamp(refrenceKeeper.inventoryCount, 0, 4);

                    GameObject.Find("LocalRelay").GetComponent<DestroyGun>().CmdDestroyGun(gameObject, individualCount);
                }
            }
        }
    }
}
