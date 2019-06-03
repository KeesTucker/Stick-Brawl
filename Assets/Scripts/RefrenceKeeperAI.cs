using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class RefrenceKeeperAI : NetworkBehaviour
{
    public GameObject WeaponHand;

    public List<GameObject> WeaponRefrences = new List<GameObject>();

    public List<float> itemDistanceRefrences = new List<float>();

    public List<Item> weaponInventory = new List<Item>();

    public int itemCount;

    public int inventoryCount;

    public int activeSlot = 0;

    public SwitchWeaponAI switchWeapon;

    public UpdateUI updateUI;

    public bool one = true;
    public bool two;
    public bool three;
    public bool four;

    public bool weaponHeld;

    public Item fists;

    private bool updated = false;

    private bool done = false;

    public bool isPlayer = false;

    void Start()
    {
        for (int i = 0; i < 4; i++)
        {
            weaponInventory.Add(fists);
        }
        StartCoroutine("Begin");
    }

    public override void OnStartAuthority()
    {
        if (hasAuthority && GetComponent<PlayerControl>())
        {
            updateUI = GameObject.Find("PlayerUI").GetComponent<UpdateUIRefrence>().updateUI;
            updateUI.refrenceKeeper = this;
            updateUI.OpenClose();
            
            isPlayer = true;
        }
        else
        {
            isPlayer = false;
        }
    }

    IEnumerator Begin()
    {
        yield return new WaitForSeconds(0.5f);
        done = true;
    }

    void Update()
    {
        if (!updated && updateUI)
        {
            updateUI.HighlightSlot(activeSlot);
            updated = true;
        }
        if (weaponInventory.Count > 0 && done)
        { //If these buttons are smashed multiple guns can be spawned, implement a check to see if child count is greater than three and delete anything higher than that.
            if (one)
            {
                activeSlot = 0;
                activeSlot = Mathf.Clamp(activeSlot, 0, 4);
                if (weaponInventory[activeSlot].id == 15)
                {
                    switchWeapon.Switch(15);
                }
                else if (weaponInventory[activeSlot].name != "Fists")
                {
                    switchWeapon.Switch(weaponInventory[activeSlot].id);
                }
                else
                {
                    switchWeapon.Switch(100);
                }
                if (hasAuthority)
                {
                    if (isPlayer)
                    {
                        updateUI.HighlightSlot(activeSlot);
                    }
                }
                one = false;
            }
            if (two)
            {
                activeSlot = 1;
                activeSlot = Mathf.Clamp(activeSlot, 0, 4);
                if (weaponInventory[activeSlot].name != "Fists")
                {
                    switchWeapon.Switch(weaponInventory[activeSlot].id);
                }
                else
                {
                    switchWeapon.Switch(100);
                }
                if (hasAuthority)
                {
                    if (isPlayer)
                    {
                        updateUI.HighlightSlot(activeSlot);
                    }
                }
                two = false;
            }
            if (three)
            {
                activeSlot = 2;
                activeSlot = Mathf.Clamp(activeSlot, 0, 4);
                if (weaponInventory[activeSlot].name != "Fists")
                {
                    switchWeapon.Switch(weaponInventory[activeSlot].id);
                }
                else
                {
                    switchWeapon.Switch(100);
                }
                if (hasAuthority)
                {
                    if (isPlayer)
                    {
                        updateUI.HighlightSlot(activeSlot);
                    }
                }
                three = false;
            }
            if (four)
            {
                activeSlot = 3;
                activeSlot = Mathf.Clamp(activeSlot, 0, 4);
                if (weaponInventory[activeSlot].name != "Fists")
                {
                    switchWeapon.Switch(weaponInventory[activeSlot].id);
                }
                else
                {
                    switchWeapon.Switch(100);
                }
                if (hasAuthority)
                {
                    if (isPlayer)
                    {
                        updateUI.HighlightSlot(activeSlot);
                    }
                }
                four = false;
            }
            if (weaponInventory[activeSlot].name != "Fists")
            {
                weaponHeld = true;
            }
            else
            {
                weaponHeld = false;
            }
        }
    }
}

