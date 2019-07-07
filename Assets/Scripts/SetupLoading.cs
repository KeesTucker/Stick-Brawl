using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetupLoading : MonoBehaviour
{
    public Collider[] colliders;

    public GameObject playerName;

    public GameObject nameTag;

    public bool preInstantiated;
    // Use this for initialization
    IEnumerator Start()
    {
        nameTag = Instantiate(playerName, transform.position, Quaternion.identity);
        nameTag.GetComponent<SyncName>().parent = gameObject;
        for (int i = 0; i < colliders.Length; i++)
        {
            //if (!isServer && !GetComponent<PlayerControl>())
            //{
            //    colliders[i].gameObject.layer = 14;
            //}
            for (int v = 0; v < colliders.Length; v++)
            {
                Physics.IgnoreCollision(colliders[i], colliders[v]);
            }
        }
        yield return new WaitForEndOfFrame();
        if (preInstantiated)
        {
            if (PlayerPrefs.HasKey(ShopItemType.Skin.ToString() + "selected"))
            {
                GetComponent<SkinApply>().UpdateSkin(PlayerPrefs.GetInt(ShopItemType.Skin.ToString() + "selected"));
            }
            else
            {
                PlayerPrefs.SetInt(ShopItemType.Skin.ToString() + "selected", 8);
                GetComponent<SkinApply>().UpdateSkin(8);
            }
        }
    }
}
