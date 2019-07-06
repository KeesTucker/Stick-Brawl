using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class SpawnRocketAI : NetworkBehaviour
{
    public GameObject rocket;

    public GameObject rocketGO;

    public bool spaceDepressed = true;

    public Collider[] ragdollColliders;

    public SpriteRenderer[] ragdollSpriteRenderers;

    public ParticleSystem particle;

    public ParticleSystem.EmissionModule emission;

    public float timeDeath = 20f;

    public bool done = false;

    public bool AISpace = false; //ai turns this on

    public bool camActive;

    public GameObject mainCam;

    public bool destroyed = false;

    public GameObject box;
    public GameObject boxHold;

    public RefrenceKeeperAI refrenceKeeper;

    public bool fall = false;

    public int rocketCount = 0;

    [SyncVar]
    public bool ready;

    public bool doneUpdate = false;

    IEnumerator Start()
    {
        rocketGO = Instantiate(rocket, transform.position, transform.rotation); //Spawn Rocket
        rocketGO.transform.GetChild(1).gameObject.SetActive(false);
        rocketGO.GetComponent<RocketMove>().ragdoll = transform;

        boxHold = Instantiate(box, transform.position, Quaternion.identity);

        
        ragdollColliders = GetComponentsInChildren<Collider>();
        ragdollSpriteRenderers = GetComponentsInChildren<SpriteRenderer>();
        foreach (Collider col in ragdollColliders)
        {
            col.enabled = false;
        }
        foreach (SpriteRenderer sprite in ragdollSpriteRenderers)
        {
            sprite.enabled = false;
        }
        yield return new WaitForSeconds(0.2f);
        if (gameObject.name == "LocalPlayer" && hasAuthority)
        {
            //mainCam = GameObject.Find("Main Camera(Clone)");
            //mainCam.GetComponent<Camera>().orthographicSize = 100;
            camActive = true;
        }
        rocketGO.GetComponent<SpriteRenderer>().color = Color.clear;
        if (!isServer)
        {
            if (refrenceKeeper.updateUI)
            {
                refrenceKeeper.updateUI.fNote.SetActive(false);
            }
        }
        /*if (SyncData.gameMode == 2)
        {
            yield return new WaitForSeconds(10f);
            ready = true;
        }*/

        ready = true;
        StartCoroutine("timerToKill");
        if (refrenceKeeper.updateUI)
        {
            refrenceKeeper.updateUI.fNote.SetActive(false);
        }
    }

    void Update()
    {
        if (isServer && !ready && Input.GetKey("f"))
        {
            ready = true;
            StartCoroutine("timerToKill");
            if (refrenceKeeper.updateUI)
            {
                refrenceKeeper.updateUI.fNote.SetActive(false);
            }
        }
        if (AISpace)
        {
            spaceDepressed = true;
        }
        if (spaceDepressed && !done)
        {
            foreach (Collider col in ragdollColliders)
            {
                col.enabled = true;
            }
            foreach (SpriteRenderer sprite in ragdollSpriteRenderers)
            {
                sprite.enabled = true;
            }
            rocketGO.transform.GetChild(1).gameObject.SetActive(true);
            rocketGO.transform.GetChild(0).gameObject.SetActive(false);
            rocketGO.GetComponent<SpriteRenderer>().enabled = false;
            rocketGO.GetComponent<Collider>().enabled = false;
            transform.position = rocketGO.transform.position;
            StartCoroutine("destroy");
            done = true;
        }
        if (spaceDepressed && rocketGO)
        {
            transform.position = rocketGO.transform.position;
        }
    }

    void FixedUpdate()
    {
        if (spaceDepressed && rocketCount < 40)
        {
            rocketCount++;
            GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
        }
    }

    void LateUpdate()
    {
        /*if (boxHold && rocketGO)
        {
            rocketGO.transform.position = boxHold.transform.position;
            transform.position = boxHold.transform.position;
        }*/
        if (ready && !doneUpdate)
        {
            Destroy(boxHold);
            if (hasAuthority && !fall && GetComponent<PlayerControl>())
            {
                fall = true;
                /*if (SyncData.gameMode == 1)
                {
                    foreach (FallTerrain ft in GameObject.Find("Terrain").transform.GetComponentsInChildren<FallTerrain>())
                    {
                        //ft.StartWrapper();
                    }
                }*/
            }
            doneUpdate = true;
        }
    }

    IEnumerator timerToKill()
    {
        yield return new WaitForEndOfFrame();
        spaceDepressed = true;
        if (isServer)
        {
            RpcDepress();
        }
        else
        {
            CmdDepress();
        }
    }

    [ClientRpc]
    void RpcDepress()
    {
        spaceDepressed = true;
    }

    [Command]
    void CmdDepress()
    {
        spaceDepressed = true;
        RpcDepress();
    }

    IEnumerator destroy()
    {
        if (camActive)
        {
            //float size = 100;
            //for (int i = 0; i < 60; i++)
            //{
             //   mainCam.GetComponent<Camera>().orthographicSize = size;
             //   size--;
             //   yield return new WaitForEndOfFrame();
            //}
        }
        yield return new WaitForSeconds(0.5f);
        destroyed = true;
        Destroy(rocketGO);
    }
}
