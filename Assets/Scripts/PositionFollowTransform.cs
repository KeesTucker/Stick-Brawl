using UnityEngine;

public class PositionFollowTransform : MonoBehaviour
{

    public Transform playert;

    void Start()
    {
        playert = GameObject.Find("Ragdoll").transform;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = playert.position;
    }
}
