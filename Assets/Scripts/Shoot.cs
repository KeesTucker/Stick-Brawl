using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour {

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

    public RefrenceKeeper refrenceKeeper;

    public AimShoot aimShoot;

    public GameObject muzzleFlash;

    public Material muzzleMaterial;

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

    IDKeeper iDKeeper;

    public GameObject localRelay;

    public SyncWeapon localWeaponSync;

    bool changeHandPos;

    float reloadRandomiser;

    int multiplier;

    public RaycastHit hit;

    Vector3 targetReload;

    public GameObject ragdoll;

    public bool missLinks;

    IEnumerator Start()
    {
        for (int i = 0; i < 4; i++)
        {
            bulletsLeft.Add(0);
        }      

        aimShoot = GameObject.Find("Ragdoll").GetComponent<AimShoot>();        
        muzzleMaterial = muzzleFlash.GetComponent<Renderer>().material;
        muzzleMaterial.SetFloat("Vector1_B173D9FB", 0);
        iDKeeper = GameObject.Find("Ragdoll").GetComponent<IDKeeper>();
        yield return new WaitForEndOfFrame();
        refrenceKeeper = GameObject.Find("Local").GetComponent<RefrenceKeeper>();
    }

    public void Setup(GameObject relay)
    {
        localRelay = relay;
        localWeaponSync = localRelay.GetComponent<SyncWeapon>();
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
        if (Input.GetKeyDown("r") && refrenceKeeper.inventoryCount > 0 && magSize > 1 && !reloading)
        {
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
            targetReload = new Vector3(transform.position.x - 3f * multiplier, bulletPosition.position.y - 0.3f + reloadRandomiser, transform.position.z);
            LeftHand.GetComponent<Rigidbody>().AddForce(new Vector3(Mathf.Clamp(targetReload.x - LeftHand.transform.position.x, -1, 1), Mathf.Clamp(targetReload.y - LeftHand.transform.position.y, -1, 1), 0) * Time.deltaTime * 5000);
        }
        if (Input.GetMouseButtonDown(0))
        {
            fireButtonDown = true;
        }
        if (Input.GetMouseButtonUp(0))
        {
            fireButtonDown = false;
        }
        if (fireButtonDown == true && loopDown == true && refrenceKeeper.weaponInventory.Count > 0)
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
            if(magSize > 1)
            {
                if(bulletsLeft[refrenceKeeper.activeSlot] > magSize)
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
        if(refrenceKeeper.weaponInventory[refrenceKeeper.activeSlot].magazineSize > 1)
        {
            reloading = true;
            yield return new WaitForSeconds(0.3f);
            if (reloading)
            {
                if (hasMag)
                {
                    mag = GameObject.Find(("Weapon/" + transform.GetChild(2).gameObject.name) + "/Magazine");
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
                    StartCoroutine(DestroyMag(magazine));
                    yield return new WaitForSeconds(refrenceKeeper.weaponInventory[refrenceKeeper.activeSlot].reloadTime);
                    if (reloading) {
                        if (mag != null)
                        {
                            mag.SetActive(true);
                        }
                        reloading = false;

                        bulletsLeft[refrenceKeeper.activeSlot] = magSize;
                        
                        burstCount = 0;
                    }
                    
                }
                else
                {
                    yield return new WaitForSeconds(refrenceKeeper.weaponInventory[refrenceKeeper.activeSlot].reloadTime);
                    if (reloading)
                    {
                        reloading = false;
                        bulletsLeft[refrenceKeeper.activeSlot] = magSize;
                        burstCount = 0;
                    }
                }
            }
        }
    }

    IEnumerator FireBullet()
    {
        localWeaponSync.CmdFire(refrenceKeeper.weaponInventory[refrenceKeeper.activeSlot].damage);
        for (int x = 0; x < Mathf.Clamp(Random.Range(refrenceKeeper.weaponInventory[refrenceKeeper.activeSlot].bulletSplit / 2, refrenceKeeper.weaponInventory[refrenceKeeper.activeSlot].bulletSplit), 1, 9999); x++)
        {
            GameObject bullet = Instantiate(
                    bulletPrefab,
                    bulletPosition.position,
                    bulletPosition.rotation);
            startAngle = transform.right;
            foreach (GameObject playerPart in iDKeeper.body)
            {
                Physics.IgnoreCollision(bullet.GetComponent<Collider>(), playerPart.GetComponent<Collider>());
            }
            for (int i = 0; i < 20; i++)
            {
                bullet.GetComponent<Rigidbody>().AddForce(new Vector3(startAngle.x + (Random.Range(-bloom, bloom) / 360), startAngle.y + Random.Range(-bloom, bloom), startAngle.z) * (impact / 2) * Time.deltaTime * 100);
            }
            if (missLinks)
            {
                bullet.GetComponent<MissLinks>().Miss(hit.collider.gameObject.GetComponent<BulletAvoidPlat>().chainLinks);
            }
            bullet.GetComponent<DamageDealer>().damage = refrenceKeeper.weaponInventory[refrenceKeeper.activeSlot].damage;
            bullet.GetComponent<DamageDealer>().onServer = localRelay.GetComponent<SyncWeapon>().isServer;
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
            recoilAdder.AddForce(-transform.right * Time.deltaTime * (recoil / 3) * 50 * 2); //add these to the arm, maybe make another child which is connected via a hingejoint;
            if (!upsideDown)
            {
                //recoilAdder.AddForce(transform.up * Time.deltaTime * recoil * 55 * 2);
                GetComponent<Rigidbody>().AddForceAtPosition(transform.up * Time.deltaTime * (recoil / 3) * 55 * 2f, recoilAdder.transform.position);
            }
            else
            {
                //recoilAdder.AddForce(transform.up * Time.deltaTime * recoil * -55 * 2);
                GetComponent<Rigidbody>().AddForceAtPosition(transform.up * Time.deltaTime * (recoil / 3) * -55 * 2f, recoilAdder.transform.position);
            }
        }

        yield return new WaitForSeconds((1 / fireRate));
        loopDown = true;

    }

    IEnumerator MuzzleOff()
    {
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
