using System;
using System.Collections;
using System.Collections.Generic;

public class WeaponList : IComparable<WeaponList>{

    public Item WeaponItem { get; set; }
    public int WeaponIndex { get; set; }

    public int CompareTo(WeaponList Weapons)
    {       // A null value means that this object is greater.
        if (Weapons == null)
        {
            return 1;
        }
        else
        {
            return this.WeaponIndex.CompareTo(Weapons.WeaponIndex);
        }
    }

}
