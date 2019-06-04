using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour
{
    public AudioClip[] clips;

    private double nextEventTime;
    private int flip = 0;
    public AudioSource audioSource;
    private bool running = false;

    private float oldVolume;

    void Start()
    {
        nextEventTime = AudioSettings.dspTime + 2.0f;
        running = true;
        flip = Random.Range(0, clips.Length);
        DontDestroyOnLoad(gameObject);
    }

    void Update()
    {
        if (!running)
        {
            return;
        }

        if (oldVolume != SyncData.volume)
        {
            audioSource.volume = SyncData.volume;
            oldVolume = SyncData.volume;
        }

        double time = AudioSettings.dspTime;

        if (time + 1.0f > nextEventTime)
        {
            // We are now approx. 1 second before the time at which the sound should play,
            // so we will schedule it now in order for the system to have enough time
            // to prepare the playback at the specified time. This may involve opening
            // buffering a streamed file and should therefore take any worst-case delay into account.
            audioSource.clip = clips[flip];
            audioSource.PlayScheduled(nextEventTime);

            nextEventTime += clips[flip].length;

            // Flip between two audio sources so that the loading process of one does not interfere with the one that's playing out

            int oldflip = flip;
            
            while (flip == oldflip)
            {
                flip = Random.Range(0, clips.Length);
            }
        }
    }
}
