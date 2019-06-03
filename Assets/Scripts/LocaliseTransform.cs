using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocaliseTransform : MonoBehaviour {

    SpawnWeapons spawnWeapons;

    void Start()
    {
        spawnWeapons = GameObject.Find("Items").GetComponent<SpawnWeapons>();
    }

    public void setTransformItem(GameObject Item, int WeaponIndex)
    {
        Item.transform.localPosition = spawnWeapons.Weapons[WeaponIndex].WeaponItem.spawnPosition;
        Item.transform.localEulerAngles = spawnWeapons.Weapons[WeaponIndex].WeaponItem.spawnRotation;
        Item.transform.localScale = spawnWeapons.Weapons[WeaponIndex].WeaponItem.spawnScale;
    }

    public void setTransformHandheld(GameObject Weapon, int WeaponIndex)
    {
        Weapon.transform.localPosition = spawnWeapons.Weapons[WeaponIndex].WeaponItem.position;
        Weapon.transform.localEulerAngles = spawnWeapons.Weapons[WeaponIndex].WeaponItem.rotation;
        Weapon.transform.localScale = spawnWeapons.Weapons[WeaponIndex].WeaponItem.scale;
    }

    public void setTransformHandheldF(GameObject Weapon, int WeaponIndex)
    {
        Weapon.transform.localPosition = spawnWeapons.Weapons[WeaponIndex].WeaponItem.positionFlipped;
        if (spawnWeapons.Weapons[WeaponIndex].WeaponItem.axisToFlip == 3)
        {
            Weapon.transform.localEulerAngles = spawnWeapons.Weapons[WeaponIndex].WeaponItem.rotation + new Vector3(0, 0, 180f) + spawnWeapons.Weapons[WeaponIndex].WeaponItem.rotationFlipped;
        }
        else if (spawnWeapons.Weapons[WeaponIndex].WeaponItem.axisToFlip == 2)
        {
            Weapon.transform.localEulerAngles = spawnWeapons.Weapons[WeaponIndex].WeaponItem.rotation + new Vector3(0, 180f, 0) + spawnWeapons.Weapons[WeaponIndex].WeaponItem.rotationFlipped;
        }
        else if (spawnWeapons.Weapons[WeaponIndex].WeaponItem.axisToFlip == 1)
        {
            Weapon.transform.localEulerAngles = spawnWeapons.Weapons[WeaponIndex].WeaponItem.rotation + new Vector3(180f, 0, 0) + spawnWeapons.Weapons[WeaponIndex].WeaponItem.rotationFlipped;
        }
        Weapon.transform.localScale = spawnWeapons.Weapons[WeaponIndex].WeaponItem.scale;
    }

}
