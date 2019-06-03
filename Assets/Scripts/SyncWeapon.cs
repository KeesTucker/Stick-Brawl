using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class SyncWeapon : NetworkBehaviour {

    public GameObject WeaponHolder;

    public SpawnWeapons spawnWeapons;

    public GameObject mass;

    public GameObject bulletPositioner;

    public GameObject weapon;

    public GameObject grapplePrefab;

    public Vector3 startAngle;

    public Material muzzleMaterial;

    public Transform rotationManager;

    public Rigidbody recoilAdder;

    public LocaliseTransform localiseTransform;

    [SerializeField]
    public GameObject muzzleFlash;

    [SyncVar]
    public int WeaponIndex;

    public float bloom;

    public float impact;

    public float recoil;

    IDKeeper iDKeeper;

    public int multiplier;

    public GameObject grapple;

    RefrenceKeeper refrenceKeeper;

    public GameObject LHT;

    public GameObject HandHeldWeapon;

    HingeJoint hinge;

    JointSpring hingeSpring;

    public GameObject AimSync;
    private float angleGun;

    // Use this for initialization
    void Start () {
        muzzleMaterial = muzzleFlash.GetComponent<Renderer>().material;
        muzzleMaterial.SetFloat("Vector1_B173D9FB", 0);
        refrenceKeeper = GameObject.Find("Local").GetComponent<RefrenceKeeper>();
        localiseTransform = GameObject.Find("Items").GetComponent<LocaliseTransform>();
        spawnWeapons = GameObject.Find("Items").GetComponent<SpawnWeapons>();
        iDKeeper = transform.Find("RagdollPlaceholder").gameObject.GetComponent<IDKeeper>();
        //AimSync = transform.Find("AimSync").gameObject;
        hinge = weapon.GetComponent<HingeJoint>();
        hingeSpring = hinge.spring;
    }
    /*if (transform.parent.parent.parent.parent.parent.gameObject.name == "LocalRelay")
    {

    }*/
    [Command]
    public void CmdGrappleKill()
    {
        RpcGrappleKill();
    }

    [ClientRpc]
    public void RpcGrappleKill()
    {
        Destroy(grapple);
    }

    [Command]
    public void CmdGrappleFire(Vector3 dir, Color m_Color, GameObject parent)
    {
        RpcGrappleFire(dir, m_Color, parent);
    }

    [ClientRpc]
    public void RpcGrappleFire(Vector3 dir, Color m_Color, GameObject parent)
    {
        if (gameObject.name != "LocalRelay")
        {
            grapple = Instantiate(
                grapplePrefab,
                WeaponHolder.transform.position,
                WeaponHolder.transform.rotation);
            grapple.GetComponent<renderGrapple>().LHT = LHT;
            grapple.GetComponent<grappleActivator>().hand = LHT.transform.parent.gameObject;
            grapple.GetComponent<renderGrapple>().Setup(m_Color);
            grapple.GetComponent<grappleActivator>().Setup(m_Color);
            startAngle = dir;
            foreach (GameObject playerPart in iDKeeper.body)
            {
                Physics.IgnoreCollision(grapple.GetComponent<Collider>(), playerPart.GetComponent<Collider>());
            }
            for (int i = 0; i < 20; i++)
            {
                grapple.GetComponent<Rigidbody>().AddForce(startAngle * 600 * Time.deltaTime * 100);
            }
        }
    }

    [Command]
	public void CmdSwitchWeaponIndex(int WI)
    {
        WeaponIndex = WI;
        RpcSwitchWeapon(WI);
    }

    [ClientRpc]
    public void RpcSwitchWeapon(int WI)
    {
        if (gameObject.name != "LocalRelay")
        {
            //Change Weapon
            if (weapon.transform.childCount > 2)
            {
                Destroy(weapon.transform.GetChild(2).gameObject);
            }
            HandHeldWeapon = Instantiate(
                spawnWeapons.Weapons[WI].WeaponItem.itemModel,
                weapon.transform.position,
                weapon.transform.rotation);
            HandHeldWeapon.transform.SetParent(weapon.transform);
            bulletPositioner.transform.localPosition = spawnWeapons.Weapons[WeaponIndex].WeaponItem.muzzlePosition;
            HandHeldWeapon.layer = 10;
            muzzleFlash.transform.localScale = spawnWeapons.Weapons[WeaponIndex].WeaponItem.muzzleFlashScale;
            for (int z = 0; z < HandHeldWeapon.transform.childCount; z++)
            {
                HandHeldWeapon.transform.GetChild(z).gameObject.layer = 10;
            }
            //bulletPositioner.transform.localPosition = spawnWeapons.Weapons[WeaponIndex].WeaponItem.muzzlePosition;
            mass.GetComponent<Rigidbody>().mass = spawnWeapons.Weapons[WeaponIndex].WeaponItem.mass / 30;
            bloom = spawnWeapons.Weapons[WeaponIndex].WeaponItem.bloom;
            impact = spawnWeapons.Weapons[WeaponIndex].WeaponItem.impact;
            recoil = spawnWeapons.Weapons[WeaponIndex].WeaponItem.recoil;
        }
    }

    [Command]
    public void CmdFire(float damage)
    {
        RpcFire(damage);
    }

    [ClientRpc]
    public void RpcFire(float damage)
    {
        if (gameObject.name != "LocalRelay")
        {
            for (int x = 0; x < Mathf.Clamp(Random.Range(spawnWeapons.Weapons[WeaponIndex].WeaponItem.bulletSplit / 2, spawnWeapons.Weapons[WeaponIndex].WeaponItem.bulletSplit), 1, 9999); x++)
            {
                GameObject bullet = Instantiate(
                        spawnWeapons.Weapons[WeaponIndex].WeaponItem.bullet,
                        new Vector3(bulletPositioner.transform.position.x, bulletPositioner.transform.position.y, 0.1f),
                        bulletPositioner.transform.rotation);
                startAngle = weapon.transform.right;
                foreach (GameObject playerPart in iDKeeper.body)
                {
                    Physics.IgnoreCollision(bullet.GetComponent<Collider>(), playerPart.GetComponent<Collider>());
                }
                for (int i = 0; i < 20; i++)
                {
                    bullet.GetComponent<Rigidbody>().AddForce(new Vector3(startAngle.x + (Random.Range(-bloom, bloom) / 360), startAngle.y + Random.Range(-bloom, bloom), startAngle.z) * (impact / 3) * Time.deltaTime * 100);
                }

                bullet.GetComponent<DamageDealer>().damage = damage;
                bullet.GetComponent<DamageDealer>().onServer = isServer;
                Destroy(bullet, 5.0f);
            }
            GameObject shell = Instantiate(
                    spawnWeapons.Weapons[WeaponIndex].WeaponItem.shell,
                    new Vector3(weapon.transform.position.x, bulletPositioner.transform.position.y - 0.3f, weapon.transform.position.z),
                    weapon.transform.rotation);
            for (int i = 0; i < 10; i++)
            {
                shell.GetComponent<Rigidbody>().AddForce(weapon.transform.up * Random.Range(0.3f, 1.2f) * multiplier * 3000 * Time.deltaTime);
                shell.GetComponent<Rigidbody>().AddForce(weapon.transform.right * Random.Range(0.3f, 1.2f) * -1 * multiplier * 8000 * Time.deltaTime);
                shell.GetComponent<Rigidbody>().AddForce(weapon.transform.right * Random.Range(0.3f, 1.2f) * 1 * multiplier * 8000 * Time.deltaTime);
            }
            muzzleMaterial.SetFloat("Vector1_B173D9FB", 1f);
            StartCoroutine("MuzzleOff");
            for (int i = 0; i < 25; i++) // normal values are i < 50 and no * 2 on the end make vairables for these
            {
                recoilAdder.AddForce(-weapon.transform.right * Time.deltaTime * (recoil / 3) * 50 * 2); //add these to the arm, maybe make another child which is connected via a hingejoint;
                if (multiplier == 1)
                {
                    //recoilAdder.AddForce(weapon.transform.up * Time.deltaTime * recoil * 55 * 2);
                    weapon.GetComponent<Rigidbody>().AddForceAtPosition(weapon.transform.up * Time.deltaTime * (recoil / 3) * 55 * 2f, recoilAdder.transform.position);
                }
                else
                {
                    //recoilAdder.AddForce(weapon.transform.up * Time.deltaTime * recoil * -55 * 2);
                    weapon.GetComponent<Rigidbody>().AddForceAtPosition(weapon.transform.up * Time.deltaTime * (recoil / 3) * -55 * 2f, recoilAdder.transform.position);
                }
            }
        } 
    }

    IEnumerator MuzzleOff()
    {
        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();
        muzzleMaterial.SetFloat("Vector1_B173D9FB", 0);
    }

    void FixedUpdate()
    {
        if (!isLocalPlayer)
        {
            angleGun = AngleBetweenPoints(weapon.transform.parent.position, AimSync.transform.position);
            if (weapon.transform.childCount > 2)
            {

                //this bunch of crap converts the ugly value of the rotation of the gun + the aim rotation to a nice value in between -180 and 180 so it can be applied just basically get modulus and rectifys it, then applies it.
                if ((angleGun - weapon.transform.parent.rotation.eulerAngles.z + 180f) % 360 < -180)
                {
                    hingeSpring.targetPosition = ((angleGun - weapon.transform.parent.rotation.eulerAngles.z + 180f) % 360) + 360;
                }
                else if ((angleGun - weapon.transform.parent.rotation.eulerAngles.z + 180f) % 360 > 180)
                {
                    hingeSpring.targetPosition = ((angleGun - weapon.transform.parent.rotation.eulerAngles.z + 180f) % 360) - 360;
                }
                else
                {
                    hingeSpring.targetPosition = (angleGun - weapon.transform.parent.rotation.eulerAngles.z + 180f) % 360;
                }

                hinge.spring = hingeSpring;

                if (rotationManager.rotation.eulerAngles.z < 270 && rotationManager.rotation.eulerAngles.z > 90)
                {
                    //shoot.upsideDown = true;
                    multiplier = -1;
                    //switched = refrenceKeeper.weaponInventory[activeSlot].switchOffset;
                    localiseTransform.setTransformHandheldF(WeaponHolder.transform.GetChild(2).gameObject, spawnWeapons.Weapons[WeaponIndex].WeaponItem.id);
                    bulletPositioner.transform.localPosition = spawnWeapons.Weapons[WeaponIndex].WeaponItem.muzzlePosition;
                    WeaponHolder.GetComponent<HingeJoint>().connectedAnchor = new Vector3(0.8939499f, 2f, 0);
                }
                else
                {
                    //shoot.upsideDown = false;
                    multiplier = 1;
                    //switched = refrenceKeeper.weaponInventory[activeSlot].switchOffset;
                    localiseTransform.setTransformHandheld(WeaponHolder.transform.GetChild(2).gameObject, spawnWeapons.Weapons[WeaponIndex].WeaponItem.id);
                    bulletPositioner.transform.localPosition = spawnWeapons.Weapons[WeaponIndex].WeaponItem.muzzlePosition + spawnWeapons.Weapons[WeaponIndex].WeaponItem.switchOffset;
                    WeaponHolder.GetComponent<HingeJoint>().connectedAnchor = new Vector3(0.8939499f, -2f, 0);
                }
            }
        }
    }

    float AngleBetweenPoints(Vector2 a, Vector2 b)
    {
        return Mathf.Atan2(a.y - b.y, a.x - b.x) * Mathf.Rad2Deg;
    }
}
