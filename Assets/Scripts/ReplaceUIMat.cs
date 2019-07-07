using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class ReplaceUIMat : MonoBehaviour
{
    public Material mat;
    // Start is called before the first frame update
    void Start()
    {
        foreach (Image image in FindObjectsOfType<Image>())
        {
            image.material = mat;
        }
    }
}
