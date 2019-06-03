using UnityEngine;
using Mirror;

public class SyncRotation : NetworkBehaviour {

    public GameObject Ragdoll;
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

    public GameObject AimSync;

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

    public Quaternion holder;
    [SyncVar]
    public GameObject parent;

    public bool run;
    public GameObject AimPrefab;

    // Use this for initialization
    void Start () {

        if (parent.name != "LocalRelay")
        {
            run = false;
        }
        else
        {
            run = true;
        }
        if (run)
        {
            parent.GetComponent<SpawnRotHolder>().CmdAssignAuthority(GetComponent<NetworkIdentity>());
            Ragdoll = GameObject.Find("Local/Ragdoll");
            ULRA = GameObject.Find("Local/Ragdoll/ULRA");
            LLRA = GameObject.Find("Local/Ragdoll/ULRA/LLRA");
            ULLA = GameObject.Find("Local/Ragdoll/ULLA");
            LLLA = GameObject.Find("Local/Ragdoll/ULLA/LLLA");
            ULRL = GameObject.Find("Local/Ragdoll/ULRL");
            LLRL = GameObject.Find("Local/Ragdoll/ULRL/LLRL");
            ULLL = GameObject.Find("Local/Ragdoll/ULLL");
            LLLL = GameObject.Find("Local/Ragdoll/ULLL/LLLL");
            Head = GameObject.Find("Local/Ragdoll/Head");
            Jetpack = GameObject.Find("Local/Ragdoll/Jetpack");
            Weapon = LLRA.transform.Find("Rotation Gun Manager/Weapon").gameObject;
            AimSync = GameObject.Find("MouseFollower");


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
        }
    }

    // Update is called once per frame
    void FixedUpdate () {
        if (run)
        {
            holder = ULRA.transform.rotation;
            ULRAR.transform.rotation = holder;
            holder = Ragdoll.transform.rotation;
            RagdollR.transform.rotation = holder;
            holder = LLRA.transform.rotation;
            LLRAR.transform.rotation = holder;
            holder = ULLA.transform.rotation;
            ULLAR.transform.rotation = holder;
            holder = LLLA.transform.rotation;
            LLLAR.transform.rotation = holder;
            transform.position = AimSync.transform.position;
            //AimSyncR.transform.position = AimSync.transform.position;
            /*holder = ULRL.transform.rotation;
            ULRLR.transform.rotation = holder;
            holder = LLRL.transform.rotation;
            LLRLR.transform.rotation = holder;
            holder = ULLL.transform.rotation;
            ULLLR.transform.rotation = holder;
            holder = LLLL.transform.rotation;
            LLLLR.transform.rotation = holder;
            holder = Head.transform.rotation;
            HeadR.transform.rotation = holder;
            holder = Jetpack.transform.rotation;
            JetpackR.transform.rotation = holder;*/
            //holder = Weapon.transform.rotation;
            //WeaponR.transform.rotation = holder;
        }
    }
}
