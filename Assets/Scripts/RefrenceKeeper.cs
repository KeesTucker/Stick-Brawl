using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RefrenceKeeper : MonoBehaviour {

    public GameObject WeaponParent;

    public List<GameObject> WeaponRefrences = new List<GameObject>();

    public List<float> itemDistanceRefrences = new List<float>();

    public List<Item> weaponInventory = new List<Item>();

    public List<Item> consumableInventory = new List<Item>();

    public int itemCount;

    public int inventoryCount;

    public int activeSlot = 0;

    public UpdateUI updateUI;

    public bool isPlayer = true;

    public SwitchWeapon switchWeapon;

    IEnumerator Start()
    {
        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();

        if (GetComponent<PlayerControl>())
        {
            updateUI.HighlightSlot(activeSlot);
            isPlayer = true;
        }
        else
        {
            isPlayer = false;
        }
        
        switchWeapon = GameObject.Find("Weapon").GetComponent<SwitchWeapon>();
    }

    void Update()
    {
        if (weaponInventory.Count > 0)
        { //If these buttons are smashed multiple guns can be spawned, implement a check to see if child count is greater than three and delete anything higher than that.
            if (Input.GetKeyDown("1"))
            {
                activeSlot = 0;
                activeSlot = Mathf.Clamp(activeSlot, 0, weaponInventory.Count - 1);
                if (isPlayer)
                {
                    updateUI.HighlightSlot(activeSlot);
                }
                switchWeapon.Switch(weaponInventory[activeSlot].id);
            }
            if (Input.GetKeyDown("2"))
            {
                activeSlot = 1;
                activeSlot = Mathf.Clamp(activeSlot, 0, weaponInventory.Count - 1);
                if (isPlayer)
                {
                    updateUI.HighlightSlot(activeSlot);
                }
                switchWeapon.Switch(weaponInventory[activeSlot].id);
            }
            if (Input.GetKeyDown("3"))
            {
                activeSlot = 2;
                activeSlot = Mathf.Clamp(activeSlot, 0, weaponInventory.Count - 1);
                if (isPlayer)
                {
                    updateUI.HighlightSlot(activeSlot);
                }
                switchWeapon.Switch(weaponInventory[activeSlot].id);
            }
            if (Input.GetKeyDown("4"))
            {
                activeSlot = 3;
                activeSlot = Mathf.Clamp(activeSlot, 0, weaponInventory.Count - 1);
                if (isPlayer)
                {
                    updateUI.HighlightSlot(activeSlot);
                }
                switchWeapon.Switch(weaponInventory[activeSlot].id);
            }
        }
    }
}

