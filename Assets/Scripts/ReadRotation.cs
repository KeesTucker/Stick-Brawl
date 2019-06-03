using UnityEngine;
using Mirror;

public class ReadRotation : NetworkBehaviour {

    public GameObject RagdollR;
    public GameObject ULRAR;
    public GameObject LLRAR;
    public GameObject ULLAR;
    public GameObject ULRLR;
    public GameObject LLLAR;
    public GameObject LLRLR;
    public GameObject ULLLR;
    public GameObject LLLLR;
    public GameObject HeadR;
    public GameObject JetpackR;
    public GameObject WeaponR;
    //public GameObject AimSyncR;

    public GameObject Ragdoll;
    /*public HingeJoint ULRA;
    public HingeJoint LLRA;
    public HingeJoint ULLA;
    public HingeJoint ULRL;
    public HingeJoint LLLA;
    public HingeJoint LLRL;
    public HingeJoint ULLL;
    public HingeJoint LLLL;
    public HingeJoint Head;
    public HingeJoint Jetpack;*/

    public GameObject ULRA;
    public GameObject LLRA;
    public GameObject ULLA;
    public GameObject ULRL;
    public GameObject LLLA;
    public GameObject LLRL;
    public GameObject ULLL;
    public GameObject LLLL;
    public GameObject Head;
    public GameObject Jetpack;
    public GameObject Weapon;
    //public GameObject AimSync;

    public JointSpring hingeSpring;

    [SyncVar]
    public GameObject parent;

    public float step = 1;

    public float speed;

    public bool run;

