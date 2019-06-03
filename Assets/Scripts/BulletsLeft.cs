using UnityEngine;
using Mirror;

public class BulletsLeft : NetworkBehaviour {

    [SyncVar]
    public int bullets = 0;
}
