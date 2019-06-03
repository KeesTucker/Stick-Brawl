using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class SyncPos : NetworkBehaviour {

    float time;

    public float sendRate = 9;
    public float sendDelay;

    public Vector2 lastPos;
    public Vector2 nextPos;

    public bool start;
    public float progress;

    public Rigidbody rb;

    new Vector2 posAim;
	
    void Start()
    {
        sendDelay = 1f / sendRate;
        time = Random.Range(0, sendDelay);
    }
	// Update is called once per frame
	void Update () {
        if (hasAuthority)
        {
            time += Time.deltaTime;
            if (time > sendDelay)
            {
                time = 0;
                CmdSetPos(new Vector2(transform.position.x, transform.position.y));
            }
        }
        else
        {
            progress += (1 / (sendDelay / (Time.deltaTime)));
            posAim = new Vector2(Mathf.Lerp(lastPos.x, nextPos.x, progress), Mathf.Lerp(lastPos.y, nextPos.y, progress));
            rb.AddForce((posAim.x - transform.position.x) * 50000 * Time.deltaTime, (posAim.y - transform.position.y) * 50000 * Time.deltaTime, 0);
        }
	}

    [Command]
    public void CmdSetPos(Vector2 pos)
    {
        if (!hasAuthority)
        {
            if (!start)
            {
                transform.position = new Vector3(pos.x, pos.y, 0);
                lastPos = pos;
                start = true;
            }
            else
            {
                lastPos = new Vector2(transform.position.x, transform.position.y);
            }
            nextPos = pos;
            progress = 0;
        }
        RpcSetPos(pos);
    }

    [ClientRpc]
    public void RpcSetPos(Vector2 pos)
    {
        if (!hasAuthority)
        {
            lastPos = nextPos;
            nextPos = pos;
            progress = 0;
        }
    }
}
