using UnityEngine;
using Mirror;

public class SyncRotationAI : NetworkBehaviour
{

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

    public bool run = false;
    public GameObject AimPrefab;

    public Transform localParent;

    // Use this for initialization
    void Start()
    {
        parent.GetComponent<SpawnRotHolderAI>().CmdAssignAuthority(GetComponent<NetworkIdentity>());
    }

    public override void OnStartAuthority()
    {
        if (hasAuthority)
        {
            localParent = parent.GetComponent<PlayerSetupAI>().parent.transform;
            Ragdoll = localParent.Find("RagdollAI").gameObject;
            ULRA = localParent.Find("RagdollAI/ULRAR").gameObject;
            LLRA = localParent.Find("RagdollAI/ULRAR/LLRAR").gameObject;
            ULLA = localParent.Find("RagdollAI/ULLAR").gameObject;
            LLLA = localParent.Find("RagdollAI/ULLAR/LLLAR").gameObject;
            ULRL = localParent.Find("RagdollAI/ULRLR").gameObject;
            LLRL = localParent.Find("RagdollAI/ULRLR/LLRLR").gameObject;
            ULLL = localParent.Find("RagdollAI/ULLLR").gameObject;
            LLLL = localParent.Find("RagdollAI/ULLLR/LLLLR").gameObject;
            Head = localParent.Find("RagdollAI/HeadR").gameObject;
            Jetpack = localParent.Find("RagdollAI/JetpackR").gameObject;
            Weapon = LLRA.transform.Find("Rotation Gun ManagerAI/WeaponAI").gameObject;
            AimSync = localParent.Find("AIAim").gameObject;


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

            run = true;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
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
