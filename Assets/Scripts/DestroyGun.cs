using UnityEngine;
using Mirror;

public class DestroyGun : NetworkBehaviour {

    RefrenceKeeper refrenceKeeper;

    void Start()
    {
        refrenceKeeper = GameObject.Find("Local").GetComponent<RefrenceKeeper>();
    }

    [Command]
    public void CmdDestroyGun(GameObject gun, int id)
    {
        gun.GetComponent<Pickup>().deactivated = true;
        Debug.Log("destroy");
        RpcDestroyGun(gun, id);
        refrenceKeeper.itemDistanceRefrences[id - 1] = 20;
        Destroy(gun);
    }

    [ClientRpc]
    void RpcDestroyGun(GameObject gun, int id)
    {
        GameObject.Find("Local").GetComponent<RefrenceKeeper>().itemDistanceRefrences[id - 1] = 20;
        Debug.Log("destroy");
        Destroy(gun);
    }
}
