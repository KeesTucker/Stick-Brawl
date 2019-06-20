using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;

public class HealthAI : NetworkBehaviour {
    [SyncVar]
    public float health = 200;

    public GameObject weapon;

    public GameObject weaponItem;

    public RefrenceKeeperAI refrenceKeeper;

    public bool deaded;

    public bool isPlayer;

    public GameObject Ghost;

    public GameObject deathExplode;

    public AudioSource audioSource;
    public AudioClip explosion;
    public Transform local;

    public Image healthHUDR;
    public Image healthHUDL;

    public float oldHealth;
	
	IEnumerator Start()
    {
        refrenceKeeper = GetComponent<RefrenceKeeperAI>();
        if (GetComponent<AISetup>().isServer)
        {
            health = SyncData.health;
            oldHealth = health;
        }
        else
        {
            health = GameObject.Find("PlayerLocal(Clone)").GetComponent<HealthAI>().health;
            oldHealth = health;
        }
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
        healthHUDR = GameObject.Find("PlayerUI/HUD/Panel/HealthR").GetComponent<Image>();
        healthHUDL = GameObject.Find("PlayerUI/HUD/Panel/HealthL").GetComponent<Image>();
    }

    void Update()
    {
        if (health <= 0 && !deaded)
        {
            StartCoroutine("DestroyPlayer");
            deaded = true;
        }
        if (health != oldHealth && isPlayer && hasAuthority)
        {
            healthHUDR.fillAmount = (health / SyncData.health);
            healthHUDL.fillAmount = (health / SyncData.health);
            oldHealth = health;
        }
    }

    public IEnumerator DestroyPlayer()
    {
        audioSource.PlayOneShot(explosion, SyncData.sfx * 0.6f * (Mathf.Clamp((200f - Vector3.Distance(transform.position, local.position) * 2f), 0, 200) / 200f));
        GetComponent<GroundForceAI>().dead = true;
        ParticleSystem.MainModule system = deathExplode.GetComponent<ParticleSystem>().main;
        system.startColor = GetComponent<ColourSetterAI>().m_NewColor;
        deathExplode.SetActive(true);
        GetComponent<SpriteRenderer>().color = Color.red;
        GetComponent<ColouriserAI>().m_NewColor = Color.red;
        foreach (SpriteRenderer sr in transform.GetComponentsInChildren<SpriteRenderer>())
        {
            sr.color = Color.red;
        }
        foreach (ColouriserAI c in transform.GetComponentsInChildren<ColouriserAI>())
        {
            c.m_NewColor = Color.red;
        }
        GetComponent<AimShootAI>().RClick = false;
        if (GetComponent<PlayerControl>())
        {
            GetComponent<PlayerControl>().a = false;
            GetComponent<PlayerControl>().s = false;
            GetComponent<PlayerControl>().d = false;
        }
        else if (GetComponent<BaseControl>())
        {
            GetComponent<BaseControl>().a = false;
            GetComponent<BaseControl>().s = false;
            GetComponent<BaseControl>().d = false;
        }
        if (hasAuthority && GetComponent<PlayerControl>() && !refrenceKeeper.updateUI.won.activeInHierarchy)
        {
            refrenceKeeper.updateUI.deadMessage.SetActive(true);
            if (SyncData.gameMode == 1)
            {
                refrenceKeeper.updateUI.deadPanel.SetActive(true);
                yield return new WaitForSeconds(3f);
                refrenceKeeper.updateUI.deadPanel.SetActive(false);
            }
            
        }
        transform.Find("Physics AnimatorAI").GetComponent<PlayerMovementAI>().enabled = false;
        if (hasAuthority)
        {
            //GetComponent<SpawnItem>().CmdSpawnKilled(weaponItem, transform.position, 15, 0, 1);
        }
        for (int i = 0; i < refrenceKeeper.weaponInventory.Count; i++)
        {
            int id = refrenceKeeper.weaponInventory[i].id;
            if (hasAuthority)
            {
                GetComponent<SpawnItem>().CmdSpawnKilled(weaponItem, transform.position, id, 0, refrenceKeeper.weaponInventory[i].currentBullets);
            }
        }
        refrenceKeeper.weaponInventory.Clear();
        refrenceKeeper.inventoryCount = 0;
        refrenceKeeper.itemCount = 0;
        for (int i = 0; i < weapon.transform.childCount; i++)
        {
            Destroy(weapon.transform.GetChild(i).gameObject);
        }

        foreach (HingeJoint hj in GetComponentsInChildren<HingeJoint>())
        {
            hj.useSpring = false;
        }
        if (isPlayer)
        {
            CmdSpawnGhost();
        }
    }

    [Command]
    public void CmdSpawnGhost()
    {
        GameObject currentSpawn = Instantiate(Ghost, transform.position, Quaternion.identity);
        currentSpawn.GetComponent<GhostMovement>().parent = gameObject;
        NetworkServer.SpawnWithClientAuthority(currentSpawn, GetComponent<AISetup>().parent);
    }

