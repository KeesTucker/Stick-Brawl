using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorTerrain : MonoBehaviour {

    public Biome biome;
    public int type;
    public int subtype;
    public GameObject tree;
    public bool DragonBones;

    public void color()
    {
        if (type == 0)
        {
            foreach (SpriteRenderer sr in transform.GetComponentsInChildren<SpriteRenderer>())
            {
                if (sr.gameObject.name == "Top")
                {
                    sr.color = biome.grassColor;
                }
                else if (sr.gameObject.name == "Base")
                {
                    sr.color = biome.baseColor;
                }
            }
        }
        else if (type == 1)
        {
            Destroy(transform.GetChild(0).gameObject);
            GameObject plantObject = Instantiate(biome.detail[subtype], transform.position, Quaternion.identity);
            Vector3 scale = plantObject.transform.localScale;
            plantObject.transform.parent = transform;
            plantObject.transform.localScale = scale;
            plantObject.transform.GetChild(0).GetComponent<SpriteRenderer>().color = biome.grassColor;
            //transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = biome.detail[subtype];
        }
        else if (type == 2)
        {
            Destroy(tree);
            GameObject treeN = Instantiate(biome.trees[subtype], new Vector3(0, 0, 0), Quaternion.identity);
            Vector3 scale = treeN.transform.localScale;
            treeN.transform.parent = transform;
            treeN.transform.localScale = scale;
            treeN.transform.position = transform.position;
            tree = treeN;
            
            if (DragonBones)
            {
                //colourise somehow
            }
            else
            {
                foreach (SpriteRenderer spriteRenderer in transform.GetComponentsInChildren<SpriteRenderer>())
                {
                    if (spriteRenderer.transform.parent.gameObject.tag == "TreeTop")
                    {
                        spriteRenderer.color = biome.grassColor;
                    }
                    else
                    {
                        spriteRenderer.color = biome.trunkColor;
                    }
                }
            }
        }
        if (type == 3)
        {
            Destroy(transform.GetChild(0).gameObject);
            GameObject plantObject = Instantiate(biome.detail[subtype], transform.position, Quaternion.identity);
            Vector3 scale = plantObject.transform.localScale;
            plantObject.transform.parent = transform;
            plantObject.transform.localScale = scale;
            plantObject.transform.GetChild(0).GetComponent<SpriteRenderer>().color = biome.rockColor;
            //transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = biome.detail[subtype];
        }
    }
}
