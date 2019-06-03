using UnityEngine;
using Mirror;

public class DestroyGunAI : NetworkBehaviour
{
    [Command]
    public void CmdDestroyGun(GameObject gun)
    {
        RpcDestroyGun(gun);
        Destroy(gun);
    }

    [ClientRpc]
    void RpcDestroyGun(GameObject gun)
    {
        Destroy(gun);
    }
}
