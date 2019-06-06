using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateFPS : MonoBehaviour
{
    public TMPro.TMP_Text text;
    private int counter;
    public float totalFramesTime;

    void Update()
    {
        if (counter > 30)
        {
            counter = 0;
            if (SyncData.targetFPS != -1)
            {
                text.text = "FPS: " + ((int)Mathf.Clamp((1f / (totalFramesTime / 30f)), 0, SyncData.targetFPS - Random.Range(1, 4))).ToString();
            }
            else
            {
                text.text = "FPS: " + ((int)(1f / (totalFramesTime / 30f))).ToString();
            }
            totalFramesTime = 0;
        }
        else
        {
            totalFramesTime += Time.deltaTime;
            counter++;
        }
        
    }
}
