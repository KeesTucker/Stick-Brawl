using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinApply : MonoBehaviour
{
    public SpriteRenderer[] spriteRenderers;

    public Sprite[] sprites;

    public ShopItem[] shopItems;

    public void UpdateSkin()
    {
        for (int i = 0; i < sprites.Length; i++)
        {
            spriteRenderers[i].sprite = sprites[i];
        }
    }

    public void UpdateSkin(int id)
    {
        for (int i = 0; i < spriteRenderers.Length; i++)
        {
            spriteRenderers[i].sprite = shopItems[id].sprites[i];
        }
    }
}
