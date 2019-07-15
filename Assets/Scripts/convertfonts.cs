using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class convertfonts : MonoBehaviour
{
    public TMPro.TMP_FontAsset oldfont1;
    public TMPro.TMP_FontAsset oldfont2;
    public TMPro.TMP_FontAsset newfont1;
    public TMPro.TMP_FontAsset newfont2;

    // Start is called before the first frame update
    void OnEnable()
    {
        foreach (TMPro.TMP_Text item in FindObjectsOfType<TMPro.TMP_Text>())
        {
            if (item.font = oldfont1)
            {
                item.font = newfont1;
            }
            if (item.font = oldfont2)
            {
                item.font = newfont2;
            }
        }
        foreach (TMPro.TextMeshProUGUI item in FindObjectsOfType<TMPro.TextMeshProUGUI>())
        {
            if (item.font = oldfont1)
            {
                item.font = newfont1;
            }
            if (item.font = oldfont2)
            {
                item.font = newfont2;
            }
        }
    }
}
