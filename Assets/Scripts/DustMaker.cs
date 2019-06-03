using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DustMaker : MonoBehaviour {

    public GameObject particle;

    public bool foot;
    public AudioSource audioSource;
    public AudioClip footstep;

    public Transform local;

    public HealthAI health;

    IEnumerator Start()
    {
        while (!GameObject.Find("LocalPlayer") && !GameObject.Find("LoadingPlayer"))
        {
            yield return null;
        }
        if (GameObject.Find("LocalPlayer"))
        {
            local = GameObject.Find("LocalPlayer").transform;
        }
        else if (GameObject.Find("LoadingPlayer"))
        {
            local = GameObject.Find("LoadingPlayer").transform;
        }
    }

    void OnCollisionEnter(Collision collsionInfo)
    {
        if (health)
        {
            if (collsionInfo.collider.gameObject.layer == 12 && !health.deaded)
            {
                if (foot)
                {
                    audioSource.PlayOneShot(footstep, SyncData.sfx * 0.3f * (Mathf.Clamp((200f - Vector3.Distance(transform.position, local.position) * 2f), 0, 200) / 200f));
                }
                Instantiate(particle, transform.position, Quaternion.identity);
            }
        }
        else if (local.gameObject.name == "LoadingPlayer")
        {
            if (foot)
            {
                audioSource.PlayOneShot(footstep, SyncData.sfx * 0.3f * (Mathf.Clamp((200f - Vector3.Distance(transform.position, local.position) * 2f), 0, 200) / 200f));
            }
            Instantiate(particle, transform.position, Quaternion.identity);
        }
    }
}