    [ClientRpc]
    public void RpcDestroyPlayer()
    {
        audioSource.PlayOneShot(explosion, SyncData.sfx * 0.6f * (Mathf.Clamp((200f - Vector3.Distance(transform.position, local.position) * 2f), 0, 200) / 200f));
        deaded = true;
        GetComponent<GroundForceAI>().dead = true;
        ParticleSystem.MainModule system = deathExplode.GetComponent<ParticleSystem>().main;
        system.startColor = GetComponent<ColourSetterAI>().m_NewColor;
        deathExplode.SetActive(true);
        GetComponent<SpriteRenderer>().color = Color.red;
        GetComponent<ColouriserAI>().m_NewColor = Color.red;
        foreach (SpriteRenderer sr in transform.GetComponentsInChildren<SpriteRenderer>())
        {
            sr.color = Color.red;
        }
        foreach (ColouriserAI c in transform.GetComponentsInChildren<ColouriserAI>())
        {
            c.m_NewColor = Color.red;
        }

        if (!hasAuthority)
        {
            GetComponent<AimShootAI>().RClick = false;
            if (GetComponent<PlayerControl>())
            {
                GetComponent<PlayerControl>().a = false;
                GetComponent<PlayerControl>().s = false;
                GetComponent<PlayerControl>().d = false;
            }
            else if (GetComponent<BaseControl>())
            {
                GetComponent<BaseControl>().a = false;
                GetComponent<BaseControl>().s = false;
                GetComponent<BaseControl>().d = false;
            }
            for (int i = 0; i < refrenceKeeper.weaponInventory.Count; i++)
            {
                int id = refrenceKeeper.weaponInventory[i].id;
                if (hasAuthority)
                {
                    GetComponent<SpawnItem>().CmdSpawnKilled(weaponItem, transform.position, id, 0, refrenceKeeper.weaponInventory[i].currentBullets);
                }
            }
            refrenceKeeper.weaponInventory.Clear();
            refrenceKeeper.inventoryCount = 0;
            refrenceKeeper.itemCount = 0;
            for (int i = 0; i < weapon.transform.childCount; i++)
            {
                Destroy(weapon.transform.GetChild(i).gameObject);
            }
            foreach (HingeJoint hj in GetComponentsInChildren<HingeJoint>())
            {
                hj.useSpring = false;
            }
        }
    }

    [Command]
    public void CmdDestroyPlayer()
    {
        audioSource.PlayOneShot(explosion, SyncData.sfx * 0.6f * (Mathf.Clamp((200f - Vector3.Distance(transform.position, local.position) * 2f), 0, 200) / 200f));
        deaded = true;
        GetComponent<GroundForceAI>().dead = true;
        ParticleSystem.MainModule system = deathExplode.GetComponent<ParticleSystem>().main;
        system.startColor = GetComponent<ColourSetterAI>().m_NewColor;
        deathExplode.SetActive(true);
        GetComponent<SpriteRenderer>().color = Color.red;
        GetComponent<ColouriserAI>().m_NewColor = Color.red;
        foreach (SpriteRenderer sr in transform.GetComponentsInChildren<SpriteRenderer>())
        {
            sr.color = Color.red;
        }
        foreach (ColouriserAI c in transform.GetComponentsInChildren<ColouriserAI>())
        {
            c.m_NewColor = Color.red;
        }
        deathExplode.SetActive(true);
        if (!hasAuthority)
        {
            GetComponent<AimShootAI>().RClick = false;
            if (GetComponent<PlayerControl>())
            {
                GetComponent<PlayerControl>().a = false;
                GetComponent<PlayerControl>().s = false;
                GetComponent<PlayerControl>().d = false;
            }
            else if (GetComponent<BaseControl>())
            {
                GetComponent<BaseControl>().a = false;
                GetComponent<BaseControl>().s = false;
                GetComponent<BaseControl>().d = false;
            }
            if (hasAuthority)
            {
                //GetComponent<SpawnItem>().CmdSpawnKilled(weaponItem, transform.position, 15, 0, 1);
            }
            
            for (int i = 0; i < refrenceKeeper.weaponInventory.Count; i++)
            {
                int id = refrenceKeeper.weaponInventory[i].id;
                if (hasAuthority)
                {
                    GetComponent<SpawnItem>().CmdSpawnKilled(weaponItem, transform.position, id, 0, refrenceKeeper.weaponInventory[i].currentBullets);
                }
            }
            refrenceKeeper.weaponInventory.Clear();
            refrenceKeeper.inventoryCount = 0;
            refrenceKeeper.itemCount = 0;
            for (int i = 0; i < weapon.transform.childCount; i++)
            {
                Destroy(weapon.transform.GetChild(i).gameObject);
            }
            foreach (HingeJoint hj in GetComponentsInChildren<HingeJoint>())
            {
                hj.useSpring = false;
            }
            RpcDestroyPlayer();
        }
    }

    [Command]
    public void CmdUpdateHealth(float damage)
    {
        health -= damage;
    }
}
