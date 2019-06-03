using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColourSetSliders : MonoBehaviour {

    public Transform r;
    public Transform g;
    public Transform b;

    public ColourSetterLoad colourSetter;

    public TMPro.TMP_Text nameTag;

    IEnumerator Start()
    {
        yield return new WaitForSeconds(0.2f);
        nameTag = GameObject.Find("Nametag(Clone)").transform.GetChild(0).gameObject.GetComponent<TMPro.TMP_Text>();
        if (PlayerPrefs.HasKey("r") && PlayerPrefs.HasKey("g") && PlayerPrefs.HasKey("b"))
        {
            r.localPosition = new Vector3(PlayerPrefs.GetFloat("r") - 1, r.localPosition.y, r.localPosition.z);
            g.localPosition = new Vector3(PlayerPrefs.GetFloat("g") - 1, g.localPosition.y, g.localPosition.z);
            b.localPosition = new Vector3(PlayerPrefs.GetFloat("b") - 1, b.localPosition.y, b.localPosition.z);
        }
        UpdateColor();
    }

    public void UpdateColor()
    {
        Color color = new Color(1 + r.localPosition.x, 1 + g.localPosition.x, 1 + b.localPosition.x);
        colourSetter.SetColor(color);
        SyncData.color = color;
        nameTag.color = color;
        PlayerPrefs.SetFloat("r", color.r);
        PlayerPrefs.SetFloat("g", color.g);
        PlayerPrefs.SetFloat("b", color.b);
        PlayerPrefs.Save();
    }
}
