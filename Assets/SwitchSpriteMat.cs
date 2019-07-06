using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchSpriteMat : MonoBehaviour
{
    public Material mat;

    IEnumerator Start()
    {
        while (!GameObject.Find("LocalPlayer"))
        {
            yield return null;
        }
        yield return new WaitForSeconds(1f);
        foreach (SpriteRenderer sprite in FindObjectsOfType<SpriteRenderer>())
        {
            sprite.material = mat;
        }
        foreach (DragonBones.UnityArmatureComponent anim in FindObjectsOfType<DragonBones.UnityArmatureComponent>())
        {
            anim.animation.Stop();
        }
    }
}
