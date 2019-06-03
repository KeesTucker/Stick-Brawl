using UnityEngine;
using Mirror;

public class PlayerSetup : NetworkBehaviour {

    [SerializeField]
    Behaviour[] componentsToDisable;
    [SerializeField]
    Behaviour[] componentsToEnableOnLocal;
    [SerializeField]
    Behaviour[] componentsToDisableOnLocal;
    [SerializeField]
    Behaviour[] componentsToEnable;

    Camera sceneCamera;

    [SyncVar]
    public Color m_NewColor;

    [SyncVar]
    public bool jetOn;

    [SyncVar]
    public bool dropped = false;

    [SerializeField]
    Camera charCam;
    void Start()
    {
        if (!isLocalPlayer)
        {
            for (int i = 0; i < componentsToDisable.Length; i++)
            {
                componentsToDisable[i].enabled = false;
            }
            for (int i = 0; i < componentsToEnable.Length; i++)
            {
                componentsToEnable[i].enabled = true;
            }
            transform.Find("RagdollPlaceholder").gameObject.SetActive(true);
        }
        else
        {
            for (int i = 0; i < componentsToEnableOnLocal.Length; i++)
            {
                componentsToEnableOnLocal[i].enabled = true;
            }
            for (int i = 0; i < componentsToDisableOnLocal.Length; i++)
            {
                componentsToDisableOnLocal[i].enabled = true;
            }
            transform.Find("RagdollPlaceholder").gameObject.SetActive(false);
            gameObject.name = "LocalRelay";
            m_NewColor = GameObject.Find("Local").GetComponent<ColourSetter>().m_NewColor;
            GameObject.Find("Local/Ragdoll/ULRA/LLRA/Rotation Gun Manager/Weapon").GetComponent<Shoot>().Setup(gameObject);
            GameObject.Find("Local/Ragdoll").GetComponent<AimShoot>().Setup(gameObject);
            /*sceneCamera = Camera.main;
            gameObject.name = "Local";
            if (sceneCamera != null)
            {
                sceneCamera.gameObject.SetActive(false);
            }
            charCam.gameObject.SetActive(true);*/
        }
    }

    void Update()
    {
        if (isLocalPlayer)
        {
            CmdSyncJet(GameObject.Find("Local").transform.Find("Physics Animator").GetComponent<PlayerMovement>().jetOn);
        }
    }

    [Command]
    void CmdSyncJet(bool jetState)
    {
        jetOn = jetState;
    }

    /*void OnDisable()
    {
        if (sceneCamera != null)
        {
            sceneCamera.gameObject.SetActive(true);
        }
    }*/

}
