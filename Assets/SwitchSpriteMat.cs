using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class SwitchSpriteMat : MonoBehaviour
{
    public Material mat;

    IEnumerator Start()
    {
        yield return new WaitForSeconds(5f);
        foreach (SpriteRenderer sprite in transform.GetComponentsInChildren<SpriteRenderer>())
        {
            if (sprite.material != mat)
            {
                Debug.Log(sprite.gameObject.name);
            }
        }
    }
}
