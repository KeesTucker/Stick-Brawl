using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimShootAI : MonoBehaviour {
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

    public ShootAI shoot;
    public RefrenceKeeperAI refrenceKeeper;
    public LocaliseTransform localiseTransform;

    public GameObject grappleLauncher;

    public float angleGun;

    public GameObject recoilAdder;

    public HingeJoint hinge;
    [SerializeField]
    GameObject ragDoll;

    [SerializeField]
    HingeJoint GrappleArm;
    [SerializeField]
    HingeJoint GrappleHand;
    [SerializeField]
    JointSpring hingeSpring;

    [SerializeField]
    GroundForceAI GroundForce;

    public GameObject LHTT;
    public GameObject localRelay;

    public GameObject ragdoll;

    public GameObject animator;

    public bool RClick = false;

    public Transform aim;

    public bool rightBeenClicked;

    public Collider[] colliders;

    public HealthAI health;

    public GroundForceAI groundForceAI;

    IEnumerator Start()
    {
        groundForceAI = ragdoll.GetComponent<GroundForceAI>();
        fireGrapple = true;
        if (transform.gameObject.GetComponent<SetupLoading>())
        {
            colliders = transform.gameObject.GetComponent<SetupLoading>().colliders;
        }
        else
        {
            colliders = transform.gameObject.GetComponent<AISetup>().colliders;
        }
        yield return new WaitForEndOfFrame();
        if (refrenceKeeper)
        {
            activeSlot = refrenceKeeper.activeSlot;
        }
        hingeSpring = hinge.spring;
        yield return new WaitForSeconds(0.3f);
        if (GameObject.Find("Items"))
        {
            localiseTransform = GameObject.Find("Items").GetComponent<LocaliseTransform>();
        }
    }

    void Update()
    {
        if (aim)
        {
            Vector3 mouseWorldPosition = aim.position; //Make this follow a gameobject that the AI can control therefore it can aim etc.

            float angle = AngleBetweenPoints(location.position, mouseWorldPosition);
            if (rotationGunManager)
            {
                angleGun = AngleBetweenPoints(rotationGunManager.transform.position, mouseWorldPosition);
            }
            //Ta daa
            target.rotation = Quaternion.Euler(new Vector3(0f, 0f, angle) - new Vector3(0f, 0f, body.rotation.eulerAngles.z + 90)); //This is for arm rotation, trying something new with weapon rotation
            grappleLauncher.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, angleGun + 180f));

            if (refrenceKeeper != null)
            {
                if (refrenceKeeper.weaponHeld)
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

                    //syncMoveState.CmdSetArmState(true);

                    hinge.spring = hingeSpring;

                }

                else
                {
                    //syncMoveState.CmdSetArmState(false);
                }

                activeSlot = refrenceKeeper.activeSlot;

                if (refrenceKeeper.weaponHeld && health.health > 0)
                {
                    if (target.rotation.eulerAngles.z < 360 && target.rotation.eulerAngles.z > 180)
                    {
                        shoot.upsideDown = true;
                        multiplier = -1;
                        switched = refrenceKeeper.weaponInventory[activeSlot].switchOffset;
                        localiseTransform.setTransformHandheldF(WeaponHand.transform.GetChild(1).gameObject, refrenceKeeper.weaponInventory[activeSlot].id);
                        bulletPositioner.transform.localPosition = refrenceKeeper.weaponInventory[activeSlot].muzzlePosition;
                    }
                    else
                    {
                        shoot.upsideDown = false;
                        multiplier = 1;
                        switched = refrenceKeeper.weaponInventory[activeSlot].switchOffset;
                        localiseTransform.setTransformHandheld(WeaponHand.transform.GetChild(1).gameObject, refrenceKeeper.weaponInventory[activeSlot].id);
                        bulletPositioner.transform.localPosition = refrenceKeeper.weaponInventory[activeSlot].muzzlePosition + refrenceKeeper.weaponInventory[activeSlot].switchOffset;
                    }
                    recoilAdder.transform.position = bulletPositioner.transform.position;

                }
            }

            if (refrenceKeeper && !groundForceAI.dead)
            {
                if (refrenceKeeper.weaponHeld)
                {
                    RHT.gameObject.GetComponent<HingeJoint>().useSpring = true;
                    location.gameObject.GetComponent<HingeJoint>().useSpring = true;
                }
                else
                {
                    RHT.gameObject.GetComponent<HingeJoint>().useSpring = false;
                    location.gameObject.GetComponent<HingeJoint>().useSpring = false;
                }
            }
            else if (!groundForceAI.dead)
            {
                RHT.gameObject.GetComponent<HingeJoint>().useSpring = false;
                location.gameObject.GetComponent<HingeJoint>().useSpring = false;
            }

            if (RClick)
            {
                rightClick = true;
                rightBeenClicked = true;
            }
            if (!RClick && rightBeenClicked)
            {
                rightBeenClicked = false;
                Destroy(grapple, 0.1f);
                rightClick = false;
                groundForceAI.grappled = false;
                fireGrapple = true;
            }
            if (rightClick == true)
            {
                targetL.rotation = Quaternion.Euler(new Vector3(0f, 0f, angle) - new Vector3(0f, 0f, body.rotation.eulerAngles.z + 90));
                FireGrapple();
                animator.GetComponent<PlayerMovementAI>().grappleSince = true;

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

            if (rightClick == false && groundForceAI.touchingGround == true && !groundForceAI.dead)
            {
                foreach (GameObject playerPart in playerParts)
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
            grapple = Instantiate(
                grapplePrefab,
                WeaponHand.transform.position,
                RHT.rotation);
            grapple.GetComponent<renderGrapple>().LHT = LHTT;
            grapple.GetComponent<GrappleActivatorAI>().hand = LHTT.transform.parent.gameObject;
            grapple.GetComponent<GrappleActivatorAI>().parent = transform.gameObject;
            grapple.GetComponent<GrappleActivatorAI>().Setup(gameObject.GetComponent<ColouriserAI>().m_NewColor);
            grapple.GetComponent<renderGrapple>().Setup(gameObject.GetComponent<ColouriserAI>().m_NewColor);
            startAngle = grappleLauncher.transform.right;
            foreach (Collider collider in colliders)
            {
                Physics.IgnoreCollision(grapple.GetComponent<Collider>(), collider);
            }
            for (int i = 0; i < 20; i++)
            {
                grapple.GetComponent<Rigidbody>().AddForce(startAngle * (600 / 50) * Time.deltaTime * 100);
            }
        }
        fireGrapple = false;
    }

    float AngleBetweenPoints(Vector2 a, Vector2 b)
    {
        return Mathf.Atan2(a.y - b.y, a.x - b.x) * Mathf.Rad2Deg;
    }

}
