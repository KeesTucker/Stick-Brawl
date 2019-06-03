using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillPlayer : MonoBehaviour {

    public float damage = 100000;
    public bool endKill = false;
    public bool canDamage = true;

    void OnCollisionEnter(Collision collisionInfo)
    {
        if (collisionInfo.gameObject.layer == 24 && !endKill)
        {
            if (collisionInfo.gameObject.tag == "PosRelay")
            {
                if (collisionInfo.gameObject.GetComponent<PlayerControl>() && canDamage)
                {
                    collisionInfo.gameObject.GetComponent<HealthAI>().CmdUpdateHealth(/*SyncData.health / 3f*/0);
                    StartCoroutine("DamageDelay");
                    return;
                }
                else
                {
                    Rigidbody rb;
                    rb = collisionInfo.gameObject.GetComponent<Rigidbody>();

                    float rand = Random.Range(-5000, 5000);
                    for (int i = 0; i < 20; i++)
                    {
                        rb.AddForce(rand, 50000f * Time.deltaTime, 0);
                    }
                }
            }
            else if (collisionInfo.transform.parent.gameObject.tag == "PosRelay")
            {
                if (collisionInfo.transform.parent.gameObject.GetComponent<PlayerControl>() && canDamage)
                {
                    collisionInfo.transform.parent.gameObject.GetComponent<HealthAI>().CmdUpdateHealth(/*SyncData.health / 3f*/0);
                    StartCoroutine("DamageDelay");
                    return;
                }
                else
                {
                    Rigidbody rb;
                    rb = collisionInfo.transform.parent.gameObject.GetComponent<Rigidbody>();

                    float rand = Random.Range(-5000, 5000);
                    for (int i = 0; i < 20; i++)
                    {
                        rb.AddForce(0, 50000f * Time.deltaTime, 0);
                    }
                }
            }
            else if (collisionInfo.transform.parent.parent.gameObject.tag == "PosRelay")
            {
                if (collisionInfo.transform.parent.parent.gameObject.GetComponent<PlayerControl>() && canDamage)
                {
                    collisionInfo.transform.parent.parent.gameObject.GetComponent<HealthAI>().CmdUpdateHealth(/*SyncData.health / 3f*/0);
                    StartCoroutine("DamageDelay");
                    return;
                }
                else
                {
                    Rigidbody rb;
                    rb = collisionInfo.transform.parent.parent.gameObject.GetComponent<Rigidbody>();

                    float rand = Random.Range(-5000, 5000);
                    for (int i = 0; i < 20; i++)
                    {
                        rb.AddForce(0, 50000f * Time.deltaTime, 0);
                    }
                }
            }
            else if (collisionInfo.transform.parent.parent.parent.gameObject.tag == "PosRelay" && collisionInfo.transform.parent.parent.parent.gameObject.GetComponent<PlayerControl>())
            {
                if (collisionInfo.transform.parent.parent.parent.gameObject.GetComponent<PlayerControl>() && canDamage)
                {
                    collisionInfo.transform.parent.parent.parent.gameObject.GetComponent<HealthAI>().CmdUpdateHealth(/*SyncData.health / 3f*/0);
                    StartCoroutine("DamageDelay");
                    return;
                }
                else
                {
                    Rigidbody rb;
                    rb = collisionInfo.transform.parent.parent.parent.gameObject.GetComponent<Rigidbody>();

                    float rand = Random.Range(-5000, 5000);
                    for (int i = 0; i < 20; i++)
                    {
                        rb.AddForce(0, 50000f * Time.deltaTime, 0);
                    }
                }
            }
        }
        else if (collisionInfo.gameObject.layer == 24 && endKill)
        {
            if (collisionInfo.gameObject.tag == "PosRelay")
            {
                collisionInfo.gameObject.GetComponent<HealthAI>().CmdUpdateHealth(/*SyncData.health / 3f*/0);
                StartCoroutine("DamageDelay");
                return;
            }
            else if (collisionInfo.transform.parent.gameObject.tag == "PosRelay")
            {
                collisionInfo.transform.parent.gameObject.GetComponent<HealthAI>().CmdUpdateHealth(/*SyncData.health / 3f*/0);
                StartCoroutine("DamageDelay");
                return;
            }
            else if (collisionInfo.transform.parent.parent.gameObject.tag == "PosRelay")
            {
                collisionInfo.transform.parent.parent.gameObject.GetComponent<HealthAI>().CmdUpdateHealth(/*SyncData.health / 3f*/0);
                StartCoroutine("DamageDelay");
                return;
            }
            else if (collisionInfo.transform.parent.parent.parent.gameObject.tag == "PosRelay")
            {
                collisionInfo.transform.parent.parent.parent.gameObject.GetComponent<HealthAI>().CmdUpdateHealth(/*SyncData.health / 3f*/0);
                StartCoroutine("DamageDelay");
                return;
            }
        }
    }

    IEnumerator DamageDelay()
    {
        canDamage = false;
        yield return new WaitForSeconds(1f);
        canDamage = true;
    }
}