    // Use this for initialization
    void Start () {
        if (parent.name == "LocalRelay")
        {
            run = false;
        }
        else
        {
            run = true;
        }
        if (run)
        {
            RagdollR = gameObject;
            ULRAR = gameObject.transform.Find("ULRAR").gameObject;
            LLRAR = gameObject.transform.Find("ULRAR/LLRAR").gameObject;
            ULLAR = gameObject.transform.Find("ULLAR").gameObject;
            LLLAR = gameObject.transform.Find("ULLAR/LLLAR").gameObject;
            ULRLR = gameObject.transform.Find("ULRLR").gameObject;
            LLRLR = gameObject.transform.Find("ULRLR/LLRLR").gameObject;
            ULLLR = gameObject.transform.Find("ULLLR").gameObject;
            LLLLR = gameObject.transform.Find("ULLLR/LLLLR").gameObject;
            HeadR = gameObject.transform.Find("HeadR").gameObject;
            JetpackR = gameObject.transform.Find("JetpackR").gameObject;
            WeaponR = LLRAR.transform.Find("Rotation Gun Manager/Weapon").gameObject;
            //AimSyncR = gameObject.transform.Find("AimSync").gameObject;

            Ragdoll = parent.transform.Find("RagdollPlaceholder").gameObject;
            ULRA = parent.transform.Find("Physics Animator/ULRA").gameObject;
            LLRA = parent.transform.Find("Physics Animator/ULRA/LLRA").gameObject;
            ULLA = parent.transform.Find("Physics Animator/ULLA").gameObject;
            LLLA = parent.transform.Find("Physics Animator/ULLA/LLLA").gameObject;
            ULRL = parent.transform.Find("RagdollPlaceholder/ULRLR").gameObject;
            LLRL = parent.transform.Find("RagdollPlaceholder/ULRLR/LLRLR").gameObject;
            ULLL = parent.transform.Find("RagdollPlaceholder/ULLLR").gameObject;
            LLLL = parent.transform.Find("RagdollPlaceholder/ULLLR/LLLLR").gameObject;
            Head = parent.transform.Find("RagdollPlaceholder/HeadR").gameObject;
            Jetpack = parent.transform.Find("RagdollPlaceholder/JetpackR").gameObject;
            Weapon = parent.transform.Find("RagdollPlaceholder/ULRAR/LLRAR/Rotation Gun Manager/Weapon").gameObject;
            parent.GetComponent<SyncWeapon>().AimSync = gameObject;
            //AimSync = parent.transform.Find("AimSync").gameObject;
        }
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        if (run)
        {
            Ragdoll.transform.rotation = RagdollR.transform.rotation;
            ULRA.transform.rotation = ULRAR.transform.rotation;
            LLRA.transform.rotation = LLRAR.transform.rotation;
            ULLA.transform.rotation = ULLAR.transform.rotation;
            LLLA.transform.rotation = LLLAR.transform.rotation;
            //AimSync.transform.position = AimSyncR.transform.position;
            /*ULRL.transform.rotation = ULRLR.transform.rotation;
            LLRL.transform.rotation = LLRLR.transform.rotation;
            ULLL.transform.rotation = ULLLR.transform.rotation;
            LLLL.transform.rotation = LLLLR.transform.rotation;
            Head.transform.rotation = HeadR.transform.rotation;
            Jetpack.transform.rotation = JetpackR.transform.rotation;*/
            //Weapon.transform.rotation = WeaponR.transform.rotation;
            /*speed = step * Time.deltaTime;
            Ragdoll.transform.rotation = Quaternion.Slerp(Ragdoll.transform.rotation, RagdollR.transform.rotation, speed);
            ULRA.transform.rotation = Quaternion.Slerp(ULRA.transform.rotation, ULRAR.transform.rotation, speed);
            LLRA.transform.rotation = Quaternion.Slerp(LLRA.transform.rotation, LLRAR.transform.rotation, speed);
            ULLA.transform.rotation = Quaternion.Slerp(ULLA.transform.rotation, ULLAR.transform.rotation, speed);
            LLLA.transform.rotation = Quaternion.Slerp(LLLA.transform.rotation, LLLAR.transform.rotation, speed);
            ULRL.transform.rotation = Quaternion.Slerp(ULRL.transform.rotation, ULRLR.transform.rotation, speed);
            LLRL.transform.rotation = Quaternion.Slerp(LLRL.transform.rotation, LLRLR.transform.rotation, speed);
            ULLL.transform.rotation = Quaternion.Slerp(ULLL.transform.rotation, ULLLR.transform.rotation, speed);
            LLLL.transform.rotation = Quaternion.Slerp(LLLL.transform.rotation, LLLLR.transform.rotation, speed);
            Head.transform.rotation = Quaternion.Slerp(Head.transform.rotation, HeadR.transform.rotation, speed);
            Jetpack.transform.rotation = Quaternion.Slerp(Jetpack.transform.rotation, JetpackR.transform.rotation, speed);*/
            /*hingeSpring = ULRA.spring;
            hingeSpring.targetPosition = ULRAR.transform.rotation.eulerAngles.z;
            ULRA.spring = hingeSpring;
            hingeSpring = LLRA.spring;
            hingeSpring.targetPosition = LLRAR.transform.rotation.eulerAngles.z;
            LLRA.spring = hingeSpring;
            hingeSpring = ULLA.spring;
            hingeSpring.targetPosition = ULLAR.transform.rotation.eulerAngles.z;
            ULLA.spring = hingeSpring;
            hingeSpring = LLLA.spring;
            hingeSpring.targetPosition = LLLAR.transform.rotation.eulerAngles.z;
            LLLA.spring = hingeSpring;
            hingeSpring = ULRL.spring;
            hingeSpring.targetPosition = ULRLR.transform.rotation.eulerAngles.z;
            ULRL.spring = hingeSpring;
            hingeSpring = LLRL.spring;
            hingeSpring.targetPosition = LLRLR.transform.rotation.eulerAngles.z;
            LLRL.spring = hingeSpring;
            hingeSpring = ULLL.spring;
            hingeSpring.targetPosition = ULLLR.transform.rotation.eulerAngles.z;
            ULLL.spring = hingeSpring;
            hingeSpring = LLLL.spring;
            hingeSpring.targetPosition = LLLLR.transform.rotation.eulerAngles.z;
            LLLL.spring = hingeSpring;
            hingeSpring = Head.spring;
            hingeSpring.targetPosition = HeadR.transform.rotation.eulerAngles.z;
            Head.spring = hingeSpring;
            hingeSpring = Jetpack.spring;
            hingeSpring.targetPosition = JetpackR.transform.rotation.eulerAngles.z;
            Jetpack.spring = hingeSpring;*/
        }
    }
}
