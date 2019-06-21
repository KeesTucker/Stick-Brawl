using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class SpawnRocket : NetworkBehaviour {

    public GameObject rocket;

    public GameObject rocketGO;

    public bool spaceDepressed = true;

    public Collider[] ragdollColliders;

    public SpriteRenderer[] ragdollSpriteRenderers;

    public ParticleSystem particle;

    public ParticleSystem.EmissionModule emission;

    public float timeDeath = 10f;

    public GameObject localRelay;

    public bool done = false;

	IEnumerator Start () {
        //GameObject.Find("Local/Main Camera").GetComponent<Camera>().orthographicSize = 100;
        rocketGO = Instantiate(rocket, transform.position, transform.rotation); //Spawn Rocket
        rocketGO.transform.GetChild(1).gameObject.SetActive(false);
        StartCoroutine("timerToKill");
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
        rocketGO.GetComponent<SpriteRenderer>().color = Color.clear;
        localRelay = GameObject.Find("LocalRelay");

        DepressSpace();
    }
	
	void Update () {
        if (Input.GetKey("space"))
        {
            //DepressSpace();
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
            rocketGO.GetComponent<Rigidbody>().isKinematic = false;
            rocketGO.GetComponent<Collider>().enabled = false;
            StartCoroutine("destroy");
            done = true;
        }
	}

    IEnumerator timerToKill()
    {
        yield return new WaitForSeconds(timeDeath);
        DepressSpace();
    }

    IEnumerator destroy()
    {
        //float size = 100;
        //for (int i = 0; i < 60; i++)
        //{
         //   GameObject.Find("Local/Main Camera").GetComponent<Camera>().orthographicSize = size;
         //   size--;
         //   yield return new WaitForEndOfFrame();
        //}
        yield return new WaitForSeconds(0.3f);
        Destroy(rocketGO);
    }

    public void DepressSpace()
    {
        spaceDepressed = true;
        localRelay.GetComponent<SpawnRocketClient>().spaceDepressed = true;
        localRelay.GetComponent<SpawnRocketClient>().CmdDepressSpace();
    }
}
