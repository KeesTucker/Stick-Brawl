using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class ShootAI : MonoBehaviour {

    public bool fireButtonDown;
    public bool loopDown = true;
    public int recoil;

    public Rigidbody bulletPrefabRB;

    public Rigidbody rb;

    public GameObject bulletPrefab;

    public GameObject shellPrefab;

    public GameObject LeftHand;

    public Rigidbody recoilAdder;

    public Vector3 startAngle;

    public float fireRate = 2.1f;

    public bool upsideDown;

    public int impact;
    public Rigidbody rbbullet;

    public Transform bulletPosition;

    public RefrenceKeeperAI refrenceKeeper;

    public AimShootAI aimShoot;

    public GameObject muzzleFlash;

    public Material muzzleMaterial;

    public GroundForceAI groundForce;

    public GameObject mag;

    public int magSize;

    public List<int> bulletsLeft = new List<int>();

    public float bloom;

    public Vector3 bulletDirection;

    public bool reloading;

    public GameObject shell;

    public bool burstOff = false;

    public int burstSize;

    public int burstCount;

    public float burstTime;

    public bool hasMag;

    public Rigidbody[] LimbEnds;
    public DamageDealer[] LimbDamagers;

    public GameObject localRelay;

    public SyncWeaponAI localWeaponSync;

    bool changeHandPos;

    float reloadRandomiser;

    int multiplier;

    public RaycastHit hit;

    Vector3 targetReload;

    public GameObject ragdoll;

    public bool missLinks;

    public bool r = false; //change for reload on AI

    public bool lClick = false; //change for fire on AI

    public GameObject[] playerParts;

    public int nextFist = 0;

    public Transform aim;

    public float punchForce = 10f;

    public Rigidbody body;
    public Rigidbody lowerBody;

    public GameObject punchParticle;

    public bool firstClick;

    public AudioClip gunShot;
    public AudioClip reload;
    public AudioClip punch;
    public AudioClip healSFX;
    AudioSource audioSource;

    public Transform local;

    public Image reloadHUD;
    public Image fireHUD;

    public float fireTime;
    public float reloadTime;

    public float timerReload;
    public float timerFire;

    public bool isPlayer;
    public bool notAI;

    public bool firing;

    public bool healing;

    IEnumerator Start()
    {
        while (!GameObject.Find("LocalPlayer") && !GameObject.Find("LoadingPlayer"))
        {
            yield return null;
        }
        if (GameObject.Find("LocalPlayer"))
        {
            local = GameObject.Find("LocalPlayer").transform;
            if (local.gameObject.GetComponent<AISetup>().hasAuthority && notAI)
            {
                isPlayer = true;
            }
        }
        else if (GameObject.Find("LoadingPlayer"))
        {
            local = GameObject.Find("LoadingPlayer").transform;
        }
        if (GameObject.Find("PlayerUI/HUD/Panel/Reloading"))
        {
            reloadHUD = GameObject.Find("PlayerUI/HUD/Panel/Reloading").GetComponent<Image>();
        }
        if (GameObject.Find("PlayerUI/HUD/Panel/Fire"))
        {
            fireHUD = GameObject.Find("PlayerUI/HUD/Panel/Fire").GetComponent<Image>();
        }
        audioSource = GetComponent<AudioSource>();
        for (int i = 0; i < LimbEnds.Length; i++)
        {
            LimbDamagers[i] = LimbEnds[i].gameObject.GetComponent<DamageDealer>();
        }

        for (int i = 0; i < 4; i++)
        {
            bulletsLeft.Add(0);
        }
        muzzleMaterial = muzzleFlash.GetComponent<Renderer>().material;
        muzzleMaterial.SetFloat("Vector1_B173D9FB", 0);
        yield return new WaitForSeconds(0.5f);
        if (isPlayer)
        {
            reloadHUD.gameObject.SetActive(false);
        }
    }

    public void Setup(GameObject relay)
    {
        localRelay = relay;
        localWeaponSync = localRelay.GetComponent<SyncWeaponAI>();
    }

    void FixedUpdate()
    {
        // Bit shift the index of the layer (8) to get a bit mask
        int layerMask = 1 << 12;
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(ragdoll.transform.position, Vector3.down, out hit, 65, layerMask)) //Makes sure chainlinks dont get destroyed if standing above them
        {
            if (hit.collider.gameObject.tag == "NoAttract")
            {
                missLinks = true;
            }
            else
            {
                missLinks = false;
            }
        }
        else
        {
            missLinks = false;
        }
        if (r && refrenceKeeper.weaponHeld && magSize > 1 && !reloading)
        {
            r = false;
            reloading = true;
            StartCoroutine("Reload");
        }
        if (reloading/* && !((aimShoot.target.rotation.eulerAngles.z < -130 && aimShoot.target.rotation.eulerAngles.z > -180) || (aimShoot.target.rotation.eulerAngles.z > 130 && aimShoot.target.rotation.eulerAngles.z < 180))*/)
        {
            if (!changeHandPos)
            {
                reloadRandomiser = Random.Range(-1f, 1f);
                multiplier = aimShoot.multiplier;
                changeHandPos = true;
                StartCoroutine("ChangeReload");
            }
            if (transform && bulletPosition)
            {
                targetReload = new Vector3(transform.position.x - 3f * multiplier, bulletPosition.position.y - 0.3f + reloadRandomiser, transform.position.z);
            }
            
            LeftHand.GetComponent<Rigidbody>().AddForce(new Vector3(Mathf.Clamp(targetReload.x - LeftHand.transform.position.x, -1, 1), Mathf.Clamp(targetReload.y - LeftHand.transform.position.y, -1, 1), 0) * Time.deltaTime * 5000);
        }
        if (lClick)
        {
            fireButtonDown = true;
        }
        if (!lClick)
        {
            firstClick = true;
            fireButtonDown = false;
        }
        if (refrenceKeeper.weaponInventory.Count > 0)
        {
            if (fireButtonDown == true && loopDown == true && refrenceKeeper.weaponHeld)
            {
                if (bulletsLeft[refrenceKeeper.activeSlot] < 1 && !reloading)
                {
                    StartCoroutine("Reload");
                }
                if (refrenceKeeper.inventoryCount > 0 && !reloading && !burstOff)
                {
                    StartCoroutine(FireBullet());
                    if (burstSize != 0)
                    {
                        burstCount++;
                        if (burstCount >= burstSize)
                        {
                            StartCoroutine("BurstTracker");
                            burstCount = 0;
                        }
                    }

                }
                if (magSize > 1)
                {
                    if (bulletsLeft[refrenceKeeper.activeSlot] > magSize)
                    {
                        bulletsLeft[refrenceKeeper.activeSlot] = magSize;
                    }
                    if (bulletsLeft[refrenceKeeper.activeSlot] <= 0 && !reloading)
                    {
                        reloading = true;
                        StartCoroutine("Reload");

                    }
                }
                if (!reloading)
                {
                    loopDown = false;
                }
            }
            else if (fireButtonDown == true && loopDown == true && !refrenceKeeper.weaponHeld && firstClick && !refrenceKeeper.weaponInventory[refrenceKeeper.activeSlot].heal)
            {
                StartCoroutine("Punch");
                firstClick = false;
            }
            else if (fireButtonDown == true && loopDown == true && !refrenceKeeper.weaponHeld && firstClick && refrenceKeeper.weaponInventory[refrenceKeeper.activeSlot].heal)
            {
                StartCoroutine("Heal");
                firstClick = false;
            }
        }
    }

    void Update()
    {
        if (reloading && hasMag && isPlayer)
        {
            reloadTime += Time.deltaTime;
            //reloadHUD.fillAmount = reloadTime / timerReload;
        }
        if (firing && isPlayer)
        {
            fireTime += Time.deltaTime;
            //fireHUD.fillAmount = fireTime / timerFire;
        }
        if (healing)
        {
            if (!lClick)
            {
                loopDown = true;
                firing = false;
                healing = false;
                fireTime = 0;
            }
        }
    }

    IEnumerator Punch()
    {
        audioSource.PlayOneShot(punch, SyncData.sfx / 50f * 0.1f * (Mathf.Clamp((200 - Vector3.Distance(transform.position, local.position)), 0, 200) / 200));
        int damager = 0;
        loopDown = false;
        if (groundForce.touchingWall || groundForce.touchingObject)
        {
            Vector3 dir = new Vector3(transform.position.x - aim.position.x, transform.position.y - aim.position.y, 0) * Random.Range(0.7f, 2.5f);
            for (int i = 0; i < 10; i++)
            {
                LimbEnds[nextFist].AddForce(dir * -punchForce * Time.deltaTime);
                lowerBody.AddForce(dir * punchForce / 1.5f * Time.deltaTime);
            }
            GameObject dust = Instantiate(punchParticle, LimbEnds[nextFist].transform.position, Quaternion.identity);
            dust.transform.parent = LimbEnds[nextFist].transform;
            LimbDamagers[nextFist].punching = true;
            LimbDamagers[nextFist].particleDone = false;
            LimbDamagers[nextFist].hitable = true;
            if (isPlayer)
            {
                LimbDamagers[nextFist].playerFire = true;
            }
            LimbDamagers[nextFist].hasKilled = false;
            if (isPlayer && refrenceKeeper.hasAuthority)
            {
                LimbDamagers[nextFist].localFired = true;
            }
            
            damager = nextFist;
            if (nextFist == 0)
            {
                nextFist = 1;
            }
            else
            {
                nextFist = 0;
            }
        }
        else
        {
            Vector3 dir = new Vector3(transform.position.x - aim.position.x, transform.position.y - aim.position.y, 0) * Random.Range(0.7f, 2.5f);
            for (int i = 0; i < 10; i++)
            {
                LimbEnds[nextFist + 2].AddForce(dir * -punchForce / 2 * Time.deltaTime);
                lowerBody.AddForce(dir * punchForce / 3 * Time.deltaTime);
            }
            GameObject dust = Instantiate(punchParticle, LimbEnds[nextFist + 2].transform.position, Quaternion.identity);
            dust.transform.parent = LimbEnds[nextFist + 2].transform;
            LimbDamagers[nextFist + 2].punching = true;
            LimbDamagers[nextFist + 2].particleDone = false;
            LimbDamagers[nextFist + 2].hitable = true;
            if (isPlayer)
            {
                LimbDamagers[nextFist + 2].playerFire = true;
            }
            LimbDamagers[nextFist + 2].hasKilled = false;
            if (isPlayer && refrenceKeeper.hasAuthority)
            {
                LimbDamagers[nextFist + 2].localFired = true;
            }
            damager = nextFist + 2;
            if (nextFist == 0)
            {
                nextFist = 1;
            }
            else
            {
                nextFist = 0;
            }
        }
        yield return new WaitForSeconds(0.3f);
        loopDown = true;
        yield return new WaitForSeconds(0.15f);
        LimbDamagers[damager].hitable = false;
        LimbDamagers[damager].punching = false;
    }

    IEnumerator Heal()
    {
        //audioSource.PlayOneShot(healSFX, SyncData.sfx * 0.2f * (Mathf.Clamp((200 - Vector3.Distance(transform.position, local.position)), 0, 200) / 200));
        fireTime = 0;
        timerFire = 1f / fireRate;
        firing = true;
        healing = true;
        loopDown = false;
        yield return new WaitForSeconds(1f / fireRate);
        if (healing)
        {
            ragdoll.GetComponent<HealthAI>().health += Mathf.Clamp(SyncData.health / 3f, 0, SyncData.health);
            if (ragdoll.GetComponent<ItemCheck>())
            {
                ragdoll.GetComponent<ItemCheck>().DestroyHealth();
            }
            
            loopDown = true;
            firing = false;
            healing = false;
        }
    }

    IEnumerator BurstTracker()
    {
        burstOff = true;
        yield return new WaitForSeconds(burstTime);
        burstOff = false;
        loopDown = true;
    }

    IEnumerator ChangeReload()
    {
        yield return new WaitForSeconds(0.1f);
        changeHandPos = false;
    }

    IEnumerator Reload()
    {
        audioSource.PlayOneShot(reload, SyncData.sfx / 50f * 0.4f * (Mathf.Clamp((200 - Vector3.Distance(transform.position, local.position)), 0, 200) / 200));
        reloading = true;
        if (isPlayer)
        {
            reloadHUD.gameObject.SetActive(true);
        }
        reloadTime = 0;
        if (refrenceKeeper.weaponInventory[refrenceKeeper.activeSlot].magazineSize > 1)
        {
            yield return new WaitForSeconds(0.3f);
            if (reloading)
            {
                if (hasMag)
                {
                    //Need a new system for 2d

                    /*mag = GameObject.Find(("Weapon/" + transform.GetChild(2).gameObject.name) + "/Magazine");
                    GameObject magazine = Instantiate(
                        mag,
                        mag.transform.position,
                        Quaternion.Euler(refrenceKeeper.weaponInventory[refrenceKeeper.activeSlot].spawnRotation));
                    mag.SetActive(false);
                    Rigidbody magrb = magazine.AddComponent<Rigidbody>() as Rigidbody;
                    Destroy(magazine.GetComponent<BoxCollider>());
                    magazine.AddComponent<MeshCollider>();
                    magazine.GetComponent<MeshCollider>().convex = true;
                    magazine.layer = 17;

                    for (int i = 0; i < 10; i++)
                    {
                        magrb.AddForce(-transform.up * aimShoot.multiplier * 1000 * Time.deltaTime);
                    }

                    magazine.transform.localScale = refrenceKeeper.weaponInventory[refrenceKeeper.activeSlot].spawnScale;
                    magrb.mass = 0.5f;
                    StartCoroutine(DestroyMag(magazine));*/
                    timerReload = refrenceKeeper.weaponInventory[refrenceKeeper.activeSlot].reloadTime;
                    yield return new WaitForSeconds(refrenceKeeper.weaponInventory[refrenceKeeper.activeSlot].reloadTime);
                    if (reloading)
                    {
                        /*if (mag != null)
                        {
                            mag.SetActive(true);
                        }*/
                        reloading = false;

                        bulletsLeft[refrenceKeeper.activeSlot] = magSize;

                        burstCount = 0;
                    }

                }
                else
                {
                    if (refrenceKeeper.weaponInventory[refrenceKeeper.activeSlot].bullet.name == "Slugs")
                    {
                        timerReload = refrenceKeeper.weaponInventory[refrenceKeeper.activeSlot].reloadTime / (magSize - bulletsLeft[refrenceKeeper.activeSlot]);
                        if (magSize - bulletsLeft[refrenceKeeper.activeSlot] != 0)
                        {
                            yield return new WaitForSeconds(refrenceKeeper.weaponInventory[refrenceKeeper.activeSlot].reloadTime / (magSize - bulletsLeft[refrenceKeeper.activeSlot]));
                        }
                        audioSource.Stop();

                    }
                    else
                    {
                        timerReload = refrenceKeeper.weaponInventory[refrenceKeeper.activeSlot].reloadTime;
                        yield return new WaitForSeconds(refrenceKeeper.weaponInventory[refrenceKeeper.activeSlot].reloadTime);
                    }
                    if (reloading)
                    {
                        reloading = false;
                        bulletsLeft[refrenceKeeper.activeSlot] = magSize;
                        burstCount = 0;
                    }
                }
            }
        }
        if (isPlayer)
        {
            reloadHUD.gameObject.SetActive(false);
        }
    }

    IEnumerator FireBullet()
    {
        fireTime = 0;
        timerFire = 1f / fireRate;
        firing = true;
        audioSource.PlayOneShot(gunShot, SyncData.sfx / 50f * 0.3f * (Mathf.Clamp((200 - Vector3.Distance(transform.position, local.position)), 0, 200) / 200));
        for (int x = 0; x < Mathf.Clamp(Random.Range(refrenceKeeper.weaponInventory[refrenceKeeper.activeSlot].bulletSplit / 2, refrenceKeeper.weaponInventory[refrenceKeeper.activeSlot].bulletSplit), 1, 9999); x++)
        {
            GameObject bullet = Instantiate(
                    bulletPrefab,
                    bulletPosition.position,
                    bulletPosition.rotation);
            startAngle = transform.right;
            foreach (GameObject playerPart in playerParts)
            {
                Physics.IgnoreCollision(bullet.GetComponent<Collider>(), playerPart.GetComponent<Collider>());
            }
            for (int i = 0; i < 20; i++)
            {
                bullet.GetComponent<Rigidbody>().AddForce(new Vector3(startAngle.x + (Random.Range(-bloom, bloom) / 360), startAngle.y + Random.Range(-bloom, bloom), startAngle.z) * (impact / 3) * Time.deltaTime * 100);
            }
            if (missLinks && hit.collider.gameObject.GetComponent<BulletAvoidPlat>() && bullet.GetComponent<MissLinks>())
            {
                if (hit.collider.gameObject.GetComponent<BulletAvoidPlat>().chainLinks != null)
                {
                    bullet.GetComponent<MissLinks>().Miss(hit.collider.gameObject.GetComponent<BulletAvoidPlat>().chainLinks);
                }
                
            }
            bullet.GetComponent<DamageDealer>().damage = refrenceKeeper.weaponInventory[refrenceKeeper.activeSlot].damage;
            bullet.GetComponent<DamageDealer>().hitable = true;
            if (isPlayer)
            {
                bullet.GetComponent<DamageDealer>().playerFire = true;
            }
            if (refrenceKeeper.hasAuthority && isPlayer)
            {
                bullet.GetComponent<DamageDealer>().localFired = true;
            }
            
            Destroy(bullet, 5.0f);
        }
        shell = Instantiate(
                refrenceKeeper.weaponInventory[refrenceKeeper.activeSlot].shell,
                new Vector3(transform.position.x, bulletPosition.position.y - 0.3f, transform.position.z),
                transform.rotation);
        for (int i = 0; i < 10; i++)
        {
            shell.GetComponent<Rigidbody>().AddForce(transform.up * Random.Range(0.3f, 1.2f) * aimShoot.multiplier * 3000 * Time.deltaTime);
            shell.GetComponent<Rigidbody>().AddForce(transform.right * Random.Range(0.3f, 1.2f) * -1 * aimShoot.multiplier * 8000 * Time.deltaTime);
            shell.GetComponent<Rigidbody>().AddForce(transform.right * Random.Range(0.3f, 1.2f) * 1 * aimShoot.multiplier * 8000 * Time.deltaTime);
        }
        if (magSize > 1)
        {
            bulletsLeft[refrenceKeeper.activeSlot]--;
        }
        muzzleMaterial.SetFloat("Vector1_B173D9FB", 1f);
        StartCoroutine("MuzzleOff");
        for (int i = 0; i < 25; i++) // normal values are i < 50 and no * 2 on the end make vairables for these
        {
            recoilAdder.AddForce(-transform.right * Time.deltaTime * (recoil / 3) * 50 * 1.5f); //add these to the arm, maybe make another child which is connected via a hingejoint;
            if (!upsideDown)
            {
                //recoilAdder.AddForce(transform.up * Time.deltaTime * recoil * 55 * 2);
                GetComponent<Rigidbody>().AddForceAtPosition(transform.up * Time.deltaTime * (recoil / 3) * 55 * 1.5f, recoilAdder.transform.position);
            }
            else
            {
                //recoilAdder.AddForce(transform.up * Time.deltaTime * recoil * -55 * 2);
                GetComponent<Rigidbody>().AddForceAtPosition(transform.up * Time.deltaTime * (recoil / 3) * -55 * 1.5f, recoilAdder.transform.position);
            }
        }

        yield return new WaitForSeconds((1 / fireRate));
        loopDown = true;
        firing = false;
    }

    IEnumerator MuzzleOff()
    {
        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();

        muzzleMaterial.SetFloat("Vector1_B173D9FB", 0);
    }

    IEnumerator DestroyMag(GameObject magObject)
    {
        yield return new WaitForSeconds(10f);
        Destroy(magObject);
    }

    public void StopReload()
    {
        reloading = false;
    }
}