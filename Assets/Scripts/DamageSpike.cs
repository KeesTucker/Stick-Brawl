using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageSpike : MonoBehaviour {

    public float damage;
    public bool damagable = true;
    public bool onServer = false;
    public bool parent = false;
    private GameObject destroyThis;
    public float speed = 5f;
    public Rigidbody weapon;
    public bool isAWeapon = false;
    public GameObject particle;
    public GameObject gunParticle;
    public Rigidbody rb;
    public bool particleDone = false;
    public bool punching = false;
    public float velocityX;
    public float velocityY;
    public bool weaponParticle = false;
    public Color ouchie = new Color(0.8f, 0.05f, 0.05f);
    Color oldColor;
    public GameObject hitMarker;
    GameObject hit;
    public AudioSource audioSource;
    public AudioClip splat;
    public Transform local;

    IEnumerator Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        while (!GameObject.Find("LocalPlayer"))
        {
            yield return null;
        }
        if (GameObject.Find("LocalPlayer"))
        {
            local = GameObject.Find("LocalPlayer").transform;
        }
    }

    IEnumerator OnCollisionEnter(Collision collisionInfo)
    {
        if (damagable)
        {
            if (collisionInfo.gameObject.layer == 24)
            {
                if (collisionInfo.gameObject.tag == "PosRelay")
                {
                    if (collisionInfo.gameObject.GetComponent<AISetup>().hasAuthority)
                    {
                        onServer = true;
                    }
                    else
                    {
                        onServer = false;
                    }
                    if (onServer)
                    {
                        collisionInfo.gameObject.GetComponent<HealthAI>().CmdUpdateHealth(damage);
                    }
                    damagable = false;
                    audioSource.PlayOneShot(splat, SyncData.sfx * 1.3f * (Mathf.Clamp((200 - Vector3.Distance(transform.position, local.position)), 0, 200) / 200));
                    StartCoroutine(SpawnParticle(collisionInfo));
                    yield return new WaitForSeconds(0.5f);
                    damagable = true;
                }
                else if (collisionInfo.transform.parent.gameObject.tag == "PosRelay")
                {
                    if (collisionInfo.transform.parent.gameObject.GetComponent<AISetup>().hasAuthority)
                    {
                        onServer = true;
                    }
                    else
                    {
                        onServer = false;
                    }
                    if (onServer)
                    {
                        collisionInfo.transform.parent.gameObject.GetComponent<HealthAI>().CmdUpdateHealth(damage);
                    }
                    damagable = false;
                    audioSource.PlayOneShot(splat, SyncData.sfx * 1.3f * (Mathf.Clamp((200 - Vector3.Distance(transform.position, local.position)), 0, 200) / 200));
                    StartCoroutine(SpawnParticle(collisionInfo));
                    yield return new WaitForSeconds(0.5f);
                    damagable = true;
                }
                else if (collisionInfo.transform.parent.parent.gameObject.tag == "PosRelay")
                {
                    if (collisionInfo.transform.parent.parent.gameObject.GetComponent<AISetup>().hasAuthority)
                    {
                        onServer = true;
                    }
                    else
                    {
                        onServer = false;
                    }
                    if (onServer)
                    {
                        collisionInfo.transform.parent.parent.gameObject.GetComponent<HealthAI>().CmdUpdateHealth(damage);
                    }
                    damagable = false;
                    audioSource.PlayOneShot(splat, SyncData.sfx * 1.3f * (Mathf.Clamp((200 - Vector3.Distance(transform.position, local.position)), 0, 200) / 200));
                    StartCoroutine(SpawnParticle(collisionInfo));
                    yield return new WaitForSeconds(0.5f);
                    damagable = true;
                }
                else if (collisionInfo.transform.parent.parent.parent.gameObject.tag == "PosRelay")
                {
                    if (collisionInfo.transform.parent.parent.parent.gameObject.GetComponent<AISetup>().hasAuthority)
                    {
                        onServer = true;
                    }
                    else
                    {
                        onServer = false;
                    }
                    if (onServer)
                    {
                        collisionInfo.transform.parent.parent.parent.gameObject.GetComponent<HealthAI>().CmdUpdateHealth(damage);
                    }
                    damagable = false;
                    StartCoroutine(SpawnParticle(collisionInfo));
                    audioSource.PlayOneShot(splat, SyncData.sfx * 1.3f * (Mathf.Clamp((200 - Vector3.Distance(transform.position, local.position)), 0, 200) / 200));
                    yield return new WaitForSeconds(0.5f);
                    damagable = true;
                }
            }
        }
    }

    IEnumerator SpawnParticle(Collision info)
    {
        if (info.gameObject.layer == 24)
        {
            if (particle)
            {
                ParticleSystem.MainModule system = particle.GetComponent<ParticleSystem>().main;
                if (info.gameObject.tag == "PosRelay" || info.gameObject.name == "LimbEnd")
                {
                    system.startColor = info.gameObject.GetComponent<SpriteRenderer>().color;
                    GameObject ouchParticle = Instantiate(particle, info.contacts[0].point, Quaternion.identity);
                    ouchParticle.transform.forward = new Vector3(-info.contacts[0].normal.x, -info.contacts[info.contacts.Length - 1].normal.y, info.contacts[info.contacts.Length - 1].normal.z);

                    //oldColor = info.gameObject.GetComponent<ColouriserAI>().m_NewColor;

                    //info.gameObject.GetComponent<SpriteRenderer>().color = ouchie;

                    yield return new WaitForSeconds(0.2f);
                    //info.gameObject.GetComponent<SpriteRenderer>().color = oldColor;
                    yield return new WaitForEndOfFrame();
                    //info.gameObject.GetComponent<SpriteRenderer>().color = oldColor;
                }
            }
        }
    }

    IEnumerator Destroyer()
    {
        yield return new WaitForSeconds(0.1f);
        Destroy(destroyThis);
    }
}
