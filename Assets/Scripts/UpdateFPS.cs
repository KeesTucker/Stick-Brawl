using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateFPS : MonoBehaviour
{
    public TMPro.TMP_Text text;
    private int counter;
    public float totalFramesTime = 0.0f;
    public float fpsTotal;

    void Update()
    {
        /*if (SyncData.targetFPS != -1)
        {
            text.text = "FPS: " + ((int)Mathf.Clamp((1f / (totalFramesTime / 30f)), 0, SyncData.targetFPS - Random.Range(1, 4))).ToString();
        }
        else
        {*/
        totalFramesTime += (Time.unscaledDeltaTime - totalFramesTime) * 0.1f;
        fpsTotal += 1.0f / totalFramesTime;
        counter++;

        if (counter >= 60)
        {
            counter = 0;
            text.text = "FPS: " + ((int)(fpsTotal / 60f)).ToString();
            fpsTotal = 0;
        }
        
        //}
    }
}
