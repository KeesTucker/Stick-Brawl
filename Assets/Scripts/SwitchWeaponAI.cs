using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchWeaponAI : MonoBehaviour {

    public SpawnWeapons spawnWeapons;
    public AimShootAI aimShoot;
    public ShootAI shoot;
    public RefrenceKeeperAI refrenceKeeper;
    public GameObject mass;
    public GameObject bulletPositioner;
    public GameObject WeaponHand;
    public GameObject muzzleFlash;
    public GameObject localRelay;
    public GameObject HandHeldWeapon;
    public SyncWeaponAI localWeaponSync;

    // Use this for initialization
    IEnumerator Start()
    {
        yield return new WaitForSeconds(0.3f);
        spawnWeapons = GameObject.Find("Items").GetComponent<SpawnWeapons>();
    }

    public void Setup(GameObject relay)
    {
        localRelay = relay;
        localWeaponSync = localRelay.GetComponent<SyncWeaponAI>();
    }

    public void Switch(int WeaponIndex)
    {
        if (WeaponHand.transform.childCount > 1 && WeaponHand.transform.GetChild(1).name != "Bullet Positioner")
        {
            Destroy(WeaponHand.transform.GetChild(1).gameObject);
        }
        if (WeaponHand.transform.childCount < 3)
        {
            GameObject model;
            if (WeaponIndex == 100)
            {
                model = spawnWeapons.Weapons[spawnWeapons.Weapons.Count - 1].WeaponItem.itemModel;
                WeaponIndex = spawnWeapons.Weapons.Count - 1;
            }
            else
            {
                model = spawnWeapons.Weapons[WeaponIndex].WeaponItem.itemModel;
            }
            shoot.StopReload();
            if (model != null)
            {
                HandHeldWeapon = Instantiate(
                    model,
                    WeaponHand.transform.position,
                    WeaponHand.transform.rotation);
                HandHeldWeapon.transform.SetParent(WeaponHand.transform);
            }

            if ((spawnWeapons.Weapons[WeaponIndex].WeaponItem.name != "Fists") && HandHeldWeapon != null)
            {
                aimShoot.Weapon = HandHeldWeapon.transform;
                shoot.fireRate = spawnWeapons.Weapons[WeaponIndex].WeaponItem.fireRate;
                shoot.recoil = spawnWeapons.Weapons[WeaponIndex].WeaponItem.recoil;
                shoot.impact = spawnWeapons.Weapons[WeaponIndex].WeaponItem.impact;
                shoot.magSize = spawnWeapons.Weapons[WeaponIndex].WeaponItem.magazineSize;
                shoot.bulletPrefab = spawnWeapons.Weapons[WeaponIndex].WeaponItem.bullet;
                shoot.bloom = spawnWeapons.Weapons[WeaponIndex].WeaponItem.bloom;
                shoot.bulletPrefabRB = spawnWeapons.Weapons[WeaponIndex].WeaponItem.bullet.GetComponent<Rigidbody>();
                shoot.rbbullet = spawnWeapons.Weapons[WeaponIndex].WeaponItem.bullet.GetComponent<Rigidbody>();
                shoot.burstSize = spawnWeapons.Weapons[WeaponIndex].WeaponItem.burstSize;
                shoot.burstTime = spawnWeapons.Weapons[WeaponIndex].WeaponItem.burstTime;
                shoot.hasMag = spawnWeapons.Weapons[WeaponIndex].WeaponItem.mag;
                shoot.gunShot = spawnWeapons.Weapons[WeaponIndex].WeaponItem.shot;
                shoot.reload = spawnWeapons.Weapons[WeaponIndex].WeaponItem.reload;
                shoot.burstCount = 0;
                HandHeldWeapon.layer = 10;
                muzzleFlash.transform.localScale = spawnWeapons.Weapons[WeaponIndex].WeaponItem.muzzleFlashScale * 0.1f;
                for (int z = 0; z < HandHeldWeapon.transform.childCount; z++)
                {
                    HandHeldWeapon.transform.GetChild(z).gameObject.layer = 10;
                }
                bulletPositioner.transform.localPosition = spawnWeapons.Weapons[WeaponIndex].WeaponItem.muzzlePosition;
                mass.GetComponent<Rigidbody>().mass = spawnWeapons.Weapons[WeaponIndex].WeaponItem.mass / 3;
            }
        }
    }
}
