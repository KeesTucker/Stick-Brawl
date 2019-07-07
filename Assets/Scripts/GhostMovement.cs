using UnityEngine;
using Mirror;
using System.Collections;
using System.Collections.Generic;

public class GhostMovement : NetworkBehaviour {

    public Rigidbody rb;
    public GameObject proj;

    [SyncVar]
    public GameObject parent;
    public float force = 200f;
    public int shots = 3;
    public int abilityIndex = 0;

    public GameObject cameraGO;

    public VariableJoystick joystick;

    // Use this for initialization
    public override void OnStartAuthority () {
        gameObject.tag = "ghostLocal";
        Destroy(parent.GetComponent<CamControl>().cameraGO);
        GameObject cam = Instantiate(cameraGO, transform.position, Quaternion.identity);
        cam.GetComponent<CamFollowAI>().parent = transform;
        joystick = GameObject.Find("RightJ").GetComponent<VariableJoystick>();
        foreach (GameObject go in GameObject.FindGameObjectsWithTag("Chunk"))
        {
            if (go.name == "TerrainLoader(Clone)")
            {
                go.GetComponent<ChunkLoad>().local = transform;
            }
        }
    }

	void FixedUpdate () {
        rb.AddForce(force * joystick.Direction);
    }
}
