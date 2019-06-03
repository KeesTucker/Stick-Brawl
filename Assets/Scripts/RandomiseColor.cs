using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomiseColor : MonoBehaviour {

    public ColourSetSliders colourSet;

    public Transform r;
    public Transform g;
    public Transform b;

    void OnCollisionEnter(Collision info)
    {
        if (info.gameObject.layer != 24)
        {
            StartCoroutine("ColorChange");
        }
    }

    IEnumerator ColorChange()
    {
        for (int i = 0; i < 10; i++)
        {
            Color color = Random.ColorHSV(0, 1, 0.5f, 1, 0.5f, 1);
            r.localPosition = new Vector3(color.r - 1, r.localPosition.y, r.localPosition.z);
            g.localPosition = new Vector3(color.g - 1, g.localPosition.y, g.localPosition.z);
            b.localPosition = new Vector3(color.b - 1, b.localPosition.y, b.localPosition.z);
            colourSet.UpdateColor();
            yield return new WaitForSeconds(0.1f);
        }
    }

}
