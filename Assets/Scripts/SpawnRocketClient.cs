using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class SpawnRocketClient : NetworkBehaviour
{

    public GameObject rocket;

    public GameObject rocketGO;

    [SyncVar]
    public bool spaceDepressed = false;

    public Collider[] ragdollColliders;

    public SpriteRenderer[] ragdollSpriteRenderers;

    public ParticleSystem particle;

    public ParticleSystem.EmissionModule emission;

    public bool exploded = false;

    IEnumerator Start()
    {
        if (!hasAuthority)
        {
            rocketGO = Instantiate(rocket, transform.position, transform.rotation); //Spawn Rocket
            rocketGO.transform.GetChild(1).gameObject.SetActive(false);
            rocketGO.GetComponent<RocketPoint>().ragdoll = transform.GetChild(1);
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
            rocketGO.GetComponent<SpriteRenderer>().color = transform.parent.gameObject.GetComponent<SyncColour>().m_NewColor;
        }
    }

    void Update()
    {
        if (spaceDepressed && !hasAuthority && !exploded)
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
            rocketGO.GetComponent<Rigidbody>().isKinematic = true;
            rocketGO.GetComponent<Collider>().enabled = false;
            StartCoroutine("destroy");
            exploded = true;
        }
    }

    IEnumerator destroy()
    {
        yield return new WaitForSeconds(0.3f);
        Destroy(rocketGO);
    }

    [Command]
    public void CmdDepressSpace()
    {
        spaceDepressed = true;
        RpcDepressSpace();
    }

    [ClientRpc]
    public void RpcDepressSpace()
    {
        spaceDepressed = true;
    }
}
