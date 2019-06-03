using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class SyncPosChild : NetworkBehaviour
{

    float time;

    public GameObject child;

    public float sendRate = 9;

    void Start()
    {
        time = Random.Range(0, 1f / sendRate);
    }
    // Update is called once per frame
    void Update()
    {
        if (hasAuthority)
        {
            time += Time.deltaTime;
            if (time > 1f / sendRate)
            {
                time = 0;
                CmdSetPos(new Vector2(child.transform.position.x - transform.position.x, child.transform.position.y - transform.position.y));
            }
        }
    }

    [Command]
    public void CmdSetPos(Vector2 pos)
    {
        if (!hasAuthority)
        {
            child.transform.position = new Vector3(pos.x + transform.position.x, pos.y + transform.position.y, 0);
        }
        RpcSetPos(pos);
    }

    [ClientRpc]
    public void RpcSetPos(Vector2 pos)
    {
        if (!hasAuthority)
        {
            child.transform.position = new Vector3(pos.x + transform.position.x, pos.y + transform.position.y, 0);
        }
    }
}
