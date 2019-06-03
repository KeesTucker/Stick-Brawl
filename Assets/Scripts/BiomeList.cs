using System;
using System.Collections;
using System.Collections.Generic;

public class BiomeList : IComparable<BiomeList>
{

    public Biome BiomeItem { get; set; }
    public int BiomeIndex { get; set; }
    public float BiomeStart { get; set; }
    public float BiomeEnd { get; set; }

    public int CompareTo(BiomeList Biomes)
    {       // A null value means that this object is greater.
        if (Biomes == null)
        {
            return 1;
        }
        else
        {
            return this.BiomeIndex.CompareTo(Biomes.BiomeIndex);
        }
    }

}
