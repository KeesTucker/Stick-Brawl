using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AITarget : MonoBehaviour {

    public SpawnRocketAI spawnRocket;
    public RefrenceKeeperAI refrenceKeeper;
    public SpawnWeapons spawnWeapons;

    public bool selectedStart = false;

    public Transform closestWeapon;

    public Transform closestPlayer;

    public Transform closestGrapple;

    public GameObject parent;

    public float radius = 30f;

    private float minWeaponDistance;
    private float minPlayerDistance;
    private float minGrappleDistance;

    public bool startCheckW = true;
    public bool startCheckP = true;
    public bool startCheckG = true;

    public List<GameObject> bulletTypes = new List<GameObject>();

    public int targetType;

    public bool isServer;

    public BaseControl baseControl;

    private bool shotty = false;

    public GameObject[] typeGun;

    public Transform weapon;

    private RaycastHit hit;

    public float offset;

    public HealthAI health;

    // Bit shift the index of the layer (8) to get a bit mask
    int layerMask = 1 << 24;

    int layerMaskGround = 1 << 12;

    public int frames = 0;

    int chosenWeapon;

    GameObject[] weapons;

    // Use this for initialization
    void Start () {
        isServer = parent.GetComponent<AISetup>().isServer;
        spawnWeapons = GameObject.Find("Items").GetComponent<SpawnWeapons>();
        // This would cast rays only against colliders in layer 8.
        // But instead we want to collide against everything except layer 8. The ~ operator does this, it inverts a bitmask.
        layerMask = ~layerMask;
        frames = Random.Range(0, 12);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(parent.transform.position, radius);
        if (closestGrapple)
        {
            Gizmos.DrawSphere(closestGrapple.position, 3f);
        }
        if (closestWeapon)
        {
            Gizmos.DrawSphere(closestWeapon.position, 3f);
        }
        if (closestPlayer)
        {
            Gizmos.DrawSphere(closestPlayer.position, 3f);
        }
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 4f);
    }

	// Update is called once per frame
	void Update () {
        frames++;
        if (frames >= 12 && !health.deaded)
        {
            frames = 0;
            if (isServer && spawnRocket.ready)
            {
                if (!spawnRocket.destroyed && !selectedStart)
                {
                    weapons = GameObject.FindGameObjectsWithTag("WeaponItem");
                    if (weapons.Length > 0)
                    {
                        chosenWeapon = Random.Range(0, weapons.Length - 1);
                        targetGO(weapons[chosenWeapon]);
                        selectedStart = true;
                    }
                }
                else if (!spawnRocket.destroyed && selectedStart)
                {
                    targetGO(weapons[chosenWeapon]);
                }
                else
                {
                    startCheckG = true;
                    startCheckP = true;
                    startCheckW = true;

                    foreach (Collider objectCol in Physics.OverlapSphere(parent.transform.position, radius))
                    {
                        if (objectCol.gameObject.GetComponent<AISetup>() && objectCol.gameObject != parent)
                        {
                            if (startCheckP && !objectCol.gameObject.GetComponent<HealthAI>().deaded)
                            {
                                minPlayerDistance = Vector3.Distance(objectCol.transform.position, parent.transform.position);
                                closestPlayer = objectCol.transform;
                                startCheckP = false;
                            }
                            else if (SyncData.gameMode == 2 && !objectCol.gameObject.GetComponent<HealthAI>().deaded && objectCol.gameObject.GetComponent<PlayerControl>())
                            {
                                minPlayerDistance = 0;
                                closestPlayer = objectCol.transform;
                            }
                            else if (Vector3.Distance(objectCol.transform.position, parent.transform.position) < minPlayerDistance && !objectCol.gameObject.GetComponent<HealthAI>().deaded)
                            {
                                minPlayerDistance = Vector3.Distance(objectCol.transform.position, parent.transform.position);
                                closestPlayer = objectCol.transform;
                            }
                        }
                        else if (objectCol.gameObject.layer == 11 && objectCol.transform.name != "Heal")
                        {
                            if (startCheckW)
                            {
                                minWeaponDistance = Vector3.Distance(objectCol.transform.position, parent.transform.position);
                                closestWeapon = objectCol.transform;
                                startCheckW = false;
                            }
                            else if (Vector3.Distance(objectCol.transform.position, parent.transform.position) < minWeaponDistance)
                            {
                                minWeaponDistance = Vector3.Distance(objectCol.transform.position, parent.transform.position);
                                closestWeapon = objectCol.transform;
                            }
                        }
                        else if (objectCol.gameObject.tag == "TerrainPart" || objectCol.gameObject.tag == "NoAttract")
                        {
                            if (startCheckG)
                            {
                                minGrappleDistance = Vector3.Distance(objectCol.transform.position, transform.position);
                                closestGrapple = objectCol.transform;
                                startCheckG = false;
                            }
                            else if (Vector3.Distance(objectCol.transform.position, transform.position) < minGrappleDistance)
                            {
                                minGrappleDistance = Vector3.Distance(objectCol.transform.position, parent.transform.position);
                                closestGrapple = objectCol.transform;
                            }
                        }
                        if (startCheckG)
                        {
                            closestGrapple = null;
                        }
                        else if (startCheckW)
                        {
                            closestWeapon = null;
                        }
                        else if (startCheckP)
                        {
                            closestPlayer = null;
                        }
                    }
                    if (refrenceKeeper.inventoryCount < 3 && closestWeapon)
                    {
                        if (closestWeapon.parent.parent.GetComponent<WeaponIndexHolder>())
                        {
                            if (minWeaponDistance < minPlayerDistance && (!bulletTypes.Contains(spawnWeapons.Weapons[closestWeapon.parent.parent.GetComponent<WeaponIndexHolder>().WeaponIndex].WeaponItem.bullet) || spawnWeapons.Weapons[closestWeapon.parent.parent.GetComponent<WeaponIndexHolder>().WeaponIndex].WeaponItem.special)) //Make sure not picking up same weapontype
                            {
                                if (Random.Range(0, 10) == 1 && closestGrapple)
                                {
                                    if (closestGrapple.name.Contains("Tree"))
                                    {
                                        transform.position = closestGrapple.transform.position + new Vector3(0, 30f, 0);
                                    }
                                    else
                                    {
                                        transform.position = closestGrapple.transform.position;
                                    }
                                    baseControl.rClick = true;
                                    StartCoroutine("StopGrapple");
                                }
                                else
                                {
                                    transform.position = closestWeapon.transform.position;
                                }
                                targetType = 0;
                            }
                            else if (closestPlayer)
                            {
                                if (Random.Range(0, 8) == 1 && closestGrapple)
                                {
                                    if (closestGrapple.name.Contains("Tree"))
                                    {
                                        transform.position = closestGrapple.transform.position + new Vector3(0, 30f, 0);
                                    }
                                    else
                                    {
                                        transform.position = closestGrapple.transform.position;
                                    }
                                    baseControl.rClick = true;
                                    transform.position = closestPlayer.transform.position;
                                    StartCoroutine("StopGrapple");
                                }
                                else
                                {
                                    transform.position = closestPlayer.transform.position;
                                }
                                targetType = 1;
                            }
                        }
                    }
                    else if (closestPlayer)
                    {
                        if (Random.Range(0, 8) == 1 && closestGrapple)
                        {
                            if (closestGrapple.name.Contains("Tree"))
                            {
                                transform.position = closestGrapple.transform.position + new Vector3(0, 30f, 0);
                            }
                            else
                            {
                                transform.position = closestGrapple.transform.position;
                            }
                            baseControl.rClick = true;
                            transform.position = closestPlayer.transform.position;
                            StartCoroutine("StopGrapple");
                        }
                        else
                        {
                            transform.position = closestPlayer.transform.position;
                        }
                        targetType = 1;
                    }
                    else
                    {
                        transform.position = new Vector3(0, transform.position.y, 0);
                    }
                }
            }
            if (targetType == 1)
            {
                if (Random.Range(0, 10) == 3)
                {
                    int rand = Random.Range(1, refrenceKeeper.inventoryCount);
                    if (rand == 1)
                    {
                        baseControl.one = true;
                    }
                    else if (rand == 2)
                    {
                        baseControl.two = true;
                    }
                    else if (rand == 3)
                    {
                        baseControl.three = true;
                    }
                    else if (rand == 4)
                    {
                        baseControl.four = true;
                    }
                }
                /*if (refrenceKeeper.inventoryCount == 1)
                {
                    baseControl.one = true;
                }
                if (minPlayerDistance < 35) //Melee Distance
                {
                    for (int i = 0; i < refrenceKeeper.weaponInventory.Count; i++)
                    {
                        if (refrenceKeeper.weaponInventory[i].bullet)
                        {
                            if (refrenceKeeper.weaponInventory[i].bullet.name == "Slugs" || refrenceKeeper.weaponInventory[i].special) //Switch to shotty
                            {
                                shotty = true;
                                if (refrenceKeeper.activeSlot != i)
                                {
                                    if (i == 0)
                                    {
                                        baseControl.one = true;
                                    }
                                    else if (i == 1)
                                    {
                                        baseControl.two = true;
                                    }
                                    else if (i == 2)
                                    {
                                        baseControl.three = true;
                                    }
                                }
                            }
                            else if (!shotty) //Use punch
                            {
                                baseControl.four = true;
                            }
                        }
                    }
                }
                else if (minPlayerDistance < 60 && bulletTypes.Contains(typeGun[1]))
                {
                    for (int i = 0; i < refrenceKeeper.weaponInventory.Count; i++)
                    {
                        if (refrenceKeeper.weaponInventory[i].bullet)
                        {
                            if (refrenceKeeper.weaponInventory[i].bullet.name == "Small Bullet" || refrenceKeeper.weaponInventory[i].special) //Switch gun to shortrange
                            {
                                if (refrenceKeeper.activeSlot != i)
                                {
                                    if (i == 0)
                                    {
                                        baseControl.one = true;
                                    }
                                    else if (i == 1)
                                    {
                                        baseControl.two = true;
                                    }
                                    else if (i == 2)
                                    {
                                        baseControl.three = true;
                                    }
                                }
                            }
                        }
                    }
                }
                else if (minPlayerDistance < 100 && bulletTypes.Contains(typeGun[2]))
                {
                    for (int i = 0; i < refrenceKeeper.weaponInventory.Count; i++)
                    {
                        if (refrenceKeeper.weaponInventory[i].bullet)
                        {
                            if (refrenceKeeper.weaponInventory[i].bullet.name == "Medium Bullet" || refrenceKeeper.weaponInventory[i].special) //Switch gun to midrange
                            {
                                if (refrenceKeeper.activeSlot != i)
                                {
                                    if (i == 0)
                                    {
                                        baseControl.one = true;
                                    }
                                    else if (i == 1)
                                    {
                                        baseControl.two = true;
                                    }
                                    else if (i == 2)
                                    {
                                        baseControl.three = true;
                                    }
                                }
                            }
                        }
                    }
                }
                else if (minPlayerDistance < 150 && bulletTypes.Contains(typeGun[3]))
                {
                    for (int i = 0; i < refrenceKeeper.weaponInventory.Count; i++)
                    {
                        if (refrenceKeeper.weaponInventory[i].bullet)
                        {
                            if (refrenceKeeper.weaponInventory[i].bullet.name == "Heavy Bullet" || refrenceKeeper.weaponInventory[i].special) //Switch gun to midrange
                            {
                                if (refrenceKeeper.activeSlot != i)
                                {
                                    if (i == 0)
                                    {
                                        baseControl.one = true;
                                    }
                                    else if (i == 1)
                                    {
                                        baseControl.two = true;
                                    }
                                    else if (i == 2)
                                    {
                                        baseControl.three = true;
                                    }
                                }
                            }
                        }
                    }
                }
                else if (minPlayerDistance < 100)
                {
                    if (refrenceKeeper.inventoryCount > 0)
                    {
                        int i = refrenceKeeper.inventoryCount - 1;
                        if (i != refrenceKeeper.activeSlot)
                        {
                            if (i == 0)
                            {
                                baseControl.one = true;
                            }
                            else if (i == 1)
                            {
                                baseControl.two = true;
                            }
                            else if (i == 2)
                            {
                                baseControl.three = true;
                            }
                        }
                    }
                    else if (refrenceKeeper.activeSlot != 3)
                    {
                        baseControl.one = true;
                    }
                }*/
                Debug.DrawRay(weapon.position, -Vector3.Normalize(new Vector3(weapon.position.x - transform.position.x, weapon.position.y - transform.position.y, 0)) * (minPlayerDistance - 5f), Color.red);
                if (minPlayerDistance < 35 && !refrenceKeeper.weaponHeld)
                {
                    baseControl.lClick = false;
                    if (!baseControl.lClick)
                    {
                        StartCoroutine("punchKill");
                    }
                }
                else if (refrenceKeeper.weaponHeld && !Physics.Raycast(weapon.position, -Vector3.Normalize(new Vector3(weapon.position.x - transform.position.x, weapon.position.y - transform.position.y, 0)), out hit, minPlayerDistance - 5f, layerMask))
                {
                    baseControl.lClick = true;
                }
                else
                {
                    baseControl.lClick = false;
                }
            }

            if (minWeaponDistance < 15f && closestWeapon && closestWeapon.name != "Heal")
            {
                if (closestWeapon.GetComponent<WeaponIndexHolder>())
                {
                    bulletTypes.Add(spawnWeapons.Weapons[closestWeapon.GetComponent<WeaponIndexHolder>().WeaponIndex].WeaponItem.bullet);
                    baseControl.e = true;
                }
                else if (closestWeapon.parent.GetComponent<WeaponIndexHolder>())
                {
                    bulletTypes.Add(spawnWeapons.Weapons[closestWeapon.parent.GetComponent<WeaponIndexHolder>().WeaponIndex].WeaponItem.bullet);
                    baseControl.e = true;
                }
                else if (closestWeapon.parent.parent.GetComponent<WeaponIndexHolder>())
                {
                    bulletTypes.Add(spawnWeapons.Weapons[closestWeapon.parent.parent.GetComponent<WeaponIndexHolder>().WeaponIndex].WeaponItem.bullet);
                    baseControl.e = true;
                }
            }

            if (Random.Range(0, 15) == 1)
            {
                baseControl.rClick = true;
                if (targetType == 1 && closestPlayer)
                {
                    transform.position = closestPlayer.transform.position;
                }
                StartCoroutine("StopGrapple");
            }

            //Movement
            if (transform.position.x > parent.transform.position.x)
            {
                offset = (SyncData.health - health.health) / SyncData.health * 150;
            }
            else if (transform.position.x < parent.transform.position.x)
            {
                offset = (-SyncData.health + health.health) / SyncData.health * 150;
            }

            if (targetType == 0 || targetType == 2)
            {
                if (transform.position.x > parent.transform.position.x)
                {
                    baseControl.d = true;
                    baseControl.a = false;
                    Debug.DrawRay(parent.transform.position, Vector3.right * 5f, Color.green);
                    if (Physics.Raycast(parent.transform.position, Vector3.right, out hit, 5f, layerMaskGround) || Physics.Raycast(parent.transform.position, -Vector3.right, out hit, 5f, layerMaskGround))
                    {
                        baseControl.space = true;
                    }
                }
                else if (transform.position.x < parent.transform.position.x)
                {
                    baseControl.a = true;
                    baseControl.d = false;
                    Debug.DrawRay(parent.transform.position, -Vector3.right * 5f, Color.green);
                    if (Physics.Raycast(parent.transform.position, Vector3.right, out hit, 5f, layerMaskGround) || Physics.Raycast(parent.transform.position, -Vector3.right, out hit, 5f, layerMaskGround))
                    {
                        baseControl.space = true;
                    }
                }
            }
            else if (targetType == 1)
            {
                if (Random.Range(0, (int)(1 / Time.deltaTime)) == 1)
                {
                    baseControl.space = true;
                }
                if (transform.position.x - offset > parent.transform.position.x)
                {
                    baseControl.d = true;
                    baseControl.a = false;
                    Debug.DrawRay(parent.transform.position, Vector3.right * 15f, Color.green);
                    if (Physics.Raycast(parent.transform.position, Vector3.right, out hit, 5f, layerMaskGround) || Physics.Raycast(parent.transform.position, -Vector3.right, out hit, 5f, layerMaskGround))
                    {
                        baseControl.space = true;
                    }
                }
                else if (transform.position.x - offset < parent.transform.position.x)
                {
                    baseControl.a = true;
                    baseControl.d = false;
                    Debug.DrawRay(parent.transform.position, -Vector3.right * 15f, Color.green);
                    if (Physics.Raycast(parent.transform.position, -Vector3.right, out hit, 5f, layerMaskGround) || Physics.Raycast(parent.transform.position, Vector3.right, out hit, 5f, layerMaskGround))
                    {
                        baseControl.space = true;
                    }
                }
            }
        }
    }

    public void targetGO(GameObject target)
    {
        if (target)
        {
            transform.position = target.transform.position;
        }
    }

    IEnumerator punchKill()
    {
        baseControl.lClick = true;
        yield return new WaitForSeconds(0.2f);
        baseControl.lClick = false;
    }

    IEnumerator StopGrapple()
    {
        yield return new WaitForSeconds(Random.Range(0.5f, 4f));
        baseControl.rClick = false;
    }
}
