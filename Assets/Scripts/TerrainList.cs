using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TerrainList : IComparable<TerrainList>{

    public GameObject TerrainItem { get; set; }
    public int TerrainIndex { get; set; }

    public int CompareTo(TerrainList Terrain)
    {       // A null value means that this object is greater.
        if (Terrain == null)
        {
            return 1;
        }
        else
        {
            return this.TerrainIndex.CompareTo(Terrain.TerrainIndex);
        }
    }

}
