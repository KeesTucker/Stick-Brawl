using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetupLoading : MonoBehaviour
{
    public Collider[] colliders;

    public GameObject name;

    // Use this for initialization
    void Start()
    {
        GameObject nameTag = Instantiate(name, transform.position, Quaternion.identity);
        nameTag.GetComponent<SyncName>().parent = gameObject;
        for (int i = 0; i < colliders.Length; i++)
        {
            //if (!isServer && !GetComponent<PlayerControl>())
            //{
            //    colliders[i].gameObject.layer = 14;
            //}
            for (int v = 0; v < colliders.Length; v++)
            {
                Physics.IgnoreCollision(colliders[i], colliders[v]);
            }
        }
    }
}
