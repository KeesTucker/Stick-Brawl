using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimShoot : MonoBehaviour {
    public float thrust;
    public GameObject grapplePrefab;
    public GameObject WeaponHand;
    public Transform body;
    public Transform location;
    public Transform target;
    public Transform targetL;
    public Vector3 startAngle;
    public Rigidbody LH;
    public Vector3 switched = new Vector3(0, 0, 0);
    public Transform LHT;
    public Rigidbody RH;
    public Transform RHT;
    public Transform Weapon;
    IDKeeper iDKeeper;
    public bool fireGrapple = true;
    private bool rightClick;
    public GameObject[] playerParts;
    private GameObject grapple;
    public int WeaponIndex = 111;
    public int multiplier;
    public float switchedX;
    public Vector3 scale;
    public Vector3 position;
    public Vector3 positionFlipped;
    public int activeSlot;
    public GameObject bulletPositioner;
    public GameObject rotationGunManager;

    public Shoot shoot;
    public RefrenceKeeper refrenceKeeper;
    public LocaliseTransform localiseTransform;

    public GameObject grappleLauncher;

    public float angleGun;

    public GameObject recoilAdder;

    HingeJoint hinge;

    GameObject ragDoll;

    [SerializeField]
    HingeJoint GrappleArm;
    [SerializeField]
    HingeJoint GrappleHand;

    JointSpring hingeSpring;

    groundForce GroundForce;

    [SerializeField]
    Camera charCam;

    public GameObject LHTT;
    public GameObject localRelay;
    public SyncWeapon localWeaponSync;

    public SyncMoveState syncMoveState;

    IEnumerator Start()
    {
        shoot = GameObject.Find("Weapon").GetComponent<Shoot>();
        rotationGunManager = GameObject.Find("Rotation Gun Manager");
        ragDoll = GameObject.Find("Local/Ragdoll");
        fireGrapple = true;
        playerParts = GameObject.FindGameObjectsWithTag("Floppy");
        iDKeeper = GameObject.Find("Local/Ragdoll").GetComponent<IDKeeper>();
        yield return new WaitForEndOfFrame();
        refrenceKeeper = GameObject.Find("Local").GetComponent<RefrenceKeeper>();
        activeSlot = refrenceKeeper.activeSlot;
        hinge = GameObject.Find("Local/Ragdoll/ULRA/LLRA/Rotation Gun Manager/Weapon").GetComponent<HingeJoint>();
        hingeSpring = hinge.spring;
        GroundForce = GameObject.Find("Local/Ragdoll").GetComponent<groundForce>();
        yield return new WaitForSeconds(0.3f);
        localiseTransform = GameObject.Find("Items").GetComponent<LocaliseTransform>();
    }

    public void Setup(GameObject relay)
    {
        localRelay = relay;
        localWeaponSync = localRelay.GetComponent<SyncWeapon>();
    }

    void Update()
    {
        //Mouse Position in the world. It's important to give it some distance from the camera. 
        //If the screen point is calculated right from the exact position of the camera, then it will
        //just return the exact same position as the camera, which is no good.
        Vector3 mouseWorldPosition = charCam.ScreenToWorldPoint(Input.mousePosition + Vector3.forward * 10f);

        //Angle between mouse and this object
        float angle = AngleBetweenPoints(location.position, mouseWorldPosition);
        angleGun = AngleBetweenPoints(rotationGunManager.transform.position, mouseWorldPosition);
        //Ta daa

        target.rotation = Quaternion.Euler(new Vector3(0f, 0f, angle) - new Vector3(0f, 0f, body.rotation.eulerAngles.z + 90)); //This is for arm rotation, trying something new with weapon rotation

        
        grappleLauncher.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, angleGun + 180f));
        if (refrenceKeeper != null)
        {
            if (refrenceKeeper.weaponInventory.Count > 0)
            {
                //this bunch of crap converts the ugly value of the rotation of the gun + the aim rotation to a nice value in between -180 and 180 so it can be applied just basically get modulus and rectifys it, then applies it.
                if ((angleGun - rotationGunManager.transform.rotation.eulerAngles.z + 180f) % 360 < -180)
                {
                    hingeSpring.targetPosition = ((angleGun - rotationGunManager.transform.rotation.eulerAngles.z + 180f) % 360) + 360;
                }
                else if ((angleGun - rotationGunManager.transform.rotation.eulerAngles.z + 180f) % 360 > 180)
                {
                    hingeSpring.targetPosition = ((angleGun - rotationGunManager.transform.rotation.eulerAngles.z + 180f) % 360) - 360;
                }
                else
                {
                    hingeSpring.targetPosition = (angleGun - rotationGunManager.transform.rotation.eulerAngles.z + 180f) % 360;
                }

                syncMoveState.CmdSetArmState(true);

                hinge.spring = hingeSpring;
            }

            else
            {
                syncMoveState.CmdSetArmState(false);
            }

            activeSlot = refrenceKeeper.activeSlot;

            if (refrenceKeeper.weaponInventory.Count > 0)
            {
                if (target.rotation.eulerAngles.z < 360 && target.rotation.eulerAngles.z > 180)
                {
                    shoot.upsideDown = true;
                    multiplier = -1;
                    switched = refrenceKeeper.weaponInventory[activeSlot].switchOffset;
                    localiseTransform.setTransformHandheldF(WeaponHand.transform.GetChild(2).gameObject, refrenceKeeper.weaponInventory[activeSlot].id);
                    bulletPositioner.transform.localPosition = refrenceKeeper.weaponInventory[activeSlot].muzzlePosition;
                }
                else
                {
                    shoot.upsideDown = false;
                    multiplier = 1;
                    switched = refrenceKeeper.weaponInventory[activeSlot].switchOffset;
                    localiseTransform.setTransformHandheld(WeaponHand.transform.GetChild(2).gameObject, refrenceKeeper.weaponInventory[activeSlot].id);
                    bulletPositioner.transform.localPosition = refrenceKeeper.weaponInventory[activeSlot].muzzlePosition + refrenceKeeper.weaponInventory[activeSlot].switchOffset;
                }
                recoilAdder.transform.position = bulletPositioner.transform.position;

            }
        }

        if (WeaponHand.transform.childCount > 1)
        {
            RHT.gameObject.GetComponent<HingeJoint>().useSpring = true;
            location.gameObject.GetComponent<HingeJoint>().useSpring = true;
        }
        else
        {
            RHT.gameObject.GetComponent<HingeJoint>().useSpring = false;
            location.gameObject.GetComponent<HingeJoint>().useSpring = false;
        }

        if (Input.GetMouseButtonDown(1))
        {
            rightClick = true;
        }
        if (Input.GetMouseButtonUp(1))
        {
            Destroy(grapple, 0.1f);
            localWeaponSync.CmdGrappleKill();
            syncMoveState.CmdSetArmGrappleState(false);
            rightClick = false;
            GameObject.Find("Local/Ragdoll").GetComponent<groundForce>().grappled = false;
            fireGrapple = true;
        }
        if (rightClick == true)
        {
            targetL.rotation = Quaternion.Euler(new Vector3(0f, 0f, angle) - new Vector3(0f, 0f, body.rotation.eulerAngles.z + 90));
            FireGrapple();
            GameObject.Find("Local/Physics Animator").GetComponent<PlayerMovement>().grappleSince = true;

        }
        if (rightClick && GroundForce.touchingGround)
        {
            GrappleHand.useSpring = true;
            GrappleArm.useSpring = true;
        }
        else
        {
            GrappleHand.useSpring = false;
            GrappleArm.useSpring = false;
        }

        if(rightClick == false && GameObject.Find("Ragdoll").GetComponent<groundForce>().touchingGround == true)
        {
            foreach (GameObject playerPart in playerParts)
            {
                if (playerPart.GetComponent<ID>().IDPlayer == ragDoll.GetComponent<ID>().IDPlayer)
                {
                    playerPart.GetComponent<HingeJoint>().useSpring = true;
                }
            }
        }
    }

    public void FireGrapple()
    {
        if (fireGrapple)
        {
            syncMoveState.CmdSetArmGrappleState(true);
            localWeaponSync.CmdGrappleFire(grappleLauncher.transform.right, gameObject.GetComponent<Colouriser>().m_NewColor, gameObject);
            grapple = Instantiate(
                grapplePrefab,
                WeaponHand.transform.position,
                RHT.rotation);
            grapple.GetComponent<renderGrapple>().LHT = LHTT;
            grapple.GetComponent<grappleActivator>().hand = LHTT.transform.parent.gameObject;
            grapple.GetComponent<grappleActivator>().Setup(gameObject.GetComponent<Colouriser>().m_NewColor);
            grapple.GetComponent<renderGrapple>().Setup(gameObject.GetComponent<Colouriser>().m_NewColor);
            grapple.GetComponent<grappleActivator>().syncMoveState = syncMoveState;
            startAngle = grappleLauncher.transform.right;
            foreach (GameObject playerPart in iDKeeper.body)
            {
                Physics.IgnoreCollision(grapple.GetComponent<Collider>(), playerPart.GetComponent<Collider>());
            }
            for (int i = 0; i < 20; i++)
            {
                grapple.GetComponent<Rigidbody>().AddForce(startAngle * 600 * Time.deltaTime * 100);
            }
        }
        fireGrapple = false;
    }

    float AngleBetweenPoints(Vector2 a, Vector2 b)
    {
        return Mathf.Atan2(a.y - b.y, a.x - b.x) * Mathf.Rad2Deg;
    }

}
